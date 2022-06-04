using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KenKata.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace KenKata.WebApp.Tests
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString =
            @"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\DevOpsAzure\\NewKenKataSolution\\ShoppingCart_KenKata_Db.mdf;Integrated Security=True;Connect Timeout=30";

        public TestDatabaseFixture()
        {
            
        }
        public SqlContext CreateContext()
            => new SqlContext(
                new DbContextOptionsBuilder<SqlContext>()
                    .UseSqlServer(ConnectionString)
                    .Options);

    }
}
