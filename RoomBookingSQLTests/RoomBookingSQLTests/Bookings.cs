using System;
using Xunit;
using Xunit.Abstractions;
using Npgsql;
using System.Collections.Generic;

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

        [Fact]
        public void ExistsRoomID()
        {
            Output.WriteLine("Bookings - room_id should exist in the Rooms table \n");
            conn.Open();
            NpgsqlCommand cmdR = new NpgsqlCommand("SELECT id FROM rooms", conn);
            NpgsqlDataReader drR = cmdR.ExecuteReader();
            var roomList = new List<Int64>();
            while (drR.Read())
            {
                var entry = Convert.ToInt64(drR[0]);
                roomList.Add(entry);
            }
            conn.Close();
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT room_id FROM bookings ORDER BY status DESC LIMIT 20", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = Convert.ToInt64(dr[0]);
                Assert.Contains(entry, roomList);
            }
            conn.Close();
            Console.WriteLine("Bookings - room_id exists in the Rooms table \n");
        }
        [Fact]
        public void ExistsEmail()
        {
            Output.WriteLine("Bookings - Email for booked rooms should exist \n");
            conn.Open();
            NpgsqlCommand cmdU = new NpgsqlCommand("SELECT email FROM users", conn);
            NpgsqlDataReader drU = cmdU.ExecuteReader();
            var emailList = new List<string>();
            while (drU.Read())
            {
                var entry = drU[0];
                emailList.Add(entry.ToString());
            }
            conn.Close();
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT email FROM bookings WHERE status = 'BOOKED'", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.Contains(entry, emailList);
            }
            conn.Close();
            Console.WriteLine("Bookings - Email for booked rooms exist \n");
        }
        [Fact]
        public void EmailEmpty()
        {
            Output.WriteLine("Bookings - Email for unbooked rooms should equal \"Email\" \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT email FROM bookings WHERE status = 'AVAILABLE' LIMIT 10", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            var emailList = new List<string>();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.Equal("Email", entry);
            }
            conn.Close();
        }
        [Fact]
        public void ExistsUserID()
        {
            Output.WriteLine("Bookings - id should exist in the users table and be of type Int64 \n");
            conn.Open();
            NpgsqlCommand cmdR = new NpgsqlCommand("SELECT id FROM users", conn);
            NpgsqlDataReader drR = cmdR.ExecuteReader();
            var userList = new List<Int64>();
            while (drR.Read())
            {
                var entry = Convert.ToInt64(drR[0]);
                userList.Add(entry);
            }
            conn.Close();
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT user_id FROM bookings ORDER BY status DESC LIMIT 10", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = Convert.ToInt64(dr[0]);
                Assert.Contains(entry, userList);
                Assert.IsType(Type.GetType("System.Int64"), entry);
            }
            conn.Close();
            Console.WriteLine("Bookings - id exists in the Rooms table and is of type Int64 \n");
        }
        [Fact]
        public void UserIDEmpty()
        {
            Output.WriteLine("Bookings - User ID for unbooked rooms should be blank \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT user_id FROM bookings WHERE status = 'AVAILABLE' LIMIT 10", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.Equal("", entry.ToString());
            }
            conn.Close();
            Console.WriteLine("Bookings - User ID for unbooked rooms is blank \n");
        }
        [Fact]
        public void NameEmpty()
        {
            Output.WriteLine("Bookings - Name for unbooked rooms should be blank \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT name FROM bookings LIMIT 10", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.Equal("", entry.ToString());
            }
            conn.Close();
            Console.WriteLine("Bookings - Name for unbooked rooms is blank \n");
        }
        [Fact]
        public void StringReturnNote()
        {
            Output.WriteLine("Bookings - Note should be of type string \n");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT note FROM bookings WHERE status = 'BOOKED' LIMIT 10", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var entry = dr[0];
                Assert.IsType(Type.GetType("System.String"), entry);
            }
            conn.Close();
            Console.WriteLine("Bookings - Note is of type string \n");
        }
    }
}
