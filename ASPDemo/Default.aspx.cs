using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ASPDemo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataLayer.DB.ApplicationName = "ASPDemo App";
            SqlConnection conn = DataLayer.DB.GetSqlConnection();

            IEnumerable<DataLayer.Group> x = DataLayer.Groups.FindGroups();

            var cmd = new SqlCommand("SELECT * FROM Members", conn);
            using (var rdr = cmd.ExecuteReader()) {
     
              while (rdr.Read()) {
                var id = rdr["userID"];
                var name = rdr["name"];
                
                
              }
            }

        }

        protected void LinkButtonGet_Click(object sender, EventArgs e)
        {
            try
            {
                DataLayer.Products ps = new DataLayer.Products();
                DataLayer.Product product = ps.GetProduct(int.Parse(TextBoxID.Text));

                TextBoxName.Text = product.Name;
                TextBoxDesc.Text = product.Description;
                
                DataTable tableLog = DataLayer.Groups.GetTable("Groups");
                             
                GridView1.DataSource = tableLog;
                GridView1.DataBind();
                DataList1.DataSource = tableLog;
                TextBoxName.Text = tableLog.Rows[0].ItemArray[0].ToString();
         

            }
            catch {}
        }

        protected void LinkButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
              DataLayer.Groups.AddGroup(TextBoxName.Text);
            }
            catch { }
        }
    }
}

