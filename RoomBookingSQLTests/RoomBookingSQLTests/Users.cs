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
            Output.WriteLine("Users - ID should be of type Int64 \n");
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

        [Fact]
        public void StringAndValidationEmail()
        {
            Output.WriteLine("Users - Email should be have contain '@' and '.' \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT email FROM users", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var entry = dr[0];
                Assert.True(entry.ToString().Contains('@'));
                Assert.True(entry.ToString().Contains('.'));
            }
            Console.WriteLine("Users - Email contains '@' and '.' \n");
        }

        [Fact]
        public void LengthValidationPassword()
        {
            Output.WriteLine("Users - Password should be 60 chars in length \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT encrypted_password FROM users", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var entry = dr[0];
                int ps = entry.ToString().Length;
                Assert.Equal(60, ps);
            }
            Console.WriteLine("Users - Password is 60 chars in length \n");
        }
        [Fact]
        public void StringReturnName()
        {
            Output.WriteLine("Users - Name should be of type string \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT name FROM users", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.String"), entry);
            }
            conn.Close();
            Console.WriteLine("Users - Name is of type string \n");
        }
    }
}
