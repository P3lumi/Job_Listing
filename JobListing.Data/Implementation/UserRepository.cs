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

        public async Task<List<User>> GetUsers(string email)
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



        public async Task<bool> Delete<T>(T entity)
        {
            var stmt = "DELETE FROM [user] WHERE id = @userid";
            var response = 0;

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

            return response == 1 ? true : false;


        }



        public async Task<List<User>> GetUserByEmail(string email)
        {
            
            string stmt = $"SELECT * FROM [user] WHERE email = @useremail";
                if (_conn == null)
                throw new Exception("Connection not established!");

            var listOfUsers = new List<User>();

            try
            {
                using (var cmd = new SqlCommand(stmt, _conn))
                {
                    _conn.Open();
                    var res = cmd.ExecuteReader();

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
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conn.Close();
            }


        }


        public async Task<List<User>> GetUserById(string id)
        {
            string stmt = $"SELECT * FROM [user] WHERE id = @userid";
            if (_conn == null)
                throw new Exception("Connection not established!");

            var listOfUsers = new List<User>();

            try
            {
                using (var cmd = new SqlCommand(stmt, _conn))
                {
                    _conn.Open();
                    var res = cmd.ExecuteReader();

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
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }


        public async Task<bool> Edit<T>(T entity)
        {

            int response;
            var user = entity as User;
            string stmt = $"UPDATE [user] SET id = '{user.Id}', lastName = '{user.Lastname}', " +
                $"firstName = '{user.Firstname}', email = '{user.Email}'" +
                $"WHERE id = '{user.Id}' OR email = '{user.Email}'";

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

            return response == 1 ? true : false;
        }

        public Task<bool> Add<T>(T entity, string role = "role")
        {
            throw new NotImplementedException();
        }
    }
}
