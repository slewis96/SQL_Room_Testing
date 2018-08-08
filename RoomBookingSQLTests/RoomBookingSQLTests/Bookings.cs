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
                Assert.IsType(Type.GetType("System.Int64"), entry);
            }
            conn.Close();
            Console.WriteLine("Bookings - ID is of type Int64 \n");
        }

        [Fact]
        public void CharReturnImportance()
        {
            Output.WriteLine("Bookings - Importance should be !, !! or !!! \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT importance FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            string[] importance = { "!", "!!", "!!!" };
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.Contains(entry, importance);
            }
            Console.WriteLine("Bookings - Importance is either !, !! or !!! \n");
        }
        [Fact]
        public void CategoryPredefined()
        {
            Output.WriteLine("Bookings - Category should be one of the predefined selections and type string \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT category FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            string[] precat = { "Face-to-Face Interview", "Phone Interview", "Mock Exams", "Exams", "Presentation Rehersal", "1-On-1", "Client Visit", "Management Meeting", "Sparta Day", "Other" };
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.Contains(entry, precat);
                Assert.IsType(Type.GetType("System.String"), entry);

            }
            conn.Close();
            Console.WriteLine("Bookings - Category is one of the predefined selections and type string \n");
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
            Console.WriteLine("Bookings - Day is an int and between 0 and 31 \n");
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
            Console.WriteLine("Bookings - Starttime is an int and under 24 \n");
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
            Console.WriteLine("Bookings - Endtime is an int and above 0 \n");
        }

        [Fact]
        public void DateReturnDate()
        {
            Output.WriteLine("Bookings - Date attribute should be of type Date \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT date FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.DateTime"), entry);
            }
            conn.Close();
            Console.WriteLine("Bookings - Date attribute is of type Date \n");
        }

        [Fact]
        public void IntAndAboveParticipants()
        {
            Output.WriteLine("Bookings - Participants should be an int and above 0 \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT participants FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.Int32"), entry);
                Assert.True(((int)entry > 0));
            }
            conn.Close();
            Console.WriteLine("Bookings - Participants is an int and above 0 \n");
        }

        [Fact]
        public void StatusIntAndPredefined()
        {
            Output.WriteLine("Bookings - Status should be one of the predefined selections and a string \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT status FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            string[] prestat = { "AVAILABLE", "PENDING", "BOOKED"};
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.Contains(entry, prestat);
                Assert.IsType(Type.GetType("System.String"), entry);
            }
            conn.Close();
            Console.WriteLine("Bookings - Status is one of the predefined selections and a string \n");
        }
    }
}
