using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISACE
{
    public partial class ReportForm : System.Windows.Forms.Form
    {
        Report report = new Report();
        Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;

        public ReportForm()
        {
            Program.f10 = this;
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
                //Таблица.
                ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
                int n = 0;
                if (listBox1.SelectedItem.ToString() == "Компьютерная техника")
                    n = 1;
                if (listBox1.SelectedItem.ToString() == "Комплектующие")
                    n = 2;
                if (listBox1.SelectedItem.ToString() == "Программы")
                    n = 3;
                if (listBox1.SelectedItem.ToString() == "Обращения")
                    n = 4;
                if (listBox1.SelectedItem.ToString() == "Технические отчёты")
                    n = 5;
                if (listBox1.SelectedItem.ToString() == "Рекомендации")
                    n = 6;
                if (listBox1.SelectedItem.ToString() == "Пользователи")
                    n = 7;
                if (listBox1.SelectedItem.ToString() == "Тесты")
                    n = 8;
                if (listBox1.SelectedItem.ToString() == "Места размещения")
                    n = 9;
                if (listBox1.SelectedItem.ToString() == "Типы тестов")
                    n = 10;
                if (listBox1.SelectedItem.ToString() == "Типы комплектующих")
                    n = 11;
                if (listBox1.SelectedItem.ToString() == "Типы отчётов")
                    n = 12;
                if (listBox1.SelectedItem.ToString() == "Типы статусов")
                    n = 13;
                if (listBox1.SelectedItem.ToString() == "Типы техники")
                    n = 14;
                switch (n)// базовые выводы всей таблицы

                {
                    case 1:
                        report.Fill("SELECT e.equipment_id as 'ID', e.equipment_name as 'Наименование', t.type_name as 'Тип устройства', e.equipment_inventory_name as 'Инвентарный номер', e.equipment_webname as 'Сетевое имя', e.equipment_IP as 'IP - адрес', u.login as 'Пользователь', p.place_name as 'Место размещения', e.equipment_date as 'Дата поступления', s.status_name as 'Статус' FROM Computer_equipment as e INNER JOIN Type_equipment as t ON (e.equipment_type=t.type_id) LEFT JOIN Users as u ON (e.equipment_user=u.user_id) LEFT JOIN Place as p ON (e.equipment_place = p.place_id) INNER JOIN Status_equipment as s ON (e.equipment_status=s.status_id)");
                        // return;

                        break;
                    case 2:
                        report.Fill("SELECT c.item_id as 'ID', c.code_of_creator as 'Код производителя', c.item_name as 'Наименование', c.web_cpecification as 'Информация о продукте', c.instalation_date as 'Дата установки', t.item_type_name as 'Тип устройства', ce.equipment_name as 'Установлен в' FROM Item_computers as c INNER JOIN Item_type as t ON (c.item_type=t.item_type_id) LEFT JOIN Computer_equipment as ce ON (c.equipment_id=ce.equipment_id)");
                        break;
                    case 3:
                        report.Fill("SELECT p.programm_id as 'ID', p.programm_name as 'Наименование', p.programm_key as 'Ключ программы', p.programm_date as 'Дата установки', ce.equipment_name as 'Устройство' FROM Programm as p LEFT JOIN Computer_equipment as ce ON (p.equipment_id = ce.equipment_id)");

                        break;
                    case 4:
                        report.Fill("SELECT b.breackdowns_id as 'ID', b.description as 'Описание', b.breackdowns_date as 'Дата обращения', ce.equipment_name as 'Устройство' FROM Breackdowns as b LEFT JOIN Computer_equipment as ce ON (b.equipment_id = ce.equipment_id)");

                        break;
                    case 5:
                        report.Fill("SELECT r.reports_id as 'ID', r.discription as 'Описание', r.report_date as 'Дата', t.type_name as 'Тип отчёта', ce.equipment_name as 'Устройство' FROM Reports as r INNER JOIN Reports_type as t ON (r.report_type = t.reports_type_id) INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id) ");

                        break;
                    case 6:
                        report.Fill("SELECT r.recommendations_id as 'ID', r.description as 'Описание', r.recommendation_date as 'Дата', ce.equipment_name as 'Устройство' FROM Recommendations as r INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id)");

                        break;
                    case 7:
                        report.Fill("SELECT u.user_id as 'ID', u.login as 'Логин', u.user_fname as 'Имя', u.user_sname as 'Фамилия', u.user_mail as 'Почта', u.user_phone as 'Телефон', r.role_name as 'Роль' FROM Users as u  INNER JOIN Role as r ON (u.user_role_id=r.role_id) ");
                        break;
                    case 8:
                        report.Fill("SELECT t.test_id as 'ID', t.description as 'Описание', t.test_date as 'Дата', tt.test_name as 'Наименование теста', ce.equipment_name as 'Устройство' FROM Tests_equipment as t INNER JOIN Tests_type as tt ON (t.test_type = tt.tests_type_id) INNER JOIN Computer_equipment as ce ON (t.equipment_id = ce.equipment_id)  ");

                        break;
                    case 9:
                        report.Fill("SELECT p.place_id as 'ID', p.place_name as 'Наименование'   FROM Place as p");

                        break;
                    case 10:
                        report.Fill("SELECT tt.tests_type_id as 'ID', tt.test_name as 'Тип теста'   FROM Tests_type as tt");

                        break;
                    case 11:
                        report.Fill("SELECT c.item_type_id as 'ID', c.item_type_name as 'Наименование' FROM Item_type as c");

                        break;
                    case 12:
                        report.Fill("SELECT r.reports_type_id as 'ID', r.type_name as 'Наименование' FROM Reports_type as r");

                        break;
                    case 13:
                        report.Fill("SELECT s.status_id as 'ID', s.status_name as 'Наименование' FROM Status_equipment as s");

                        break;
                    case 14:
                        report.Fill("SELECT t.type_id as 'ID', t.type_name as 'Наименование' FROM Type_equipment as t");

                        break;


                    default:
                        break;
                }

                for (int j = 0; j < guna2DataGridView1.ColumnCount; j++)
                {
                    ExcelApp.Cells[1, j + 1] = guna2DataGridView1.Columns[j].HeaderText;
                }


                for (int i = 1; i < guna2DataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < guna2DataGridView1.ColumnCount; j++)
                    {
                        ExcelApp.Cells[i + 1, j + 1] = guna2DataGridView1.Rows[i - 1].Cells[j].Value;
                    }
                }

                //Вызываем нашу созданную эксельку.
                ExcelApp.Visible = true;
                ExcelApp.UserControl = true;
            }
            catch 
            {
                MessageBox.Show("Для использования экспорта необходимо установить MS Excel", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                
            }
            
        }
    }
}
