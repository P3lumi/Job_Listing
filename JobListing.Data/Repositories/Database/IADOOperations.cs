using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.Repositories.Database
{
    public interface IADOOperations
    {
        Task<bool> ExecuteForQuery(string stmt); // Create, Insert, Update, Delete

        Task<bool> ExecuteForTransactionQuery(string stmt, string stmt2);

        Task<List<ExecuterReaderResult>> ExecuteForReader(string stmt, params string[] fields);
    }
}
