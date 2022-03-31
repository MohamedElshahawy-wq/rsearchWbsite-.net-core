using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace researchOnlineWebsite.Controllers
{
    public class AdminsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Em, string pa)
        {
        SqlConnection conn1 = new SqlConnection("Server=.;Initial Catalog=researchWebsite;Integrated Security=True");

        string sql;
        sql = "SELECT * FROM adminUsers where email ='" + Em + "' and  password ='" + pa + "' ";
        SqlCommand comm = new SqlCommand(sql, conn1);
        conn1.Open();
        SqlDataReader reader = comm.ExecuteReader();

        if (reader.Read())
        {

                //string role = (string)reader["roleId"];
                string role = reader["roleId"].ToString();
                //    string id = Convert.ToString((int)reader["Id"]);
                //    HttpContext.Session.SetString("Name", na);
                //    HttpContext.Session.SetString("Role", role);
                //    HttpContext.Session.SetString("userid", id);
                reader.Close();
                conn1.Close();
                if (role == "1")
                    return RedirectToAction("allUsers", "Supervisorofcustomerrequests");

                else if (role == "2")
                    return RedirectToAction("Index", "Admin");

                else if (role == "3")
                    return RedirectToAction("Index", "GeographyAdmin");

                else if (role == "5")
                   return RedirectToAction("Index", "ResearchContents");
                else
                    ViewData["Message1"] = "u don't have any role";
                return View();


            }
            else
            {
                ViewData["Message"] = "wrong user name password";
                return View();
            }
        }

}
}
