using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ISACE
{
    public partial class Form2 : Form
    {
        //string connectionString = "Server=DESKTOP-Q3OPSRO;Database=ISAACCE;Trusted_Connection=True;";
        List<Tuple<string, string>> roleList = new List<Tuple<string, string>>();
        UserInfo userInfo = new UserInfo();
        List<Tuple<string, string, string, string, string,string>> userList = new List<Tuple<string, string, string, string, string,string>>();
        string userId;
        public Form2()
        {
            InitializeComponent();
            Form1 main = this.Owner as Form1;
            
            userInfo.UserRoleList(roleList);
            userInfo.UserInfoList(userList);
            
           //кнопка добавить
            if (Program.flag == true)
            {
                guna2TextBox1.Visible = false;
                foreach (var item in roleList)
                {
                    RoleListBox.Items.Add(item.Item2);
                }
            }
            //кнопка изменить
            if(Program.flag == false)
            {
                guna2TextBox1.Visible = true;
                foreach (var item in roleList)
                {
                    RoleListBox.Items.Add(item.Item2);
                }
                foreach (DataGridViewRow row in Program.f1.guna2DataGridView4.SelectedRows)
                {
                    userId = row.Cells[0].Value.ToString();
                    LoginTextBox.Text = row.Cells[1].Value.ToString();
                    FNameTextBox.Text = row.Cells[2].Value.ToString();
                    SNameTextBox.Text = row.Cells[3].Value.ToString();
                    EmailTextBox.Text = row.Cells[4].Value.ToString();
                    PhoneTextBox.Text = row.Cells[5].Value.ToString();
                    
                    // вывод всех ролей и селект по велью
                    //RoleListBox.SelectedItem.Equals(row.Cells[6].Value.ToString());
                    PassTextBox1.Text = row.Cells[7].Value.ToString();
                    PassTextBox2.Text = row.Cells[7].Value.ToString();
                    // RoleListBox.Items.Add(row.Cells[5].Value.ToString());

                    RoleListBox.SelectedItem = (row.Cells[6].Value.ToString());
                }
               
            }

        }
        private bool check()
        {


      
        
            if (new Regex(@"^(\+1|1|\+3|3|\+7|7|8)+([0-9]){10,12}$").Match(PhoneTextBox.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректный номер телефона", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9]{4,49}$").Match(LoginTextBox.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректный логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]{4,49}$").Match(FNameTextBox.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректное имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]{4,49}$").Match(SNameTextBox.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректная фамилия", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (new Regex(@"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$").Match(EmailTextBox.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректная почта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (new Regex(@"^[a-zA-Z0-9]{3,49}$").Match(PassTextBox1.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (new Regex(@"^[a-zA-Z0-9]{3,49}$").Match(PassTextBox2.Text). Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            


            if (PassTextBox1.Text != PassTextBox2.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                
            }
            //if (LoginTextBox.Text.Length >= 49 || FNameTextBox.Text.Length >= 49 || SNameTextBox.Text.Length >= 49 || EmailTextBox.Text.Length >= 49 || PhoneTextBox.Text.Length >= 49 || PassTextBox1.Text.Length >= 49)
            //{
            //    MessageBox.Show("Слишком длинное сообщение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            //if (LoginTextBox.Text.Length <= 4 || FNameTextBox.Text.Length <= 4 || SNameTextBox.Text.Length <= 4 || EmailTextBox.Text.Length <= 4 || PhoneTextBox.Text.Length <= 4 || PassTextBox1.Text.Length <= 4)
            //{
            //    MessageBox.Show("Слишком короткое сообщение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            if (LoginTextBox.Text == "" || FNameTextBox.Text == "" || SNameTextBox.Text == "" || EmailTextBox.Text == "" || PhoneTextBox.Text == "" || PassTextBox1.Text == "")
            {
                MessageBox.Show("Поля не должны быть пустыми", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (RoleListBox.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать роль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (Program.flag == true)
            {
                foreach (var item in userList)
                {
                    if (item.Item2.ToString() == LoginTextBox.Text || item.Item3.ToString() == EmailTextBox.Text || item.Item4.ToString() == PhoneTextBox.Text)
                    {
                        MessageBox.Show("Данные уже используются в системе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            if (Program.flag == false)
            {
                foreach (var item in userList)
                {
                    if (item.Item1!=userId)
                    {
                        if (item.Item2.ToString() == LoginTextBox.Text || item.Item3.ToString() == EmailTextBox.Text || item.Item4.ToString() == PhoneTextBox.Text)
                        {
                            MessageBox.Show("Данные уже используются в системе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    
                }
            }

            return true;
        }
        // кнопка сохранить
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (check() == true)
            {
                //bool check = false;
                //если было добавление
                if (Program.flag == true)
                {
                    string roleId = "";
                    foreach (var item in roleList)
                    {
                        if (item.Item2 == RoleListBox.SelectedItem.ToString())
                        {
                            roleId = item.Item1.ToString();
                            break;
                        }
                    }


                    /// 
                    /// Проверки нужны на текст боксы
                    ///

                    //if (check == true)
                    {

                        string insertUser = ("INSERT INTO Users (login,user_fname,user_sname,user_mail,user_phone,user_role_id,user_pass) VALUES ('" + LoginTextBox.Text + "','" + FNameTextBox.Text + "','" + SNameTextBox.Text + "' , '" + EmailTextBox.Text + "','" + PhoneTextBox.Text + "','" + roleId + "','" + PassTextBox1.Text + "')");
                        /// 
                        /// Проверки нужны на текст боксы
                        ///
                        userInfo.UserQuery(insertUser);
                    }


                    string user = "SELECT u.user_id as 'ID', u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль', u.user_pass as 'Пароль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";

                    userInfo.FillUserInfo(user);

                    Close();
                }
                // если изменение
                if (Program.flag == false)
                {
                    if (guna2TextBox1.Text != LoginTextBox.Text)
                    {
                        MessageBox.Show("Данные не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string roleId = "";
                    foreach (var item in roleList)
                    {
                        if (item.Item2 == RoleListBox.SelectedItem.ToString())
                        {
                            roleId = item.Item1.ToString();
                            break;
                        }
                    }
                    string updateUser = ("UPDATE Users set login = '" + LoginTextBox.Text + "',user_fname = '" + FNameTextBox.Text + "',user_sname = '" + SNameTextBox.Text + "', user_mail='" + EmailTextBox.Text + "',user_phone = '" + PhoneTextBox.Text + "',user_role_id = '" + roleId + "',user_pass = '" + PassTextBox1.Text + "' WHERE user_id = '" + userId.ToString() + "'");
                    userInfo.UserQuery(updateUser);


                    string user = "SELECT u.user_id as 'ID', u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль', u.user_pass as 'Пароль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";

                    userInfo.FillUserInfo(user);

                    Close();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
            Close();
        }

        private void PhoneTextBox_TextChanged(object sender, EventArgs e)
        {
          
        }
    }
}
