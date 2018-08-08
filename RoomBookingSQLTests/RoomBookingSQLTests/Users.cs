using System;
using Xunit;
using Xunit.Abstractions;
using Npgsql;

namespace RoomBookingSQLTests
{
    public class Users
    {
        public NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres;" + "Password=;Database=bookingsystem_development;");
        protected ITestOutputHelper Output;
        public Users(ITestOutputHelper output)
        {
            Output = output;
        }
        [Fact]
        public void IntReturnID()
        {
            Output.WriteLine("Users    - ID should be of type Int64 \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id FROM users", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.Int64"), entry);
            }
            conn.Close();
            Console.WriteLine("Users - ID is of type Int64 \n");
        }
    }
}
