using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace App_Pedidos
{
    public partial class FormCarrito : Form
    {
        private List<Producto> productosCarrito;

        public FormCarrito(List<Producto> productos)
        {
            InitializeComponent();
            productosCarrito = productos;
        }

        private void FormCarrito_Load(object sender, EventArgs e)
        {
            dataGridViewCarrito.Columns.Clear();
            dataGridViewCarrito.Rows.Clear();

            dataGridViewCarrito.Columns.Add("Id", "ID");
            dataGridViewCarrito.Columns.Add("Nombre", "Nombre");
            dataGridViewCarrito.Columns.Add("Categoria", "Categoría");
            dataGridViewCarrito.Columns.Add("Precio", "Precio");
            dataGridViewCarrito.Columns.Add("Cantidad", "Cantidad");
            dataGridViewCarrito.Columns.Add("Total", "Total");

            foreach (var producto in productosCarrito)
            {
                dataGridViewCarrito.Rows.Add(
                    producto.Id,
                    producto.Nombre,
                    producto.Categoria,
                    producto.Precio,
                    producto.Cantidad,
                    producto.Total
                );
            }
        }
    }

    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; } 
        public decimal Total => Precio * Cantidad;
    }
}
