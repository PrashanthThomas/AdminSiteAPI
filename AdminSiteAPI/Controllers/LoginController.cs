using AdminSiteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AdminSiteAPI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("api/Admin/verifyLogin")]
        public AdminLogin verifyLogin([FromBody] AdminLogin adminLoginDet)
        {
                AdminLogin userDet = new AdminLogin();
                SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from adminLogin where username= @username and pwd= @pwd", con);
                cmd.Parameters.AddWithValue("@username", adminLoginDet.UserName);
                cmd.Parameters.AddWithValue("@pwd", adminLoginDet.Pwd);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                userDet.UserName = dr["username"].ToString();
                userDet.PermissionLevel = dr["permissionlevel"].ToString();
            } else
            {
                return null;
            }
                return userDet;
           
        }
    }
}
