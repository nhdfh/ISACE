using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISACE
{
    public partial class LoginForm : System.Windows.Forms.Form
    {
        List<Tuple<string, string>> roleList = new List<Tuple<string, string>>();
        UserInfo userInfo = new UserInfo();
        List<Tuple<string, string, string, string, string,string>> userList = new List<Tuple<string, string, string, string, string,string>>();
        string userId;
        bool login = false;
        
        public LoginForm()
        {
            userInfo.UserRoleList(roleList);
            userInfo.UserInfoList(userList);
            Program.adpass = false;
            InitializeComponent();
            foreach (var item in roleList)
            {
                RoleListBox.Items.Add(item.Item2);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)//вход
        {
            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9]{4,49}$").Match(guna2TextBox1.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректный логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (new Regex(@"^[a-zA-Z0-9]{3,49}$").Match(guna2TextBox2.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var item in userList)
            {
                if (item.Item2.ToString() == guna2TextBox1.Text && item.Item5.ToString() == guna2TextBox2.Text)
                {
                    //переход в основную форму
                    login = true;
                    MainForm form = new MainForm(item.Item6.ToString());
                    this.Hide();
                    form.ShowDialog();
                    this.Close();

                }
                
            }
            if (login == false)
            {
                MessageBox.Show("Некорректные данные для входа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
            if (new Regex(@"^[a-zA-Z0-9]{3,49}$").Match(PassTextBox2.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



            if (PassTextBox1.Text != PassTextBox2.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            foreach (var item in userList)
            {
                if (item.Item2.ToString() == LoginTextBox.Text || item.Item3.ToString() == EmailTextBox.Text || item.Item4.ToString() == PhoneTextBox.Text)
                {
                    MessageBox.Show("Данные уже используются в системе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        private void guna2Button2_Click(object sender, EventArgs e)//регистрация
        {
            tabControl1.SelectTab(tabPage2);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (check() == true)
            {
                //если было добавление

                Program.adpass = false;
                string roleId = "";
                if (RoleListBox.SelectedItem.ToString() == "admin")
                {
                    AdminPassForm form18 = new AdminPassForm();
                    form18.ShowDialog();
                }
                if (RoleListBox.SelectedItem.ToString() != "admin")
                {
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


                    userInfo.UserInfoList(userList);

                    tabControl1.SelectTab(tabPage1);
                }
                if (Program.adpass == true)
                {


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


                    userInfo.UserInfoList(userList);

                    tabControl1.SelectTab(tabPage1);
                }
            }
        }

        private void оПрограммеToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage1);
        }
    }
}
