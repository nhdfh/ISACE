using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ISACE
{
    class Administration
    {
        string connectionString ="Data Source = DB.db"; //"Server=DESKTOP-PIR66RC\"SQLEXPRESS;Database=ISAACCE;Trusted_Connection=True;";

        public void FillTestTypeInfo(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // string user = "SELECT * FROM [Users]";
                //string user = "SELECT u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataSet Prog_set = new DataSet();
                adapter.Fill(Prog_set);
                Program.f1.guna2DataGridView10.DataSource = Prog_set.Tables[0];
                //guna2DataGridView4.DataSource = user_set.Tables[0];
                Program.f1.guna2DataGridView10.Columns[0].Visible = false;

            }
        }
        public void FillPlaceInfo(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // string user = "SELECT * FROM [Users]";
                //string user = "SELECT u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataSet Prog_set = new DataSet();
                adapter.Fill(Prog_set);
                Program.f1.guna2DataGridView9.DataSource = Prog_set.Tables[0];
                //guna2DataGridView4.DataSource = user_set.Tables[0];
                Program.f1.guna2DataGridView9.Columns[0].Visible = false;

            }
        }
        public void FillComponentTypeInfo(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // string user = "SELECT * FROM [Users]";
                //string user = "SELECT u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataSet Prog_set = new DataSet();
                adapter.Fill(Prog_set);
                Program.f1.guna2DataGridView11.DataSource = Prog_set.Tables[0];
                //guna2DataGridView4.DataSource = user_set.Tables[0];
                Program.f1.guna2DataGridView11.Columns[0].Visible = false;

            }
        }
        public void FillReportTypeInfo(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // string user = "SELECT * FROM [Users]";
                //string user = "SELECT u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataSet Prog_set = new DataSet();
                adapter.Fill(Prog_set);
                Program.f1.guna2DataGridView12.DataSource = Prog_set.Tables[0];
                //guna2DataGridView4.DataSource = user_set.Tables[0];
                Program.f1.guna2DataGridView12.Columns[0].Visible = false;

            }
        }
        public void FillStatusInfo(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // string user = "SELECT * FROM [Users]";
                //string user = "SELECT u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataSet Prog_set = new DataSet();
                adapter.Fill(Prog_set);
                Program.f1.guna2DataGridView13.DataSource = Prog_set.Tables[0];
                //guna2DataGridView4.DataSource = user_set.Tables[0];
                Program.f1.guna2DataGridView13.Columns[0].Visible = false;

            }
        }
        public void FillItemTypeInfo(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // string user = "SELECT * FROM [Users]";
                //string user = "SELECT u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataSet Prog_set = new DataSet();
                adapter.Fill(Prog_set);
                Program.f1.guna2DataGridView14.DataSource = Prog_set.Tables[0];
                //guna2DataGridView4.DataSource = user_set.Tables[0];
                Program.f1.guna2DataGridView14.Columns[0].Visible = false;

            }
        }
        public void Query(string query)
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
