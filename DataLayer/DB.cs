using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class DB
    {
        public static string ConnectionString
        {
            get
            {
                string connStr =  ConfigurationManager.ConnectionStrings["JerkyDB"].ToString();

                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connStr);
                sb.ApplicationName = ApplicationName ?? sb.ApplicationName;

                return sb.ToString();
             }
        }
        /// <summary>
        /// Returns an open connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetSqlConnection() {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        public static SqlCommand CreateCommand() {
             SqlConnection conn = DB.GetSqlConnection();

             SqlCommand cmd = new SqlCommand();
             cmd.Connection = conn;
              return cmd;
           
        }
        

               

        public static int  ConnectionTimeout { get; set; }
        
        public static string ApplicationName  { get; set; }

        public abstract class OurDbBaseClass<TEntity>
        {
            protected IEnumerable<TEntity> ToList(IDbCommand command)
            {
                using (var reader = command.ExecuteReader())
                {
                    List<TEntity> items = new List<TEntity>();
                    while (reader.Read())
                    {
                        var item = CreateEntity();
                        Map(reader, item);
                        items.Add(item);
                    }
                    return items;
                }
            }

            protected abstract void Map(IDataRecord record, TEntity entity);
            protected abstract TEntity CreateEntity();
        }
    }
}


//   Data Source=MIKE-PC;Initial Catalog=Jerky;Persist Security Info=True;User ID=mike;Password=mike
//       Server=MIKE-PC;;Initial Catalog=Jerky;User Id=mike;Password=mike
