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
    public class ADOOperation : IADOOperations
    {
        private readonly string _conStr;
        private readonly SqlConnection _conn;
        
        public ADOOperation(IConfiguration configuration)
        {
            _conStr = configuration.GetSection("ConnectionStrings:Default").Value;
            try
            {
                _conn = new SqlConnection(_conStr);
            }catch (DbException dbExc)
            {
                throw new Exception(dbExc.Message);
            }
        }


         public async Task<bool> ExecuteForQuery(string stmt)
        {
            if (_conn == null)
                throw new Exception("Connection not established!");

            var resStatus = 0;

            try
            {
                _conn.Open();

                using (var cmd = new SqlCommand(stmt, _conn))
                {
                    resStatus =  await cmd.ExecuteNonQueryAsync();

                    if (resStatus < 1)
                        return false;
                }

            }
            catch (DbException dbEx)
            {
                throw new Exception(dbEx.Message);
            }
            finally
            {
                _conn.Close();
            }

            return true;
        }

        public async Task<List<ExecuterReaderResult>> ExecuteForReader(string stmt, params string[] fields)
        {
            if (_conn == null)
                throw new Exception("Connection not established!");

            var listOfRows = new List<ExecuterReaderResult>();
            var row = new ExecuterReaderResult();

            try
            {
                using (var cmd = new SqlCommand(stmt, _conn))
                {
                    _conn.Open();
                    var res = await cmd.ExecuteReaderAsync();

                    while (res.HasRows)
                    {
                        while (res.Read())
                        {
                            foreach (var field in fields)
                            {
                                row.Fields.Add(field);
                                row.Values.Add(res[field].ToString());
                            }
                            listOfRows.Add(row);                          
                            
                        }

                        await res.NextResultAsync();
                    }
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _conn.Close();
            }

            return listOfRows;
        }

        public async Task<bool> ExecuteForTransactionQuery(string stmt, string stmt2)
        {
            if (_conn == null)
                throw new Exception("Connection not established!");

            bool transState = false;

            SqlTransaction trans = null;
            try
            {
                _conn.Open();
                trans = _conn.BeginTransaction();
                using(var cmd = new SqlCommand(stmt, _conn))
                {
                    cmd.Transaction = trans;

                    // execute my first statment
                    await cmd.ExecuteNonQueryAsync();

                    // execute second query
                    cmd.CommandText = stmt2;
                    await cmd.ExecuteNonQueryAsync();

                    trans.Commit();
                    transState = true;
                }

            }
            catch (DbException ex)
            {
                await trans.RollbackAsync();
                throw new Exception(ex.Message);
            }
            finally
            {
                trans.Dispose();
                _conn.Close();
            }

            return transState;
        }


    }
}
