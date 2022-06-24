using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ISACE
{
    class UserInfo
    {
        string connectionString = "Data Source = DB.db"; // "Server=DESKTOP-PIR66RC\"SQLEXPRESS;Database=ISAACCE;Trusted_Connection=True;";



        public void FillUserInfo(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // string user = "SELECT * FROM [Users]";
                //string user = "SELECT u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataSet user_set = new DataSet();
                adapter.Fill(user_set);
                Program.f1.guna2DataGridView4.DataSource = user_set.Tables[0];
                //guna2DataGridView4.DataSource = user_set.Tables[0];
                Program.f1.guna2DataGridView4.Columns[0].Visible = false;
                Program.f1.guna2DataGridView4.Columns[7].Visible = false;
            }
        }

        public void UserRoleList(List<Tuple<string, string>> roleList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string role = "SELECT * FROM [Role]";
                SQLiteCommand command = new SQLiteCommand(role, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    roleList.Add(new Tuple<string, string>(reader[0].ToString(), reader[1].ToString()));
                }
                reader.Close();
                connection.Close();
            }
           
        }

        public void UserInfoList(List<Tuple<string,string,string,string,string,string>> userList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string role = "SELECT u.user_id,u.login,u.user_mail,u.user_phone,u.user_pass,r.role_name FROM [Users] as u INNER JOIN  Role as r on (u.user_role_id = r.role_id)";
                SQLiteCommand command = new SQLiteCommand(role, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new Tuple<string, string, string, string, string,string>(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                }
                reader.Close();
                connection.Close();
            }

        }

        public void UserQuery(string query)
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //string insertUser = ("INSERT INTO Users (login,user_fname,user_sname,user_mail,user_phone,user_role_id,user_pass) VALUES ('" + LoginTextBox.Text + "','" + FNameTextBox.Text + "','" + SNameTextBox.Text + "' , '" + EmailTextBox.Text + "','" + PhoneTextBox.Text + "','" + roleid + "','" + PassTextBox1.Text + "')");
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();

            }
        }

       
    }
}
