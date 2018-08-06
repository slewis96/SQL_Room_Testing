using System;
using Xunit;
using Npgsql;
using System.IO;

namespace RoomBookingSQLTests
{
    public class Bookings
    {
        public NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1; User Id = postgres;" + "Password=;Database=bookingsystem_development;");

        [Fact]
        public void IntReturnID()
        {
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT id FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.Int64"), entry);
            }
            conn.Close();
        }

        [Fact]
        public void CharReturnImportance()
        {
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
        [Microsoft.VisualStudio.RoomBookingSQLTests.Bookings.Description("Testing that the day_id is of type integer and greater than 0.")]
        public void IntAndUnderDay()
        {
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT day_id FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.Int32"), entry);
                Assert.True((int)entry > 0);
            }
            conn.Close();
        }
    }
}
