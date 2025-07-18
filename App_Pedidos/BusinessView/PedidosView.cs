using App_Pedidos.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_Pedidos.BusinessView
{
    public partial class PedidosView : Form
    {
        //instancia el controlador de pedidos
        private ControladorPedidosNegocio controlador;
        public PedidosView()
        {
            InitializeComponent();
            controlador = new ControladorPedidosNegocio();
        }

        private void fetchPedidos() {
            dgvPedidos.DataSource = controlador.ObtenerPedidos();
            dgvPedidos.Columns["ClienteId"].HeaderText = "Id del cliente";
            dgvPedidos.Columns["PedidoID"].HeaderText = "Id del Pedido";
        }

        private void PedidosView_Load(object sender, EventArgs e)
        {
            fetchPedidos();
        }

        private void dgvPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
