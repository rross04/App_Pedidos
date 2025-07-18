using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;
using App_Pedidos;

namespace App_Pedidos
{
    public partial class Form3 : Form
    {
        private List<Producto> productos = new List<Producto>();
        private List<Producto> productosFiltrados = new List<Producto>();

        public Form3()
        {
            InitializeComponent();
        }

        private async void Form3_Load(object sender, EventArgs e)
        {
            string url = "https://localhost:5001/api/productos";
            HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    productos = JsonConvert.DeserializeObject<List<Producto>>(json);
                    productosFiltrados = new List<Producto>(productos);

                    // Llenar ComboBox categorías
                    var categorias = productos.Select(p => p.Categoria).Distinct().ToList();
                    categorias.Insert(0, "Todas");
                    cmbCategoria.DataSource = categorias;

                    // Mostrar productos
                    MostrarProductos(productosFiltrados);
                }
                else
                {
                    MessageBox.Show("No se pudieron cargar los productos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void MostrarProductos(List<Producto> lista)
        {
            dgvProductos.Columns.Clear();
            dgvProductos.Rows.Clear();
            dgvProductos.AutoGenerateColumns = false;

            dgvProductos.Columns.Add("Id", "ID");
            dgvProductos.Columns.Add("Nombre", "Nombre");
            dgvProductos.Columns.Add("Categoria", "Categoría");
            dgvProductos.Columns.Add("Precio", "Precio");

            var cantidadCol = new DataGridViewNumericUpDownColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                Minimum = 1,
                Maximum = 100,
                Value = 1
            };
            dgvProductos.Columns.Add(cantidadCol);

            foreach (var prod in lista)
            {
                int rowIndex = dgvProductos.Rows.Add(prod.Id, prod.Nombre, prod.Categoria, prod.Precio);
                dgvProductos.Rows[rowIndex].Cells["Cantidad"].Value = 1;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarProductos();
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarProductos();
        }

        private void FiltrarProductos()
        {
            string texto = txtBuscar.Text.ToLower();
            string categoria = cmbCategoria.SelectedItem?.ToString() ?? "Todas";

            productosFiltrados = productos
                .Where(p => (categoria == "Todas" || p.Categoria == categoria) &&
                            p.Nombre.ToLower().Contains(texto))
                .ToList();

            MostrarProductos(productosFiltrados);
        }

        private void btnAgregarAlCarrito_Click(object sender, EventArgs e)
        {
            List<Producto> productosSeleccionados = new List<Producto>();

            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (!row.IsNewRow)
                {
                    object cantidadObj = row.Cells["Cantidad"].Value;

                    if (cantidadObj != null && int.TryParse(cantidadObj.ToString(), out int cantidad) && cantidad > 0)
                    {
                        Producto producto = new Producto
                        {
                            Id = Convert.ToInt32(row.Cells["Id"].Value),
                            Nombre = row.Cells["Nombre"].Value?.ToString(),
                            Categoria = row.Cells["Categoria"].Value?.ToString(),
                            Precio = Convert.ToDecimal(row.Cells["Precio"].Value),
                            Cantidad = cantidad
                        };

                        productosSeleccionados.Add(producto);
                    }
                }
            }

            if (productosSeleccionados.Count > 0)
            {
                FormCarrito formCarrito = new FormCarrito(productosSeleccionados);
                formCarrito.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione al menos un producto con cantidad mayor a cero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
