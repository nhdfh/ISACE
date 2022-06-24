using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ISACE
{
    class Equipment
    {
        string connectionString = "Data Source = DB.db"; //"Server=DESKTOP-PIR66RC\"SQLEXPRESS;Database=ISAACCE;Trusted_Connection=True;";
        public void FillEquipInfo(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // string user = "SELECT * FROM [Users]";
                //string user = "SELECT u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataSet Equip_set = new DataSet();
                adapter.Fill(Equip_set);
                Program.f1.guna2DataGridView1.DataSource = Equip_set.Tables[0];
                //guna2DataGridView4.DataSource = user_set.Tables[0];
                Program.f1.guna2DataGridView1.Columns[0].Visible = false;

            }
        }

        public void TypeIdList(List<Tuple<string, string>> typeList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string item = "SELECT * FROM [Type_equipment]";
                SQLiteCommand command = new SQLiteCommand(item, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    typeList.Add(new Tuple<string, string>(reader[0].ToString(), reader[1].ToString()));
                }
                reader.Close();
                connection.Close();
            }

        }
        public void UserIdList(List<Tuple<string, string>> userList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string item = "SELECT * FROM [Users]";
                SQLiteCommand command = new SQLiteCommand(item, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new Tuple<string, string>(reader[0].ToString(), reader[1].ToString()));
                }
                reader.Close();
                connection.Close();
            }

        }
        public void PlaceIdList(List<Tuple<string, string>> placeList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string item = "SELECT * FROM [Place]";
                SQLiteCommand command = new SQLiteCommand(item, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    placeList.Add(new Tuple<string, string>(reader[0].ToString(), reader[1].ToString()));
                }
                reader.Close();
                connection.Close();
            }

        }
        public void StatusIdList(List<Tuple<string, string>> statusList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string item = "SELECT * FROM [Status_equipment]";
                SQLiteCommand command = new SQLiteCommand(item, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    statusList.Add(new Tuple<string, string>(reader[0].ToString(), reader[1].ToString()));
                }
                reader.Close();
                connection.Close();
            }

        }
        public void equipNameList(List<Tuple<string>> equipNameList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string item = "SELECT equipment_name FROM [Computer_equipment]";
                SQLiteCommand command = new SQLiteCommand(item, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipNameList.Add(new Tuple<string>(reader[0].ToString()));
                }
                reader.Close();
                connection.Close();
            }

        }

        public void CompQuery(string query)
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
