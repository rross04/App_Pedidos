using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Pedidos
{
    namespace App_Pedidos
    {
        public class Producto
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Categoria { get; set; }
            public decimal Precio { get; set; }
            public int Cantidad { get; set; }

            public decimal Total
            {
                get { return Precio * Cantidad; }
            }
        }
    }

}
