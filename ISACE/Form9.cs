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
    public partial class Form9 : Form
    {
        List<Tuple<string, string>> testList = new List<Tuple<string, string>>();
        List<Tuple<string, string>> equipList = new List<Tuple<string, string>>();
        Tests tests = new Tests();
        string testId;
        public Form9()
        {
            InitializeComponent();
            tests.TestIdList(testList);
            tests.EquipIdList(equipList);
            //кнопка добавить
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
                if (testList.Count == 0)
                {
                    MessageBox.Show("Необходимо сначало добавить тип теста в систему", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.nool = false;
                }
                else
                {
                    Program.nool = true;
                }
                foreach (var item in testList)
                {
                    listBox1.Items.Add(item.Item2);
                }
                foreach (var item in equipList)
                {
                    listBox2.Items.Add(item.Item2);
                }
            }
            //кнопка изменить
            if (Program.flag == false)
            {
                foreach (var item in testList)
                {
                    listBox1.Items.Add(item.Item2);
                }
                foreach (var item in equipList)
                {
                    listBox2.Items.Add(item.Item2);
                }
                foreach (DataGridViewRow row in Program.f1.guna2DataGridView8.SelectedRows)
                {
                    testId = row.Cells[0].Value.ToString();
                    richTextBox1.Text = row.Cells[1].Value.ToString();
                    dateTimePicker1.Text = row.Cells[2].Value.ToString();

                    
                    listBox1.SelectedItem = row.Cells[3].Value.ToString();
                   
                    listBox2.SelectedItem = row.Cells[4].Value.ToString();
                    // лист с компьютерами куда установлен
                    //foreach (var item in collection)
                    //{

                    //}

                }

            }
        }
        private bool check()
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("Описание не должно быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать тип теста", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (listBox2.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать устройство", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]*$").Match(richTextBox1.Text.Replace("\n","")).Success) 
            {

            }
            else
            {
                MessageBox.Show("Некорректное описание", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (check() == true)
            {

                //если было добавление
                if (Program.flag == true)
                {
                    string testTypeId = "";
                    foreach (var item in testList)
                    {
                        if (item.Item2 == listBox1.SelectedItem.ToString())
                        {
                            testTypeId = item.Item1.ToString();
                            break;
                        }
                    }
                    string equipId = null;

                    foreach (var item in equipList)
                    {
                        if (item.Item2 == listBox2.SelectedItem.ToString())
                        {
                            equipId = item.Item1.ToString();
                            break;
                        }
                    }



                    /// 
                    /// Проверки нужны на текст боксы
                    ///

                    //if (check == true)


                    // string insertComp = ("INSERT INTO Item_computers (item_name,code_of_creator,web_cpecification,instalation_date,item_type,equipment_id) VALUES ('" + nameEquipmentComponentTextBox.Text + "','" + codeCreatorTextBox.Text + "','" + WebSpecificTextBox.Text + "' , '" + ComponentDateTimePicker.Value + "','" + itemId + "','" + equipmentId + "')");
                    string insertTest = ("INSERT INTO Tests_equipment (description, test_date, test_type, equipment_id) VALUES ('" + richTextBox1.Text.Replace("\n", "") + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + testTypeId + "','" + equipId + "')");
                    /// 
                    /// Проверки нужны на текст боксы
                    ///
                    tests.TestQuery(insertTest);






                    tests.FillTestInfo("SELECT t.test_id as 'ID', t.description as 'Описание', t.test_date as 'Дата', tt.test_name as 'Наименование теста', ce.equipment_name as 'Устройство' FROM Tests_equipment as t INNER JOIN Tests_type as tt ON (t.test_type = tt.tests_type_id) INNER JOIN Computer_equipment as ce ON (t.equipment_id = ce.equipment_id)  ");


                    Close();
                }
                // если изменение
                if (Program.flag == false)
                {
                    string testTypeId = "";


                    foreach (var item in testList)
                    {
                        if (item.Item2 == listBox1.SelectedItem.ToString())
                        {
                            testTypeId = item.Item1.ToString();
                            break;
                        }
                    }

                    string equipId = null;


                    foreach (var item in equipList)
                    {
                        if (item.Item2 == listBox2.SelectedItem.ToString())
                        {
                            equipId = item.Item1.ToString();
                            break;
                        }
                    }



                    string updateTest = ("UPDATE Tests_equipment set description = '" + richTextBox1.Text.Replace("\n", "") + "', test_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', test_type = '" + testTypeId + "', equipment_id ='" + equipId + "' WHERE test_id = '" + testId + "'");
                    tests.TestQuery(updateTest);



                    tests.FillTestInfo("SELECT t.test_id as 'ID', t.description as 'Описание', t.test_date as 'Дата', tt.test_name as 'Наименование теста', ce.equipment_name as 'Устройство' FROM Tests_equipment as t INNER JOIN Tests_type as tt ON (t.test_type = tt.tests_type_id) INNER JOIN Computer_equipment as ce ON (t.equipment_id = ce.equipment_id)  ");


                    Close();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
