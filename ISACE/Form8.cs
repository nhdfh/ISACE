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
    public partial class Form8 : Form
    {
        List<Tuple<string, string>> equipList = new List<Tuple<string, string>>();
        Recommendations recommendations = new Recommendations();
        string recId;
        public Form8()
        {
            InitializeComponent();
            recommendations.EquipIdList(equipList);
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
                foreach (DataGridViewRow row in Program.f1.guna2DataGridView7.SelectedRows)
                {
                    recId = row.Cells[0].Value.ToString();
                    richTextBox1.Text = row.Cells[1].Value.ToString();
                    dateTimePicker1.Text = row.Cells[2].Value.ToString();
                    
                    listBox1.SelectedItem = row.Cells[3].Value.ToString();
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
                MessageBox.Show("Необходимо выбрать устройство", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]*$").Match(richTextBox1.Text.Replace("\n", "")).Success)
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

                        string insertRec = ("INSERT INTO Recommendations (description, recommendation_date, equipment_id) VALUES ('" + richTextBox1.Text.Replace("\n", "") + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + equipId + "')");
                        /// 
                        /// Проверки нужны на текст боксы
                        ///
                        recommendations.RecQuery(insertRec);
                    }


                    recommendations.FillRecInfo("SELECT r.recommendations_id as 'ID', r.description as 'Описание', r.recommendation_date as 'Дата', ce.equipment_name as 'Устройство' FROM Recommendations as r INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id)");


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
                    string updateRec = ("UPDATE Recommendations set description = '" + richTextBox1.Text.Replace("\n", "") + "',recommendation_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', equipment_id = '" + equipId + "' WHERE recommendations_id = '" + recId + "'");
                    recommendations.RecQuery(updateRec);


                    recommendations.FillRecInfo("SELECT r.recommendations_id as 'ID', r.description as 'Описание', r.recommendation_date as 'Дата', ce.equipment_name as 'Устройство' FROM Recommendations as r INNER JOIN Computer_equipment as ce ON (r.equipment_id = ce.equipment_id)");


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
