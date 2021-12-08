using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.Repositories.Database
{
    public class UserRepository: IUserRepository
    {
        private readonly IADOOperations _ado;
        private readonly SqlConnection _conn;
        private readonly IConfiguration _config;

        public UserRepository(IADOOperations aDOOperations, IConfiguration config)
        {
            _ado = aDOOperations;
            _conn = new SqlConnection(config.GetSection("ConnectionStrings:Default").Value);
            _config = config;
        }


        public async Task<bool> Add<T>(T entity)
        {
            int response;
            var user = entity as User;
            string stmt = $"INSERT INTO [user] (firstname, lastname, email, passwordhash, passwordsalt, userid) VALUES ('{user.Firstname}', '{user.Lastname}', '{user.Email}', '{user.PasswordHash}', '{user.PasswordSalt}', '{user.Id}')";

           
            try
            {
                _conn.Open();

                await using (var cmd = new SqlCommand(stmt, _conn))
                {
                  
                    response = cmd.ExecuteNonQuery();
                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
            
            return response == 1? true : false;
        }

        public async Task<List<User>> GetUsers()
        {
            string _cmd = "SELECT * FROM [user]";
            if (_conn == null)
                throw new Exception("Connection not established!");

            var listOfUsers = new List<User>();

            try
            {
                using(var cmd = new SqlCommand(_cmd, _conn))
                {
                    _conn.Open();
                    var res =  cmd.ExecuteReader();

                    while (res.HasRows)
                    {
                        while (res.Read())
                        {
                            listOfUsers.Add(new User
                            {
                                Id = res["id"].ToString(),
                                Lastname = res["Lastname"].ToString(),
                                Firstname = res["Firstname"].ToString(),
                                Email = res["email"].ToString()
                            });

                        }
                        await res.NextResultAsync();
                    }
                }
                return listOfUsers;
            } catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conn.Close();
            }

        }

       

        public async Task<int> DeleteUser(string userid)
        {
            var stmt = "DeleteUser FROM [user] WHERE userid = @userid";
            var response = 0;

            try
            {
                _conn.Open();

                await using (var cmd = new SqlCommand(stmt, _conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userid);
                    response = (int)cmd.ExecuteNonQuery();

                }
                return response;
            }
            
            catch (Exception ex)
            {
                throw;
            }
           
            finally
            {
                _conn.Close();
            }


        }



    }
}
