using System;
using Xunit;
using Xunit.Abstractions;
using Npgsql;

namespace RoomBookingSQLTests
{
    public class Rooms
    {
        public NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres;" + "Password=;Database=bookingsystem_development;");
        protected ITestOutputHelper Output;
        public Rooms(ITestOutputHelper output)
        {
            Output = output;
        }
        [Fact]
        public void IntReturnID()
        {
            Output.WriteLine("Rooms - ID should be of type Int64 \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id FROM rooms", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.Int64"), entry);
            }
            conn.Close();
            Console.WriteLine("Rooms - ID is of type Int64 \n");
        }

        [Fact]
        public void StringReturnName()
        {
            Output.WriteLine("Rooms - Name should be of type string \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT room_name FROM rooms", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.String"), entry);
            }
            conn.Close();
            Console.WriteLine("Rooms - Name is of type string \n");
        }
    }
}
