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
    public partial class Form5 : Form
    {
        List<Tuple<string, string>> equipList = new List<Tuple<string, string>>();
        Prog prog = new Prog();
        string progID;
        
        public Form5()
        {
            InitializeComponent();
            prog.EquipIdList(equipList);
            if (Program.flag == true)
            {

                if (equipList.Count == 0)
                {
                    MessageBox.Show("Необходимо сначало добавить устройства в систему", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.nool = false;
                }
                else
                {
                    Program.nool = true;
                }
                foreach (var item in equipList)
                {
                    listBox1.Items.Add(item.Item2);
                }
            }
            //кнопка изменить
            if (Program.flag == false)
            {
                foreach (var item in equipList)
                {
                    listBox1.Items.Add(item.Item2);
                }
                foreach (DataGridViewRow row in Program.f1.guna2DataGridView3.SelectedRows)
                {
                    progID = row.Cells[0].Value.ToString();
                    guna2TextBox1.Text = row.Cells[1].Value.ToString();
                    guna2TextBox2.Text = row.Cells[2].Value.ToString();
                    dateTimePicker1.Text = row.Cells[3].Value.ToString();
                    
                    listBox1.SelectedItem = row.Cells[4].Value.ToString();
                   

                }

            }
        }

        private bool check()
        {
            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("Наименование не должно быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            

            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать устройство", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]*$").Match(guna2TextBox1.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректное наименование", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]*$").Match(guna2TextBox2.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректный код программы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (check() == true)
            {
                //bool check = false;
                //если было добавление

                if (Program.flag == true)
                {
                    string equipId = null;
                    if (listBox1.SelectedItem != null)
                    {
                        foreach (var item in equipList)
                        {
                            if (item.Item2 == listBox1.SelectedItem.ToString())
                            {
                                equipId = item.Item1.ToString();
                                break;
                            }
                        }
                    }


                    /// 
                    /// Проверки нужны на текст боксы
                    ///

                    //if (check == true)
                    {

                        string insertProg = ("INSERT INTO Programm (programm_name, programm_key, programm_date, equipment_id) VALUES ('" + guna2TextBox1.Text + "','"+ guna2TextBox2.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + equipId + "')");
                        /// 
                        /// Проверки нужны на текст боксы
                        ///
                        prog.ProgQuery(insertProg);
                    }


                    prog.FillProgInfo("SELECT p.programm_id as 'ID', p.programm_name as 'Наименование', p.programm_key as 'Ключ программы', p.programm_date as 'Дата установки', ce.equipment_name as 'Устройство' FROM Programm as p LEFT JOIN Computer_equipment as ce ON (p.equipment_id = ce.equipment_id)");


                    Close();
                }
                // если изменение
                if (Program.flag == false)
                {
                    string equipId = null;


                    if (listBox1.SelectedItem != null)
                    {
                        foreach (var item in equipList)
                        {
                            if (item.Item2 == listBox1.SelectedItem.ToString())
                            {
                                equipId = item.Item1.ToString();
                                break;
                            }
                        }
                    }
                    string updateProg = ("UPDATE Programm set programm_name = '" + guna2TextBox1.Text + "',programm_key='" + guna2TextBox2.Text + "',programm_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', equipment_id = '" + equipId + "' WHERE programm_id = '" + progID + "'");
                    prog.ProgQuery(updateProg);


                    prog.FillProgInfo("SELECT p.programm_id as 'ID', p.programm_name as 'Наименование', p.programm_key as 'Ключ программы', p.programm_date as 'Дата установки', ce.equipment_name as 'Устройство' FROM Programm as p LEFT JOIN Computer_equipment as ce ON (p.equipment_id = ce.equipment_id)");


                    Close();

                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
