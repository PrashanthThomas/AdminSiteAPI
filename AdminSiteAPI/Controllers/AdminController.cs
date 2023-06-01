using AdminSiteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AdminSiteAPI.Controllers
{
    [ApiController]
    public class AdminController : Controller
    {

        [Route("api/Admin/getUserList")]
        public List<User> Index()
        {
            List<User> users = new List<User>();
            SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from users", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                users.Add(new User
                {
                    Id = dr["id"].ToString(),
                    UserName = dr["username"].ToString(),
                    UserType = dr["usertype"].ToString(),
                    CompanyId = dr["companyid"].ToString(),
                    CompanyName = dr["companyname"].ToString()
                });
            }
            return users;
        }

        [Route("api/Admin/getUserList/{id}")]
        public User getUserListById(int id)
        {
            User user = new User();
            SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from users where id= @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                user.Id = dr["id"].ToString();
                user.UserName = dr["username"].ToString();
                user.UserType = dr["usertype"].ToString();
                user.CompanyId = dr["companyid"].ToString();
                user.CompanyName = dr["companyname"].ToString();
            }
            return user;
        }

        [HttpPut]
        [Route("api/Admin/updateUser")]
        public int updateUser([FromBody] User usr)
        {
            SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "update Users set userName=@userName, companyId=@companyId, companyname=@companyName, userType=@userType where id=@id";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            cmd.Parameters.AddWithValue("@id", usr.Id);
            cmd.Parameters.AddWithValue("@username", usr.UserName);
            cmd.Parameters.AddWithValue("@companyID", usr.CompanyId);
            cmd.Parameters.AddWithValue("@companyName", usr.CompanyName);
            cmd.Parameters.AddWithValue("@userType", usr.UserType);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        [HttpPost]
        [Route("api/Admin/addUser")]
        public int addUser([FromBody] User usr)
        {
            SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "insert into Users values(@id,@username,@companyID,@companyName,@userType)";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            cmd.Parameters.AddWithValue("@id", usr.Id);
            cmd.Parameters.AddWithValue("@username", usr.UserName);
            cmd.Parameters.AddWithValue("@companyID", usr.CompanyId);
            cmd.Parameters.AddWithValue("@companyName", usr.CompanyName);
            cmd.Parameters.AddWithValue("@userType", usr.UserType);
            int result = cmd.ExecuteNonQuery();
            return result;
        }


        [HttpDelete]
        [Route("api/Admin/deleteUser/{id}")]
        public int deleteUser(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "delete from Users where id=@id";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            var usr = getUserListById(id);
            cmd.Parameters.AddWithValue("@id", usr.Id);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}