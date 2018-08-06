using System;
using Xunit;
using Xunit.Abstractions;
using Npgsql;

namespace RoomBookingSQLTests
{
    public class Bookings
    {
        public NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres;" + "Password=;Database=bookingsystem_development;");
        protected ITestOutputHelper Output;
        public Bookings(ITestOutputHelper output)
        {
            Output = output;
        }
        [Fact]
        public void IntReturnID()
        {
            Output.WriteLine("Bookings - ID should be of type Int64 \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.Int32"), entry);
            }
            conn.Close();
        }

        [Fact]
        public void CharReturnImportance()
        {
            Output.WriteLine("Bookings - Importance should be ! || !! || !!! \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT importance FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            string[] importance = { "!", "!!", "!!!" };
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.Contains(entry, importance);
            }
        }
        [Fact]
        public void CategoryPredefined()
        {
            Output.WriteLine("Bookings - Category should be one of the predefined selections \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT category FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            string[] precat = { "Face-to-Face Interview", "Phone Interview", "Mock Exams", "Exams", "Presentation Rehersal", "1-On-1", "Client Visit", "Management Meeting", "Sparta Day", "Other" };
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.Contains(entry, precat);
            }
            conn.Close();
        }
        [Fact]
        public void IntAndUnderDay()
        {
            Output.WriteLine("Bookings - Day should be an int and between 0 and 31 \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT day_id FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.Int32"), entry);
                Assert.True((int)entry > 0);
                Assert.True((int)entry <= 31);
            }
            conn.Close();
        }
        [Fact]
        public void IntAndUnderReturnStart()
        {
            Output.WriteLine("Bookings - Starttime should be an int and under 24 \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT starttime FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.Int32"), entry);
                Assert.True(((int)entry < 24));
            }
            conn.Close();
        }

        [Fact]
        public void IntAndAboveReturnEnd()
        {
            Output.WriteLine("Bookings - Endtime should be an int and above 0 \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT endtime FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.Int32"), entry);
                Assert.True(((int)entry > 0));
            }
            conn.Close();
        }
    }
}
