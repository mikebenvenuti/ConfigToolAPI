using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConfigTool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //string connStr = DataLayer.DB.ConnectionString;
            DataLayer.DB.ApplicationName = "ConfigTool App";
            SqlConnection conn = DataLayer.DB.GetSqlConnection();


            return View();
        }
    }
}
