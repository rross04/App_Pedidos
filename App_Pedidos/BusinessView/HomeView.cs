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
    public partial class HomeView : Form
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PedidosView pv = new PedidosView();

            pv.FormClosed += (s, args) => this.Show();

            this.Hide();
            pv.Show();
           
            
            
        }
    }
}
