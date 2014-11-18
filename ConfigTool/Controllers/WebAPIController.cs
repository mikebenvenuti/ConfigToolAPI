using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;


namespace ConfigTool.Controllers
{
    public class WebAPIController : ApiController
    {
        
        
        
        // GET api/webapi
        public IEnumerable<Product> GetAll()
        {
            DB.ApplicationName = "Web API App";
            SqlConnection conn = DB.GetSqlConnection();

            Products ps = new Products();
            IEnumerable<Product> p = ps.GetAllProducts();

            return p;
            
        }

     

        // GET api/webapi/5
        public string Get(int id)
        {
            DataLayer.DB.ApplicationName = "Web API App";
            SqlConnection conn = DataLayer.DB.GetSqlConnection();

            Products ps = new Products();
            DataLayer.Product p = ps.GetProduct(id);

            return p.Name;
        }

        // POST api/webapi
        
        public void Post(Product jerky)
        {
            if (jerky == null)
            {    }
            Products.InsertJerky(jerky);
            
        }

        // PUT api/webapi/5
        [HttpPut]
        public void Update(Product jerky)
        {
            SqlConnection conn = DB.GetSqlConnection();
            Products.UpdateProduct(jerky);
        }

        // DELETE api/webapi/5
        public void Delete(int id)
        {
        }
    }
}
