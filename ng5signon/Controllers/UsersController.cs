using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ng5signon.Models;

namespace ng5signon.Controllers
{
    // EnableCors("AllowAll")]
    [Produces("application/json")]
    [Route("api/[Controller]/[Action]")]
    public class UsersController : Controller
    {

        private SqlConnection con;
        private void connection()
        {
            con = new SqlConnection(@"Data Source=KP\sqlexpress2014;Initial Catalog=ng5;Integrated Security=True");
        }

        // GET: api/Users
        [HttpGet]
        public resultset Get()
        {
            resultset result = new resultset();

            List<Users> usersList = new List<Users>();
            try
            {
                connection();

                SqlCommand cmd = new SqlCommand("getAllUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Users user = new Users()
                    {
                        id = Convert.ToInt32(dr["id"]),
                        name = Convert.ToString(dr["name"]),
                        phone = Convert.ToString(dr["phone"]),
                        email = Convert.ToString(dr["email"]),
                        //pass = Convert.ToString(dr["pass"])
                    };
                    usersList.Add(user);
                }

                result.isSuccess = true;
                result.data = usersList;
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.exceptionMessage = ex.Message;
            }
            return result;
        }

        // GET: api/Users/5
        //[HttpGet("{id}", Name = "Get")]
        //public resultset Get(int id)
        //{
        //    resultset result = new resultset();

        //    List<Users> usersList = new List<Users>();
        //    try
        //    {
        //        connection();

        //        SqlCommand cmd = new SqlCommand("getUserById", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@id", id);
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        con.Close();
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            Users user = new Users()
        //            {
        //                id = Convert.ToInt32(dr["id"]),
        //                name = Convert.ToString(dr["name"]),
        //                phone = Convert.ToString(dr["phone"]),
        //                email = Convert.ToString(dr["email"]),
        //                pass = Convert.ToString(dr["pass"])
        //            };
        //            usersList.Add(user);
        //        }

        //        result.isSuccess = true;
        //        result.data = usersList;
        //        result.message = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        result.isSuccess = false;
        //        result.exceptionMessage = ex.Message;
        //    }
        //    return result;
        //}

        [ActionName("Login")]
        [HttpPost]
        public resultset loginUser([FromBody] Users user)
        {
            resultset result = new resultset();
            List<Users> usersList = new List<Users>();
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "loginUser"
                };

                cmd.Connection = con;
                //cmd.CommandText = "loginUser";
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", user.email);
                //cmd.Parameters.AddWithValue("@pass", user.pass);

                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Close();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Users u = new Users()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            name = Convert.ToString(dr["name"]),
                            phone = Convert.ToString(dr["phone"]),
                            email = Convert.ToString(dr["email"]),
                            pass = Convert.ToString(dr["pass"])
                        };
                        usersList.Add(u);
                    }

                    result.isSuccess = true;
                    result.data = usersList;
                    result.message = "User Found";
                }
                else
                {
                    result.isSuccess = false;
                    result.data = "";
                    result.message = "User Not Found";
                }
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.exceptionMessage = ex.Message;
            }
            return result;

        }

        // POST: api/Users
        [HttpPost]
        public resultset Register([FromBody] Users user)
        {
            resultset result = new resultset();
            result.data = user;
            try
            {
                connection();

                SqlCommand cmd = new SqlCommand("insertUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@phone", user.phone);
                cmd.Parameters.AddWithValue("@name", user.name);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@pass", user.pass);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                result.isSuccess = true;
                result.data = "";
                result.message = "Inserted Successfully.";
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.exceptionMessage = ex.Message;
            }
            return result;
        }

        // PUT: api/Users/5
        //[HttpPut("{id}")]
        //public resultset Put(int id, Users user)
        //{
        //    resultset result = new resultset();

        //    try
        //    {
        //        connection();

        //        SqlCommand cmd = new SqlCommand("updateUser", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@id", user.id);
        //        cmd.Parameters.AddWithValue("@name", user.name);
        //        cmd.Parameters.AddWithValue("@email", user.email);
        //        cmd.Parameters.AddWithValue("@phone", user.phone);
        //        cmd.Parameters.AddWithValue("@pass", user.pass);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();

        //        result.isSuccess = true;
        //        result.data = "";
        //        result.message = "Updated Successfully.";
        //    }
        //    catch (Exception ex)
        //    {
        //        result.isSuccess = false;
        //        result.exceptionMessage = ex.Message;
        //    }
        //    return result;
        //}

        // DELETE: api/ApiWithActions/5

        //[HttpDelete("{id}")]
        //public resultset Delete(int id)
        //{
        //    resultset result = new resultset();

        //    try
        //    {
        //        connection();

        //        SqlCommand cmd = new SqlCommand("deleteUser", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@id", id);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();

        //        result.isSuccess = true;
        //        result.data = "";
        //        result.message = "Deleted Successfully.";
        //    }
        //    catch (Exception ex)
        //    {
        //        result.isSuccess = false;
        //        result.exceptionMessage = ex.Message;
        //    }
        //    return result;
        //}

    }
}
