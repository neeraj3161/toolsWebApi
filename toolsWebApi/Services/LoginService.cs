using toolsWebApi.IServices;
using System.Configuration;
using Npgsql;
using toolsWebApi.Models;
using NHibernate;
using ISession = NHibernate.ISession;
using toolsWebApi.Entity;

namespace toolsWebApi.Services
{


    public class LoginService : IloginService


    {
        public bool UserLogin(string emailId, string pwd)
        {

            var isValidUser = GetUserInfo(emailId, pwd);

            return isValidUser;
        }

        private bool GetUserInfo(string email, string pwd)
        {
            var builder = WebApplication.CreateBuilder();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            string query = $"SELECT * FROM scrape.users WHERE email = '{email}' AND password = '{pwd}';";

            bool isUserPresent = false;


            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();

                using (NpgsqlTransaction tran = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand(query.ToString(), connection, tran))
                        {
                            using (NpgsqlDataReader dr = command.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    UserModel userData = new UserModel();
                                    isUserPresent = true;
                                    while (dr.Read())
                                    {
                                        userData.Id = (int)dr["id"];
                                        userData.Name = (string)dr["name"];
                                        userData.Surname = (string)dr["surname"];
                                        userData.Email = (string)dr["email"];
                                        userData.Created = (DateTime)dr["created"];




                                    }
                                }
                            }
                        }
                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            tran.Rollback();
                        }
                        catch (Exception)
                        { }
                    }

                }
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return isUserPresent;
        }

    }
}
