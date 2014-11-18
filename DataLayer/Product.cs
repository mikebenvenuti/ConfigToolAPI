using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class  Products {

        public List<Product> ProductList { get; set; }


        public static int GetNextValue(string tablename)
        {
            Product p = new Product();

            using (SqlConnection conn = DB.GetSqlConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"NextValue";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter p1 = new SqlParameter("table", System.Data.SqlDbType.VarChar);
                    SqlParameter seq = new SqlParameter("seq", System.Data.SqlDbType.Int);
                    seq.Direction = ParameterDirection.Output;
                    /*
                    SqlParameter returnvalue = new SqlParameter();
                    returnvalue.Direction = ParameterDirection.ReturnValue; */

                    p1.Value = tablename;  
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(seq);
                    cmd.ExecuteReader();
                    return (int)seq.Value;
                    
                }
            }
            
         }

        public Product spGetProduct(int productid)
        {
            Product p = new Product();

            using (SqlConnection conn = DB.GetSqlConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"GetProductDetails";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter p1 = new SqlParameter("product", System.Data.SqlDbType.Int);
                    p1.Value = productid;
                    cmd.Parameters.Add(p1);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        p.Load(reader);
                    }
                    
                }
            }
            return p;
         }


        public static void InsertJerky(Product Jerky)
            
        {
       
            // define INSERT query with parameters
            string query = "INSERT INTO Products (Name, Description,Price,Quantity) " + 
                           "VALUES ( @Name, @Description,@Price,@Quantity) ";

            // create connection and command
            using (SqlConnection cn = DB.GetSqlConnection())
            using(SqlCommand cmd = new SqlCommand(query, cn))
            {
                // define parameters and their values
                //cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = GetNextValue("Products");
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = Jerky.Name;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = Jerky.Description;
                cmd.Parameters.Add("@Price", SqlDbType.Money).Value = Jerky.Price;
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Jerky.Quantity;
                
                // open connection, execute INSERT, close connection
                //cn.Open();
                cmd.ExecuteNonQuery();
                //cn.Close();
            }
        }



      public static void UpdateProduct(Product Jerky)
            
        {
       
            // define INSERT query with parameters
            string query = @"update Products SET Name =  @Name, Description=@Description," +
                             "Price = @Price, Quantity = @Quantity " + 
                             "where Product_id = @PID";
           
            // create connection and command
            using (SqlConnection cn = DB.GetSqlConnection())
            using(SqlCommand cmd = new SqlCommand(query, cn))
            {
                // define parameters and their values
                cmd.Parameters.Add("@PID", SqlDbType.Int).Value = Jerky.Product_id;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = Jerky.Name;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = Jerky.Description;
                cmd.Parameters.Add("@Price", SqlDbType.Money).Value = Jerky.Price;
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Jerky.Quantity;
                
                // open connection, execute INSERT, close connection
                //cn.Open();
                cmd.ExecuteNonQuery();
                //cn.Close();
            }
        }


        public IEnumerable<Product> GetAllProducts()
        {
            Product p = new Product();
            using (SqlConnection conn = DB.GetSqlConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select * from Products";
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        p.Load(reader);
                        yield return p;
                    }
                }
            }
            
        }

       /*   or do this
        * 
        * 
        * public IEnumerable<Customer> GetAll()
        {     
            List<Customer> customers = new List<Customer>();
            string query = string.Format("SELECT [CustomerID], [CompanyName], 
                       [ContactName], [ContactTitle], [City], [Phone] FROM [Customers]");

            using (SqlConnection con =
                    new SqlConnection("your connection string"))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerID = reader.GetString(0);
                        customer.CompanyName = reader.GetString(1);
                        customer.ContactName = reader.GetString(2);
                        customer.ContactTitle = reader.GetString(3);
                        customer.City = reader.GetString(4);
                        customer.Phone = reader.GetString(5);
                        customers.Add(customer);
                    }
                }
            }
            return customers.ToArray();
        }
        * 
        * 
        * 
        * 
        * 
        */


        public Product GetProduct(int product_id)
        {
            Product p = new Product();
            using (SqlConnection conn = DB.GetSqlConnection())
            {
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select * from Products where Product_id = {0}";
                    cmd.CommandText = string.Format(cmd.CommandText, product_id.ToString());
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        p.Load(reader);
                    }
                }
            }
            return p;
        }
    }

    public class Product
    {
        public int Product_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Double Price { get; set; }
        public int Quantity { get; set; }


        public void Load(SqlDataReader reader)
        {
            Product_id = Int32.Parse(reader["Product_id"].ToString());
            Name = reader["Name"].ToString();
            Description = reader["Description"].ToString();
            Price = double.Parse(reader["Price"].ToString());
            Quantity = Int32.Parse(reader["Quantity"].ToString());
        }

    }
    

}
