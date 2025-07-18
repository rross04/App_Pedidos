using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace App_Pedidos.Database
{
    public class Connection
    {
        // Connection string for SQL Server database
       // private readonly string connectionString =
        //    "Data Source=localhost;Initial Catalog=Pedidos;Integrated Security=True;Pooling=False;MultipleActiveResultSets=True";

        /*Use one or the other string, depending on whether you have express or normal sqlserver*/
        
          private readonly string connectionString =
         "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Pedidos;Integrated Security=True;Pooling=False;MultipleActiveResultSets=True";
 


        //Method to get a new SQL connection
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
