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
    public partial class Form6 : Form
    {
        List<Tuple<string, string>> itemList = new List<Tuple<string, string>>();
        List<Tuple<string, string>> equipList = new List<Tuple<string, string>>();
        string componentId;

        Equipment_component equipmentComponent = new Equipment_component();
        public Form6()
        {
            InitializeComponent();
            equipmentComponent.ItemIdList(itemList);
            equipmentComponent.EquipIdList(equipList);
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
                if (itemList.Count == 0)
                {
                    MessageBox.Show("Необходимо сначало добавить тип комплектующих в систему", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.nool = false;
                }
                else
                {
                    Program.nool = true;
                }
                foreach (var item in itemList)
                {
                    itemTypeListBox.Items.Add(item.Item2);
                }
                foreach (var item in equipList)
                {
                    EquipmentListBox.Items.Add(item.Item2);
                }
            }
            //кнопка изменить
            if (Program.flag == false)
            {
                foreach (var item in itemList)
                {
                    itemTypeListBox.Items.Add(item.Item2);
                }
                foreach (var item in equipList)
                {
                    EquipmentListBox.Items.Add(item.Item2);
                }
                foreach (DataGridViewRow row in Program.f1.guna2DataGridView2.SelectedRows)
                {
                    componentId = row.Cells[0].Value.ToString();
                    codeCreatorTextBox.Text = row.Cells[1].Value.ToString();
                    nameEquipmentComponentTextBox.Text = row.Cells[2].Value.ToString();
                    WebSpecificTextBox.Text = row.Cells[3].Value.ToString();
                    ComponentDateTimePicker.Text = row.Cells[4].Value.ToString();
                    
                    
                    itemTypeListBox.SelectedItem = row.Cells[5].Value.ToString();
                    
                    EquipmentListBox.SelectedItem = row.Cells[6].Value.ToString();
                    // лист с компьютерами куда установлен
                    //foreach (var item in collection)
                    //{

                    //}

                }

            }
        }
        private bool check()
        {
            
            if (nameEquipmentComponentTextBox.Text == "")
            {
                MessageBox.Show("Наименование не должно быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (itemTypeListBox.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать тип комплектующей", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]*$").Match(nameEquipmentComponentTextBox.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректное наименование", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (codeCreatorTextBox.Text == "")
            {

            }
            else
            {
                if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]*$").Match(codeCreatorTextBox.Text).Success)
                {

                }
                else
                {
                    MessageBox.Show("Некорректный код производителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (WebSpecificTextBox.Text!="") {
                if (new Regex(@"(https?):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*").Match(WebSpecificTextBox.Text).Success)
                {

                }
                else
                {
                    MessageBox.Show("Некорректная ссылка на спецификацию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        private void guna2Button1_Click(object sender, EventArgs e)// Сохранить компонент
        {
            if (check() == true)
            {
                
                //если было добавление
                if (Program.flag == true)
                {
                    string itemId = "";
                    foreach (var item in itemList)
                    {
                        if (item.Item2 == itemTypeListBox.SelectedItem.ToString())
                        {
                            itemId = item.Item1.ToString();
                            break;
                        }
                    }
                    string equipId = null;
                    if (EquipmentListBox.SelectedItem != null)
                    {
                        foreach (var item in equipList)
                        {
                            if (item.Item2 == EquipmentListBox.SelectedItem.ToString())
                            {
                                equipId = item.Item1.ToString();
                                break;
                            }
                        }
                    }


                    /// 
                    /// Проверки нужны на текст боксы
                    ///
                    if(equipId!=null)
                    //if (check == true)
                    {

                        // string insertComp = ("INSERT INTO Item_computers (item_name,code_of_creator,web_cpecification,instalation_date,item_type,equipment_id) VALUES ('" + nameEquipmentComponentTextBox.Text + "','" + codeCreatorTextBox.Text + "','" + WebSpecificTextBox.Text + "' , '" + ComponentDateTimePicker.Value + "','" + itemId + "','" + equipmentId + "')");
                        string insertComp = ("INSERT INTO Item_computers (item_name,code_of_creator,web_cpecification,instalation_date,item_type,equipment_id) VALUES ('" + nameEquipmentComponentTextBox.Text + "','" + codeCreatorTextBox.Text + "','" + WebSpecificTextBox.Text + "' , '" + ComponentDateTimePicker.Value.ToString("yyyy-MM-dd") + "','" + itemId + "','" + equipId + "')");
                        /// 
                        /// Проверки нужны на текст боксы
                        ///
                        equipmentComponent.CompItemQuery(insertComp);
                    }
                    else
                    {
                        string insertComp = ("INSERT INTO Item_computers (item_name,code_of_creator,web_cpecification,instalation_date,item_type) VALUES ('" + nameEquipmentComponentTextBox.Text + "','" + codeCreatorTextBox.Text + "','" + WebSpecificTextBox.Text + "' , '" + ComponentDateTimePicker.Value.ToString("yyyy-MM-dd") + "','" + itemId + "')");
                        /// 
                        /// Проверки нужны на текст боксы
                        ///
                        equipmentComponent.CompItemQuery(insertComp);
                    }


                    string component = "SELECT c.item_id as 'ID', c.code_of_creator as 'Код производителя', c.item_name as 'Наименование', c.web_cpecification as 'Информация о продукте', c.instalation_date as 'Дата установки', t.item_type_name as 'Тип устройства', ce.equipment_name as 'Установлен в' FROM Item_computers as c INNER JOIN Item_type as t ON (c.item_type=t.item_type_id) LEFT JOIN Computer_equipment as ce ON (c.equipment_id=ce.equipment_id)";

                    equipmentComponent.FillEquipComponentInfo(component);

                    Close();
                }
                // если изменение
                if (Program.flag == false)
                {
                    string itemId = "";
                    if (itemTypeListBox.SelectedItem.ToString() != null)
                    {
                        foreach (var item in itemList)
                        {
                            if (item.Item2 == itemTypeListBox.SelectedItem.ToString())
                            {
                                itemId = item.Item1.ToString();
                                break;
                            }
                        }
                    }
                    string equipId = null;
                    if (EquipmentListBox.SelectedItem != null)
                    {
                        foreach (var item in equipList)
                        {
                            if (item.Item2 == EquipmentListBox.SelectedItem.ToString())
                            {
                                equipId = item.Item1.ToString();
                                break;
                            }
                        }
                    }
                    if (equipId != null)
                    {
                        string updateComponent = ("UPDATE Item_computers set item_name = '" + nameEquipmentComponentTextBox.Text + "',code_of_creator = '" + codeCreatorTextBox.Text + "',web_cpecification = '" + WebSpecificTextBox.Text + "', instalation_date ='" + ComponentDateTimePicker.Value.ToString("yyyy-MM-dd") + "', item_type = '" + itemId + "', equipment_id ='" + equipId + "' WHERE item_id = '" + componentId + "'");
                        equipmentComponent.CompItemQuery(updateComponent);
                    }
                    else
                    {
                        string updateComponent = ("UPDATE Item_computers set item_name = '" + nameEquipmentComponentTextBox.Text + "',code_of_creator = '" + codeCreatorTextBox.Text + "',web_cpecification = '" + WebSpecificTextBox.Text + "', instalation_date ='" + ComponentDateTimePicker.Value.ToString("yyyy-MM-dd") + "', item_type = '" + itemId + "' WHERE item_id = '" + componentId + "'");
                        equipmentComponent.CompItemQuery(updateComponent);
                    }

                    string component = "SELECT c.item_id as 'ID', c.code_of_creator as 'Код производителя', c.item_name as 'Наименование', c.web_cpecification as 'Информация о продукте', c.instalation_date as 'Дата установки', t.item_type_name as 'Тип устройства', ce.equipment_name as 'Установлен в' FROM Item_computers as c INNER JOIN Item_type as t ON (c.item_type=t.item_type_id) LEFT JOIN Computer_equipment as ce ON (c.equipment_id=ce.equipment_id)";

                    equipmentComponent.FillEquipComponentInfo(component);

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
