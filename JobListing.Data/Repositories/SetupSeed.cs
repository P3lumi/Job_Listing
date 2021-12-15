using JobListing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobListing.Data.Repositories.Database
{
    public class SetupSeed
    {

        // check if the database already exist else create
        // check if the table already exist else create
        // check if table is not already seeded else seed it

        public static async Task SeedMe(IADOOperations adooperations)
        {

            string stmt1 = @"
                        Create Table user(
                            userid nvarchar (255) PRIMARY KEY,
                            lastname nvarchar(10) NOT NULL,
                            firstname nvarchar(10) NOT NULL,
                            email nvarchar(255) NOT NULL,
                            passwordhash varbinary(max) NOT NULL,
                            passwordsalt varbinary(max) NOT NULL
                        );

                        Create Table roles(
                            RoleId nvarchar (255) PRIMARY KEY,
                            RoleName nvarchar(10) NOT NULL
                        );";




            var hashes = Util.HashGenerator("12345");
            var passHash = "0x" + String.Join("", hashes[0].Select(n => n.ToString("X2")));
            var passSalt = "0x" + String.Join("", hashes[1].Select(n => n.ToString("X2")));

            string stmt2 = String.Format("INSERT INTO [user] (id, lastName, firstName, email, passwordHash, passwordSalt)" +
                        $"VALUES('1', 'John', 'Doe', 'john@doe.com', {passHash}, {passSalt});");


            stmt2 += "INSERT INTO [roles] (id, RoleName) VALUES('1', 'Admin'), ('2', 'Regular');";




            string stmt3 = @"
                        Create Table job(
                            JobId nvarchar (255) PRIMARY KEY,
                            JobTitle nvarchar(20) NOT NULL,
                            SalaryRange nvarchar(255) NOT NULL,
                            CategoryId nvarchar(255) NOT NULL,
                            Category nvarchar (20) NOT NULL,
                            IndustryId nvarchar(255) NOT NULL,
                            Industry nvarchar (20) NOT NULL
                        );";


                string stmt4 = String.Format("INSERT INTO [job] (JobId, JobTitle, SalaryRange, CategoryId, Category, IndustryId, Industry)" +
                                 $"VALUES('1', 'Software Engineer', '$8,000-$10,000', 'Cat1', 'Remote', 'Ind1', 'Technology');");

            try
            {
                await adooperations.ExecuteForTransactionQuery(stmt1, stmt2);
                await adooperations.ExecuteForTransactionQuery(stmt3, stmt4);
            }
            catch (Exception ex)
            {
                // log to file
                Console.WriteLine(ex.Message);
            }
        }




    }
}
