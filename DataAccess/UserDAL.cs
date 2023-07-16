using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T12V2.DTO;

namespace T12V2.DataAccess
{
    internal class UserDAL
    {
        SqlConnection conn;
        public UserDAL()
        {
            conn = new DbConnector().GetConnection();
            conn.Open();
        }


        

        public User? SelectByUsernameAndPassword(string username, string password)
        {
            User us = null;
            string sql = "SELECT * FROM EMPL WHERE username = @0 AND password = @1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", username);
            cmd.Parameters.AddWithValue("1", password);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                us = new User();
                us.Id = (int)reader["id"];
                us.Username = (string)reader["username"];
                us.Fullname = (string)reader["fullname"];
                us.Role = (UserRole)reader["role"];
            }
            reader.Close();
            return us;
        }

        public User? SelectById(int id)
        {
            User user = null;
            string sql = "SELECT * FROM EMPL WHERE id = @0";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", id);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user = new User();
                user.Id = (int)reader["id"];
                user.Username = (string)reader["username"];
                user.Fullname = (string)reader["fullname"];
                user.Role = (UserRole)reader["role"];
            }
            reader.Close();
            return user;
        }

        public List<User> SelectByKey(string key)
        {
            List<User> list = new List<User>();
            string sql = "SELECT * FROM EMPL WHERE username like @0 OR fullname like @1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("0", key);
            cmd.Parameters.AddWithValue("1", key);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                User emp = new User();
                emp.Id = (int)reader["id"];
                emp.Username = (string)reader["username"];
                emp.Fullname = (string)reader["fullname"];
                emp.Role = (UserRole)reader["role"];
                list.Add(emp);
            }
            reader.Close();
            return list;
        }

        public List<User> SelectAll()
        {
            List<User> list = new List<User>();
            string sql = "SELECT id, username, fullname, role FROM EMPL";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                User user = new User();
                user.Id = (int)reader["id"];
                user.Username = (string)reader["username"];
                user.Fullname = (string)reader["fullname"];
                user.Role = (UserRole)reader["role"];
                list.Add(user);
            }
            reader.Close();
            return list;
        }
        public int Insert(User user)
        {
            string sql = "INSERT INTO  EMPL(username,fullname,password,role) VALUES (@0,@1,@2,@3)";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("0", user.Username);
            cmd.Parameters.AddWithValue("1", user.Fullname);
            cmd.Parameters.AddWithValue("2", user.Password);
            cmd.Parameters.AddWithValue("3", user.Role);
            return cmd.ExecuteNonQuery();
        }
        public int Update(User user, string newPass)
        {
            string sql = "UPDATE EMPL SET PASSWORD = @1 WHERE id=@0";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@0", user.Id);
            command.Parameters.AddWithValue("@1", newPass);
            return command.ExecuteNonQuery();
        }
        public int Update(User user, string field, string newString)
        {
            string sql = $"UPDATE EMPL SET {field} = @1 WHERE id=@0";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@0", user.Id);
            command.Parameters.AddWithValue("@1", newString);
            return command.ExecuteNonQuery();
        }
        public int Delete(int id)
        {
            string sql = "DELETE FROM EMPL WHERE id=@0";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@0", id);
            return command.ExecuteNonQuery();
        }
        public void Close()
        {
            conn.Close();
        }
    }
}
