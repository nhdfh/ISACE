using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ISACE
{
    class Equipment_component
    {
        string connectionString = "Data Source = DB.db"; //"Server=DESKTOP-PIR66RC\"SQLEXPRESS;Database=ISAACCE;Trusted_Connection=True;";

        public void FillEquipComponentInfo(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // string user = "SELECT * FROM [Users]";
                //string user = "SELECT u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataSet EquipComp_set = new DataSet();
                adapter.Fill(EquipComp_set);
                Program.f1.guna2DataGridView2.DataSource = EquipComp_set.Tables[0];
                //guna2DataGridView4.DataSource = user_set.Tables[0];
                Program.f1.guna2DataGridView2.Columns[0].Visible = false;

            }
        }
        public void ItemIdList(List<Tuple<string, string>> itemList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string item = "SELECT * FROM [item_type]";
                SQLiteCommand command = new SQLiteCommand(item, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    itemList.Add(new Tuple<string, string>(reader[0].ToString(), reader[1].ToString()));
                }
                reader.Close();
                connection.Close();
            }

        }
        public void EquipIdList(List<Tuple<string, string>> equipList)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string item = "SELECT equipment_id, equipment_name FROM [Computer_equipment]";
                SQLiteCommand command = new SQLiteCommand(item, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    equipList.Add(new Tuple<string, string>(reader[0].ToString(), reader[1].ToString()));
                }
                reader.Close();
                connection.Close();
            }

        }
        public void CompItemQuery(string query)
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
