using AdminSiteAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AdminSiteAPI.Controllers
{
    public class ClientController : Controller
    {
        [Route("api/Admin/getClientList")]
        public List<Client> Index()
        {
            List<Client> clients = new List<Client>();
            SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from clients", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                clients.Add(new Client
                {
                    Id = dr["id"].ToString(),
                    ClientName = dr["clientname"].ToString(),
                    ClientProject = dr["clientproject"].ToString(),
                    ClientDepartment = dr["clientdepartment"].ToString(),
                    ClientType = dr["clienttype"].ToString()
                });
            }
            return clients;
        }

        [Route("api/Admin/getClientList/{id}")]
        public Client getClientListById(int id)
        {
            Client client = new Client();
            SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from clients where id= @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                client.Id = dr["id"].ToString();
                client.ClientName = dr["clientname"].ToString();
                client.ClientProject = dr["clientproject"].ToString();
                client.ClientDepartment = dr["clientdepartment"].ToString();
                client.ClientType = dr["clienttype"].ToString();
            }
            return client;
        }

        [HttpPut]
        [Route("api/Admin/updateClient")]
        public int updateClient([FromBody] Client client)
        {
            SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "update Clients set clientName=@clientName, clientProject=@clientProject, clientDepartment=@clientDepartment, clientType=@clientType where id=@id";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            cmd.Parameters.AddWithValue("@id", client.Id);
            cmd.Parameters.AddWithValue("@clientname", client.ClientName);
            cmd.Parameters.AddWithValue("@clientProject", client.ClientProject);
            cmd.Parameters.AddWithValue("@clientDepartment", client.ClientDepartment);
            cmd.Parameters.AddWithValue("@clientType", client.ClientType);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        [HttpPost]
        [Route("api/Admin/addClient")]
        public int addClient([FromBody] Client client)
        {
            SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "insert into Clients values(@id,@clientname,@clientProject,@clientDepartment,@clientType)";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            cmd.Parameters.AddWithValue("@id", client.Id);
            cmd.Parameters.AddWithValue("@clientname", client.ClientName);
            cmd.Parameters.AddWithValue("@clientProject", client.ClientProject);
            cmd.Parameters.AddWithValue("@clientDepartment", client.ClientDepartment);
            cmd.Parameters.AddWithValue("@clientType", client.ClientType);
            int result = cmd.ExecuteNonQuery();
            return result;
        }


        [HttpDelete]
        [Route("api/Admin/deleteClient/{id}")]
        public int deleteClient(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=PRASHANTHTH-LT;Initial Catalog=AdmineSiteDB;Integrated Security=True");
            con.Open();
            string sqlStr = "delete from Clients where id=@id";
            SqlCommand cmd = new SqlCommand(sqlStr, con);
            var usr = getClientListById(id);
            cmd.Parameters.AddWithValue("@id", usr.Id);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}
