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
    public partial class Form1 : Form
    {
        //string connectionString = "Server=DESKTOP-Q3OPSRO;Database=ISAACCE;Trusted_Connection=True;";
        
        string userId;
        string itemComponent;
        string breack;
        string progId;
        string reportId;
        string recId;
        string testId;
        string equipId;
        string testTypeId;
        string placeId;
        string equipTypeId;
        string reportTypeId;
        string statusId;
        string itemTypeID;
        
        UserInfo userInfo = new UserInfo();
        Equipment_component equipmentComponent = new Equipment_component();
        Breackdowns breackdowns = new Breackdowns();
        Prog prog = new Prog();
        Tech_report report = new Tech_report();
        Recommendations recommendations = new Recommendations();
        Tests tests = new Tests();
        Equipment equipment = new Equipment();
        Administration administration = new Administration();
        public Form1(string userRole)
        {
            
            
            Program.f1 = this;
            InitializeComponent();
            this.Text = "Информционная система учёта наличия и состояния компьютерной техники (" + userRole + ")";
            if (userRole=="admin")
            {

            }
            if (userRole == "manager")
            {
                guna2Button7.Visible = false;
                guna2Button6.Visible = false;
                guna2Button5.Visible = false;
            }
            if(userRole=="user")
            {
                TestType_Button.Visible = false;
                place_Button.Visible = false;
                guna2Button36.Visible = false;
                guna2Button37.Visible = false;
                guna2Button38.Visible = false;
                guna2Button34.Visible = false;
                guna2Button25.Visible = false;
                guna2Button24.Visible = false;
                guna2Button23.Visible = false;
                guna2Button7.Visible = false;
                guna2Button6.Visible = false;
                guna2Button5.Visible = false;
                guna2Button29.Visible = false;
                guna2Button28.Visible = false;
                guna2Button27.Visible = false;
                guna2Button33.Visible = false;
                guna2Button32.Visible = false;
                guna2Button31.Visible = false;
                guna2Button1.Visible = false;
                guna2Button9.Visible = false;
                guna2Button10.Visible = false;
                //guna2Button21.Visible = false;
                guna2Button20.Visible = false;
                guna2Button8.Visible = false;
                Report_gen_Button.Visible = false;
                guna2Button19.Visible = false;
                guna2Button17.Visible = false;
                guna2Button16.Visible = false;
                guna2Button15.Visible = false;
                guna2Button14.Visible = false;
                guna2Button13.Visible = false;
                guna2Button12.Visible = false;
            }
            tabControl1.SelectTab(Computer_Equ_tapPage);
            equipment.FillEquipInfo("SELECT e.equipment_id as 'ID', e.equipment_name as 'Наименование', t.type_name as 'Тип устройства', e.equipment_inventory_name as 'Инвентарный номер', e.equipment_webname as 'Сетевое имя', e.equipment_IP as 'IP - адрес', u.login as 'Пользователь', p.place_name as 'Место размещения', e.equipment_date as 'Дата поступления', s.status_name as 'Статус' FROM Computer_equipment as e LEFT JOIN Type_equipment as t ON (e.equipment_type=t.type_id) LEFT JOIN Users as u ON (e.equipment_user=u.user_id) LEFT JOIN Place as p ON (e.equipment_place = p.place_id) INNER JOIN Status_equipment as s ON (e.equipment_status=s.status_id)");

        }

        private void guna2Button8_Click(object sender, EventArgs e)//кнопка пользователи в меню
        {
            tabControl1.SelectTab(UserTabPage);
            string user = "SELECT u.user_id as 'ID', u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль', u.user_pass as 'Пароль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
            userInfo.FillUserInfo(user);
           
            
        }
        
        private void guna2Button6_Click(object sender, EventArgs e)//изменить пользователя
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView4.SelectedRows)
            {
                if (row.Cells[0].Value==null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                
            }
            Program.flag = false;
          
            Form2 f = new Form2();

            f.ShowDialog();
        }

        private void guna2Button7_Click(object sender, EventArgs e)//добавить пользователя
        {
            Program.flag = true;
            Form2 f = new Form2();
            
            
                f.ShowDialog();
            
            
        }

        private void guna2Button5_Click(object sender, EventArgs e)//удалить пользователя
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView4.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                userId = row.Cells[0].Value.ToString();
                
            }
            string delet = ("DELETE FROM Users where user_id = '" + userId + "'");
            userInfo.UserQuery(delet);
            string user = "SELECT u.user_id as 'ID', u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль', u.user_pass as 'Пароль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ";
            userInfo.FillUserInfo(user);

        }

        private void guna2Button11_Click(object sender, EventArgs e)//поиск компьютеров
        {
            if (guna2TextBox1.Text != "")
            {
                if (!Regex.Match(guna2TextBox1.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    equipment.FillEquipInfo("SELECT e.equipment_id as 'ID', e.equipment_name as 'Наименование', t.type_name as 'Тип устройства', e.equipment_inventory_name as 'Инвентарный номер', e.equipment_webname as 'Сетевое имя', e.equipment_IP as 'IP - адрес', u.login as 'Пользователь', p.place_name as 'Место размещения', e.equipment_date as 'Дата поступления', s.status_name as 'Статус' FROM Computer_equipment as e LEFT JOIN Type_equipment as t ON (e.equipment_type=t.type_id) LEFT JOIN Users as u ON (e.equipment_user=u.user_id) LEFT JOIN Place as p ON (e.equipment_place = p.place_id) INNER JOIN Status_equipment as s ON (e.equipment_status=s.status_id) where (e.equipment_name ='" + guna2TextBox1.Text + "' or t.type_name ='" + guna2TextBox1.Text + "' or e.equipment_inventory_name ='" + guna2TextBox1.Text + "' or u.login ='" + guna2TextBox1.Text + "' or p.place_name ='" + guna2TextBox1.Text + "' or s.status_name ='" + guna2TextBox1.Text + "')");

                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                equipment.FillEquipInfo("SELECT e.equipment_id as 'ID', e.equipment_name as 'Наименование', t.type_name as 'Тип устройства', e.equipment_inventory_name as 'Инвентарный номер', e.equipment_webname as 'Сетевое имя', e.equipment_IP as 'IP - адрес', u.login as 'Пользователь', p.place_name as 'Место размещения', e.equipment_date as 'Дата поступления', s.status_name as 'Статус' FROM Computer_equipment as e LEFT JOIN Type_equipment as t ON (e.equipment_type=t.type_id) LEFT JOIN Users as u ON (e.equipment_user=u.user_id) LEFT JOIN Place as p ON (e.equipment_place = p.place_id) INNER JOIN Status_equipment as s ON (e.equipment_status=s.status_id)");
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)//поиск пользователя
        {
            if(guna2TextBox4.Text!="")
                {
                if (!Regex.Match(guna2TextBox4.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    string user = "SELECT u.user_id as 'ID', u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль', u.user_pass as 'Пароль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) where (u.login='" + guna2TextBox4.Text + "' OR r.role_name = '" + guna2TextBox4.Text + "' )";
                    userInfo.FillUserInfo(user);
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                userInfo.FillUserInfo("SELECT u.user_id as 'ID', u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль', u.user_pass as 'Пароль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ");
            }
        }

        private void Computer_Components_Button_Click(object sender, EventArgs e)// вкладка комплектующих
        {
            tabControl1.SelectTab(Computer_Components_tabPage);
            string component = "SELECT c.item_id as 'ID', c.code_of_creator as 'Код производителя', c.item_name as 'Наименование', c.web_cpecification as 'Информация о продукте', c.instalation_date as 'Дата установки', t.item_type_name as 'Тип устройства', ce.equipment_name as 'Установлен в' FROM Item_computers as c INNER JOIN Item_type as t ON (c.item_type=t.item_type_id) LEFT JOIN Computer_equipment as ce ON (c.equipment_id=ce.equipment_id)";
            equipmentComponent.FillEquipComponentInfo(component);
        }

        private void guna2Button14_Click(object sender, EventArgs e)// добавить комплектующую
        {
            Program.flag = true;
            Form6 f = new Form6();
            if (Program.nool==true)
            {
                f.ShowDialog();
            }
            
        }

        private void guna2Button13_Click(object sender, EventArgs e)// изменить комплектующую
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView2.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form6 f = new Form6();

            f.ShowDialog();
        }

        private void guna2Button12_Click(object sender, EventArgs e)// удалить комплектующую
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView2.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                itemComponent = row.Cells[0].Value.ToString();

            }
            string delet = ("DELETE FROM item_computers where item_id = '" + itemComponent + "'");
            equipmentComponent.CompItemQuery(delet);
            string component = "SELECT c.item_id as 'ID', c.code_of_creator as 'Код производителя', c.item_name as 'Наименование', c.web_cpecification as 'Информация о продукте', c.instalation_date as 'Дата установки', t.item_type_name as 'Тип устройства', ce.equipment_name as 'Установлен в' FROM Item_computers as c INNER JOIN Item_type as t ON (c.item_type=t.item_type_id) LEFT JOIN Computer_equipment as ce ON (c.equipment_id=ce.equipment_id)";
            equipmentComponent.FillEquipComponentInfo(component);
        }

        private void Brackdown_Button_Click(object sender, EventArgs e) // выбор вкладки обращений
        {
            tabControl1.SelectTab(ReportTabPage);
            breackdowns.FillBreackdownsInfo("SELECT b.breackdowns_id as 'ID', b.description as 'Описание', b.breackdowns_date as 'Дата обращения', ce.equipment_name as 'Устройство' FROM Breackdowns as b LEFT JOIN Computer_equipment as ce ON (b.equipment_id = ce.equipment_id)");
        }

        private void guna2Button21_Click(object sender, EventArgs e)// добавить обращение
        {
            Program.flag = true;
            Form4 f = new Form4();
            if (Program.nool==true)
            {
                f.ShowDialog();
            }
            
        }

        private void guna2Button20_Click(object sender, EventArgs e)//изменить образение
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView5.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form4 f = new Form4();

            f.ShowDialog();
        }

        private void guna2Button19_Click(object sender, EventArgs e)//удалить обращение
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView5.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                breack = row.Cells[0].Value.ToString();

            }
            string delet = ("DELETE FROM Breackdowns where breackdowns_id = '" + breack + "'");
            breackdowns.BreackdownQuery(delet);
            breackdowns.FillBreackdownsInfo("SELECT b.breackdowns_id as 'ID', b.description as 'Описание', b.breackdowns_date as 'Дата обращения', ce.equipment_name as 'Устройство' FROM Breackdowns as b LEFT JOIN Computer_equipment as ce ON (b.equipment_id = ce.equipment_id)");

        }

        private void ProgrammButton_Click(object sender, EventArgs e)//выбор вкладки программ
        {
            tabControl1.SelectTab(Programm_tabPage);
            prog.FillProgInfo("SELECT p.programm_id as 'ID', p.programm_name as 'Наименование', p.programm_key as 'Ключ программы', p.programm_date as 'Дата установки', ce.equipment_name as 'Устройство' FROM Programm as p LEFT JOIN Computer_equipment as ce ON (p.equipment_id = ce.equipment_id)");

        }

        private void guna2Button17_Click(object sender, EventArgs e)//добавить программу
        {
            Program.flag = true;
            Form5 f = new Form5();
            if (Program.nool == true)
            {
                f.ShowDialog();
            }
            
        }

        private void guna2Button16_Click(object sender, EventArgs e)//изменить программу
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView3.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form5 f = new Form5();

            f.ShowDialog();
        }

        private void guna2Button15_Click(object sender, EventArgs e)//удалить программу
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView3.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                progId = row.Cells[0].Value.ToString();

            }
            string delet = ("DELETE FROM Programm where programmm_id = '" + progId + "'");
            prog.ProgQuery(delet);
            prog.FillProgInfo("SELECT p.programm_id as 'ID', p.programm_name as 'Наименование', p.programm_key as 'Ключ программы', p.programm_date as 'Дата установки', ce.equipment_name as 'Устройство' FROM Programm as p LEFT JOIN Computer_equipment as ce ON (p.equipment_id = ce.equipment_id)");

        }

        private void Tech_report_Button_Click(object sender, EventArgs e)//вкладка тех отчётов
        {
            tabControl1.SelectTab(TechReportTabPage);
            report.FillTechReportInfo("SELECT r.reports_id as 'ID', r.discription as 'Описание', r.report_date as 'Дата', t.type_name as 'Тип отчёта', ce.equipment_name as 'Устройство' FROM Reports as r INNER JOIN Reports_type as t ON (r.report_type = t.reports_type_id) INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id) ");
        }

        private void guna2Button25_Click(object sender, EventArgs e)//добавить тех отчёт
        {
            Program.flag = true;
            Form3 f = new Form3();
            if (Program.nool == true)
            {
                f.ShowDialog();
            }
            
        }

        private void guna2Button24_Click(object sender, EventArgs e)//изменить тех отчёт
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView6.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form3 f = new Form3();

            f.ShowDialog();
        }

        private void guna2Button23_Click(object sender, EventArgs e)//удалить тех отчёт
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView6.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                reportId = row.Cells[0].Value.ToString();

            }
            string delet = ("DELETE FROM Reports where reports_id = '" + reportId + "'");
            report.ReportQuery(delet);
            report.FillTechReportInfo("SELECT r.reports_id as 'ID', r.discription as 'Описание', r.report_date as 'Дата', t.type_name as 'Тип отчёта', ce.equipment_name as 'Устройство' FROM Reports as r INNER JOIN Reports_type as t ON (r.report_type = t.reports_type_id) INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id) ");

        }

        private void Recommendation_Button_Click(object sender, EventArgs e)//Вкладка рекомендации
        {
            tabControl1.SelectTab(RecommendationsTabPage);
            recommendations.FillRecInfo("SELECT r.recommendations_id as 'ID', r.description as 'Описание', r.recommendation_date as 'Дата', ce.equipment_name as 'Устройство' FROM Recommendations as r INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id)");
        }

        private void guna2Button29_Click(object sender, EventArgs e)//добавить рекомендацию
        {
            Program.flag = true;
            Form8 f = new Form8();
            if (Program.nool == true)
            {
                f.ShowDialog();
            }
            
        }

        private void guna2Button28_Click(object sender, EventArgs e)//изменить рекомендацию
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView7.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form8 f = new Form8();

            f.ShowDialog();
        }

        private void guna2Button27_Click(object sender, EventArgs e)//удалить рекомендацию
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView7.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                recId = row.Cells[0].Value.ToString();

            }
            string delet = ("DELETE FROM Recommendations where recommendations_id = '" + recId + "'");
            recommendations.RecQuery(delet);
            recommendations.FillRecInfo("SELECT r.recommendations_id as 'ID', r.description as 'Описание', r.recommendation_date as 'Дата', ce.equipment_name as 'Устройство' FROM Recommendations as r INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id)");

        }

        private void Tests_Button_Click(object sender, EventArgs e)//вкладка тесты
        {
            tabControl1.SelectTab(TestTabPage);
            tests.FillTestInfo("SELECT t.test_id as 'ID', t.description as 'Описание', t.test_date as 'Дата', tt.test_name as 'Наименование теста', ce.equipment_name as 'Устройство' FROM Tests_equipment as t INNER JOIN Tests_type as tt ON (t.test_type = tt.tests_type_id) INNER JOIN Computer_equipment as ce ON (t.equipment_id = ce.equipment_id)  ");
        }

        private void guna2Button33_Click(object sender, EventArgs e)// добавить тетст
        {
            Program.flag = true;
            Form9 f = new Form9();
            if (Program.nool == true)
            {
                f.ShowDialog();
            }
            
        }

        private void guna2Button32_Click(object sender, EventArgs e)//изменить тест
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView8.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form9 f = new Form9();

            f.ShowDialog();
        }

        private void guna2Button31_Click(object sender, EventArgs e)//удалить тест
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView8.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                testId = row.Cells[0].Value.ToString();

            }
            string delet = ("DELETE FROM Tests_equipment where test_id = '" + testId + "'");
            tests.TestQuery(delet);
            tests.FillTestInfo("SELECT t.test_id as 'ID', t.description as 'Описание', t.test_date as 'Дата', tt.test_name as 'Наименование теста', ce.equipment_name as 'Устройство' FROM Tests_equipment as t INNER JOIN Tests_type as tt ON (t.test_type = tt.tests_type_id) INNER JOIN Computer_equipment as ce ON (t.equipment_id = ce.equipment_id)  ");

        }

        private void Computer_Equ_Button_Click(object sender, EventArgs e)//вкладка компьютеры
        {
            tabControl1.SelectTab(Computer_Equ_tapPage);
            equipment.FillEquipInfo("SELECT e.equipment_id as 'ID', e.equipment_name as 'Наименование', t.type_name as 'Тип устройства', e.equipment_inventory_name as 'Инвентарный номер', e.equipment_webname as 'Сетевое имя', e.equipment_IP as 'IP - адрес', u.login as 'Пользователь', p.place_name as 'Место размещения', e.equipment_date as 'Дата поступления', s.status_name as 'Статус' FROM Computer_equipment as e LEFT JOIN Type_equipment as t ON (e.equipment_type=t.type_id) LEFT JOIN Users as u ON (e.equipment_user=u.user_id) LEFT JOIN Place as p ON (e.equipment_place = p.place_id) INNER JOIN Status_equipment as s ON (e.equipment_status=s.status_id)");
        }

        private void guna2Button1_Click(object sender, EventArgs e)//добавить комп
        {
            Program.flag = true;
            Form7 f = new Form7();
            if (Program.nool == true)
            {
                f.ShowDialog();
            }
            
        }

        private void guna2Button9_Click(object sender, EventArgs e)//изменить комп
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView1.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form7 f = new Form7();

            f.ShowDialog();
        }

        private void guna2Button10_Click(object sender, EventArgs e)//удалить комп
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView1.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                equipId = row.Cells[0].Value.ToString();

            }
            string delet = ("DELETE FROM Computer_equipment where equipment_id = '" + equipId + "'");
            equipment.CompQuery(delet);
            equipment.FillEquipInfo("SELECT e.equipment_id as 'ID', e.equipment_name as 'Наименование', t.type_name as 'Тип устройства', e.equipment_inventory_name as 'Инвентарный номер', e.equipment_webname as 'Сетевое имя', e.equipment_IP as 'IP - адрес', u.login as 'Пользователь', p.place_name as 'Место размещения', e.equipment_date as 'Дата поступления', s.status_name as 'Статус' FROM Computer_equipment as e LEFT JOIN Type_equipment as t ON (e.equipment_type=t.type_id) LEFT JOIN Users as u ON (e.equipment_user=u.user_id) LEFT JOIN Place as p ON (e.equipment_place = p.place_id) INNER JOIN Status_equipment as s ON (e.equipment_status=s.status_id)");

        }

        private void Report_gen_Button_Click(object sender, EventArgs e)//генерация отчетов
        {
            Form10 f = new Form10();
            f.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)// поиск компьютерных компонентов
        {
            if (guna2TextBox2.Text != "")
            {
                if (!Regex.Match(guna2TextBox2.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    equipmentComponent.FillEquipComponentInfo("SELECT c.item_id as 'ID', c.code_of_creator as 'Код производителя', c.item_name as 'Наименование', c.web_cpecification as 'Информация о продукте', c.instalation_date as 'Дата установки', t.item_type_name as 'Тип устройства', ce.equipment_name as 'Установлен в' FROM Item_computers as c INNER JOIN Item_type as t ON (c.item_type=t.item_type_id) LEFT JOIN Computer_equipment as ce ON (c.equipment_id=ce.equipment_id) where (t.item_type_name ='" + guna2TextBox2.Text + "' or c.item_name ='" + guna2TextBox2.Text + "' or ce.equipment_name ='" + guna2TextBox2.Text + "')");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                equipmentComponent.FillEquipComponentInfo("SELECT c.item_id as 'ID', c.code_of_creator as 'Код производителя', c.item_name as 'Наименование', c.web_cpecification as 'Информация о продукте', c.instalation_date as 'Дата установки', t.item_type_name as 'Тип устройства', ce.equipment_name as 'Установлен в' FROM Item_computers as c INNER JOIN Item_type as t ON (c.item_type=t.item_type_id) LEFT JOIN Computer_equipment as ce ON (c.equipment_id=ce.equipment_id)");
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)// поиск установлденных программ
        {
            if (guna2TextBox3.Text != "")
            {
                if (!Regex.Match(guna2TextBox3.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    prog.FillProgInfo("SELECT p.programm_id as 'ID', p.programm_name as 'Наименование', p.programm_key as 'Ключ программы', p.programm_date as 'Дата установки', ce.equipment_name as 'Устройство' FROM Programm as p LEFT JOIN Computer_equipment as ce ON (p.equipment_id = ce.equipment_id) where (p.programm_name ='" + guna2TextBox3.Text + "' or ce.equipment_name ='" + guna2TextBox3.Text + "')");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                prog.FillProgInfo("SELECT p.programm_id as 'ID', p.programm_name as 'Наименование', p.programm_key as 'Ключ программы', p.programm_date as 'Дата установки', ce.equipment_name as 'Устройство' FROM Programm as p LEFT JOIN Computer_equipment as ce ON (p.equipment_id = ce.equipment_id)");
            }
        }

        private void guna2Button18_Click(object sender, EventArgs e)//поиск обращений
        {
            if (guna2TextBox5.Text != "")
            {
                if (!Regex.Match(guna2TextBox5.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    breackdowns.FillBreackdownsInfo("SELECT b.breackdowns_id as 'ID', b.description as 'Описание', b.breackdowns_date as 'Дата обращения', ce.equipment_name as 'Устройство' FROM Breackdowns as b LEFT JOIN Computer_equipment as ce ON (b.equipment_id = ce.equipment_id) where(b.breackdowns_date='" + guna2TextBox5.Text + "' or ce.equipment_name ='" + guna2TextBox5.Text + "')");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                breackdowns.FillBreackdownsInfo("SELECT b.breackdowns_id as 'ID', b.description as 'Описание', b.breackdowns_date as 'Дата обращения', ce.equipment_name as 'Устройство' FROM Breackdowns as b LEFT JOIN Computer_equipment as ce ON (b.equipment_id = ce.equipment_id)");
            }
        }

        private void guna2Button22_Click(object sender, EventArgs e)//поиск технических отчётов
        {
            if (guna2TextBox6.Text != "")
            {
                if (!Regex.Match(guna2TextBox6.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    report.FillTechReportInfo("SELECT r.reports_id as 'ID', r.discription as 'Описание', r.report_date as 'Дата', t.type_name as 'Тип отчёта', ce.equipment_name as 'Устройство' FROM Reports as r INNER JOIN Reports_type as t ON (r.report_type = t.reports_type_id) INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id) where (ce.equipment_name ='" + guna2TextBox6.Text + "' or t.type_name ='" + guna2TextBox6.Text + "')");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                report.FillTechReportInfo("SELECT r.reports_id as 'ID', r.discription as 'Описание', r.report_date as 'Дата', t.type_name as 'Тип отчёта', ce.equipment_name as 'Устройство' FROM Reports as r INNER JOIN Reports_type as t ON (r.report_type = t.reports_type_id) INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id) ");
            }
        }

        private void guna2Button26_Click(object sender, EventArgs e)//поиск рекомендации
        {
            if (guna2TextBox7.Text != "")
            {
                if (!Regex.Match(guna2TextBox7.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    recommendations.FillRecInfo("SELECT r.recommendations_id as 'ID', r.description as 'Описание', r.recommendation_date as 'Дата', ce.equipment_name as 'Устройство' FROM Recommendations as r INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id) where (ce.equipment_name ='" + guna2TextBox7.Text + "')");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                recommendations.FillRecInfo("SELECT r.recommendations_id as 'ID', r.description as 'Описание', r.recommendation_date as 'Дата', ce.equipment_name as 'Устройство' FROM Recommendations as r INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id)");
            }
        }

        private void guna2Button30_Click(object sender, EventArgs e)//поиск тестов
        {
            if (guna2TextBox8.Text != "")
            {
                if (!Regex.Match(guna2TextBox8.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    tests.FillTestInfo("SELECT t.test_id as 'ID', t.description as 'Описание', t.test_date as 'Дата', tt.test_name as 'Наименование теста', ce.equipment_name as 'Устройство' FROM Tests_equipment as t INNER JOIN Tests_type as tt ON (t.test_type = tt.tests_type_id) INNER JOIN Computer_equipment as ce ON (t.equipment_id = ce.equipment_id)  where ( tt.test_name ='" + guna2TextBox8.Text + "' or ce.equipment_name ='" + guna2TextBox8.Text + "')");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                tests.FillTestInfo("SELECT t.test_id as 'ID', t.description as 'Описание', t.test_date as 'Дата', tt.test_name as 'Наименование теста', ce.equipment_name as 'Устройство' FROM Tests_equipment as t INNER JOIN Tests_type as tt ON (t.test_type = tt.tests_type_id) INNER JOIN Computer_equipment as ce ON (t.equipment_id = ce.equipment_id)  ");
            }
        }

        private void TestType_Button_Click(object sender, EventArgs e)//вкладка типы тестов
        {
            tabControl1.SelectTab(testTupeTabPage);
            administration.FillTestTypeInfo("SELECT tt.tests_type_id as 'ID', tt.test_name as 'Тип теста'   FROM Tests_type as tt");
        }

        private void guna2Button45_Click(object sender, EventArgs e)// добавить тип теста
        {
            Program.flag = true;
            Form12 f = new Form12();
            
            
                f.ShowDialog();
            
            
        }

        private void guna2Button44_Click(object sender, EventArgs e)// изменить тип теста
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView10.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form12 f = new Form12();
            f.ShowDialog();
        }

        private void guna2Button43_Click(object sender, EventArgs e)// удалить тип теста
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView10.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                testTypeId = row.Cells[0].Value.ToString();
            }
            administration.Query("DELETE FROM Tests_type where tests_type_id = '"+ testTypeId +"'");
            administration.FillTestTypeInfo("SELECT tt.tests_type_id as 'ID', tt.test_name as 'Тип теста'   FROM Tests_type as tt");

        }

        private void guna2Button42_Click(object sender, EventArgs e)//поиск типа теста
        {
            if (guna2TextBox10.Text != "")
            {
                if (!Regex.Match(guna2TextBox10.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    administration.FillTestTypeInfo("SELECT tt.tests_type_id as 'ID', tt.test_name as 'Тип теста'   FROM Tests_type as tt where tt.test_name = '"+guna2TextBox10.Text+"'");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                administration.FillTestTypeInfo("SELECT tt.tests_type_id as 'ID', tt.test_name as 'Тип теста'   FROM Tests_type as tt");
            }
        }

        private void guna2Button41_Click(object sender, EventArgs e)//добавить место размещения
        {
            Program.flag = true;
            Form13 f = new Form13();
            
            
                f.ShowDialog();
            
            
        }

        private void guna2Button40_Click(object sender, EventArgs e)//изменить место размещения
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView9.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form13 f = new Form13();
            f.ShowDialog();
        }

        private void guna2Button39_Click(object sender, EventArgs e)//удалить место размещения
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView9.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                placeId = row.Cells[0].Value.ToString();
            }
            administration.Query("DELETE FROM Place where place_id = '" + placeId + "'");
            administration.FillPlaceInfo("SELECT p.place_id as 'ID', p.place_name as 'Наименование'   FROM Place as p");

        }

        private void guna2Button35_Click(object sender, EventArgs e)//поиск место размещения
        {
            if (guna2TextBox9.Text != "")
            {
                if (!Regex.Match(guna2TextBox9.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    administration.FillPlaceInfo("SELECT p.place_id as 'ID', p.place_name as 'Наименование'   FROM Place as p where p.place_name = '" + guna2TextBox9.Text + "'");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                administration.FillPlaceInfo("SELECT p.place_id as 'ID', p.place_name as 'Наименование'   FROM Place as p");
            }
        }

        private void place_Button_Click(object sender, EventArgs e)// вкладка метса размещения
        {
            tabControl1.SelectTab(placeTabPage);
            administration.FillPlaceInfo("SELECT p.place_id as 'ID', p.place_name as 'Наименование'   FROM Place as p");
        }

        private void guna2Button36_Click(object sender, EventArgs e)// вкладка типы комплектующих
        {
            tabControl1.SelectTab(componentTypeTabPage);
            administration.FillComponentTypeInfo("SELECT c.item_type_id as 'ID', c.item_type_name as 'Наименование' FROM Item_type as c");
        }

        private void guna2Button49_Click(object sender, EventArgs e)//добавить типы комплектующих
        {
            Program.flag = true;
            Form14 f = new Form14();
           
                f.ShowDialog();
            
            
        }

        private void guna2Button48_Click(object sender, EventArgs e)//изменить типы комплектующих
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView11.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form14 f = new Form14();
            f.ShowDialog();
        }

        private void guna2Button47_Click(object sender, EventArgs e)//удалить типы комплектующих
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView11.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                equipTypeId = row.Cells[0].Value.ToString();
            }
            administration.Query("DELETE FROM Item_type where item_type_id = '" + equipTypeId + "'");
            administration.FillComponentTypeInfo("SELECT c.item_type_id as 'ID', c.item_type_name as 'Наименование' FROM Item_type as c");
        }

        private void guna2Button46_Click(object sender, EventArgs e)//поиск типы комплектующих
        {
            if (guna2TextBox11.Text != "")
            {
                if (!Regex.Match(guna2TextBox11.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    administration.FillComponentTypeInfo("SELECT c.item_type_id as 'ID', c.item_type_name as 'Наименование' FROM Item_type as c where c.item_type_name = '" + guna2TextBox11.Text + "'");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                administration.FillComponentTypeInfo("SELECT c.item_type_id as 'ID', c.item_type_name as 'Наименование' FROM Item_type as c");
            }
        }

        private void guna2Button37_Click(object sender, EventArgs e)// вкладка типы отчётов
        {

            tabControl1.SelectTab(ReportTypeTabPage);
            administration.FillReportTypeInfo("SELECT r.reports_type_id as 'ID', r.type_name as 'Наименование' FROM Reports_type as r");
        }

        private void guna2Button53_Click(object sender, EventArgs e)// добавить типы отчётов
        {
            Program.flag = true;
            Form15 f = new Form15();
            
            
                f.ShowDialog();
            
            
        }

        private void guna2Button52_Click(object sender, EventArgs e)// изменить типы отчётов
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView12.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form15 f = new Form15();
            f.ShowDialog();
        }

        private void guna2Button51_Click(object sender, EventArgs e)// удалить типы отчётов
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView12.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                reportTypeId = row.Cells[0].Value.ToString();
            }
            administration.Query("DELETE FROM Reports_type where reports_type_id = '" + reportTypeId + "'");
            administration.FillReportTypeInfo("SELECT r.reports_type_id as 'ID', r.type_name as 'Наименование' FROM Reports_type as r");

        }

        private void guna2Button50_Click(object sender, EventArgs e)// Поиск типы отчётов
        {
            if (guna2TextBox12.Text != "")
            {
                if (!Regex.Match(guna2TextBox12.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    administration.FillReportTypeInfo("SELECT r.reports_type_id as 'ID', r.type_name as 'Наименование' FROM Reports_type as r where r.type_name = '" + guna2TextBox12.Text + "'");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                administration.FillReportTypeInfo("SELECT r.reports_type_id as 'ID', r.type_name as 'Наименование' FROM Reports_type as r");
            }
        }

        private void guna2Button38_Click(object sender, EventArgs e)//вкладка статусов техники
        {
            tabControl1.SelectTab(StatusTabPage);
            administration.FillStatusInfo("SELECT s.status_id as 'ID', s.status_name as 'Наименование' FROM Status_equipment as s");
        }

        private void guna2Button57_Click(object sender, EventArgs e)//добавить статусов техники
        {
            Program.flag = true;
            Form16 f = new Form16();
            
            
                f.ShowDialog();
            
            
        }

        private void guna2Button56_Click(object sender, EventArgs e)//изменить статусов техники
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView12.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form16 f = new Form16();
            f.ShowDialog();
        }

        private void guna2Button55_Click(object sender, EventArgs e)//удалить статусов техники
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView13.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                statusId = row.Cells[0].Value.ToString();
            }
            administration.Query("DELETE FROM Status_equipment where status_id = '" + statusId + "'");
            administration.FillStatusInfo("SELECT s.status_id as 'ID', s.status_name as 'Наименование' FROM Status_equipment as s");
        }

        private void guna2Button54_Click(object sender, EventArgs e)//поиск statusa
        {
            if (guna2TextBox13.Text != "")
            {
                if (!Regex.Match(guna2TextBox13.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    administration.FillStatusInfo("SELECT s.status_id as 'ID', s.status_name as 'Наименование' FROM Status_equipment as s where s.status_name = '" + guna2TextBox13.Text + "'");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                administration.FillStatusInfo("SELECT s.status_id as 'ID', s.status_name as 'Наименование' FROM Status_equipment as s");
            }
        }

        private void guna2Button58_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2Button34_Click(object sender, EventArgs e)//вкладка типов техники
        {
            tabControl1.SelectTab(typeItemTabPage);
            administration.FillItemTypeInfo("SELECT t.type_id as 'ID', t.type_name as 'Наименование' FROM Type_equipment as t");
        }

        private void guna2Button62_Click(object sender, EventArgs e)//добавить
        {
            Program.flag = true;
            Form17 f = new Form17();
            
            
                f.ShowDialog();
            
        }

        private void guna2Button61_Click(object sender, EventArgs e)//изменить
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView14.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }
            Program.flag = false;
            Form17 f = new Form17();
            f.ShowDialog();
        }

        private void guna2Button60_Click(object sender, EventArgs e)//удалить
        {
            foreach (DataGridViewRow row in Program.f1.guna2DataGridView14.SelectedRows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("Выбранные данные недоступны для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                itemTypeID = row.Cells[0].Value.ToString();
            }
            administration.Query("DELETE FROM Type_equipment where type_id = '" + itemTypeID + "'");
            administration.FillItemTypeInfo("SELECT t.type_id as 'ID', t.type_name as 'Наименование' FROM Type_equipment as t");
        }

        private void guna2Button59_Click(object sender, EventArgs e)//поиск
        {
            if (guna2TextBox14.Text != "")
            {
                if (!Regex.Match(guna2TextBox14.Text, @"\(^[A - Z, a - z, а - я, А - Я, 0 - 9_ -]{ 3, 49 }$").Success)
                {
                    administration.FillItemTypeInfo("SELECT t.type_id as 'ID', t.type_name as 'Наименование' FROM Type_equipment as t where t.type_name = '" + guna2TextBox14.Text + "'");
                }
                else
                {
                    MessageBox.Show("Попытка ввода некорректных символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                administration.FillItemTypeInfo("SELECT t.type_id as 'ID', t.type_name as 'Наименование' FROM Type_equipment as t");

            }
        }
    }
}
