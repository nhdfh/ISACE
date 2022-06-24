using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ISACE
{
    class Report
    {
        string connectionString = "Data Source = DB.db"; //"Server=DESKTOP-PIR66RC\"SQLEXPRESS;Database=ISAACCE;Trusted_Connection=True;";

        public void Fill(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // string user = "SELECT * FROM [Users]";
                //string user = "SELECT u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataSet Set = new DataSet();
                adapter.Fill(Set);
                Program.f10.guna2DataGridView1.DataSource = Set.Tables[0];
                //guna2DataGridView4.DataSource = user_set.Tables[0];


            }
        }


    }
}
