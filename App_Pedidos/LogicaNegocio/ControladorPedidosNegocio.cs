using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_Pedidos.PedidosServiceRef;

namespace App_Pedidos.LogicaNegocio
{
    public class ControladorPedidosNegocio
    {
        private PedidosServiceSoapClient servicio;

        public ControladorPedidosNegocio()
        {
            servicio = new PedidosServiceSoapClient();
        }

        /// <summary>
        /// Obtiene todos los pedidos para la vista de negocio.
        /// </summary>
        /// <returns>DataTable con los pedidos</returns>
        public DataTable ObtenerPedidos()
        {
            DataSet ds = servicio.ObtenerListadoCompletoPedidosConCliente();
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return new DataTable();
        }
    }
}
