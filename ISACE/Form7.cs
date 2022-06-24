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
    public partial class Form7 : Form
    {
        List<Tuple<string, string>> typeList = new List<Tuple<string, string>>();
        List<Tuple<string, string>> userList = new List<Tuple<string, string>>();
        List<Tuple<string, string>> placeList = new List<Tuple<string, string>>();
        List<Tuple<string, string>> statusList = new List<Tuple<string, string>>();
        List<Tuple<string>> equipNameList = new List<Tuple<string>>();
        string equipId;
        Equipment equipment = new Equipment();
        public Form7()
        {
            InitializeComponent();
            equipment.TypeIdList(typeList);
            equipment.UserIdList(userList);
            equipment.PlaceIdList(placeList);
            equipment.StatusIdList(statusList);
            equipment.equipNameList(equipNameList);
            //dateTimePicker1.Format = DateTimePickerFormat.Custom;
            //dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            //кнопка добавить
            if (Program.flag == true)
            {
                if (typeList.Count == 0)
                {
                    MessageBox.Show("Необходимо сначало добавить тип устройства в систему", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.nool = false;
                }
                else
                {
                    Program.nool = true;
                }
                if (placeList.Count == 0)
                {
                    MessageBox.Show("Необходимо сначало добавить место размещения в систему", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.nool = false;
                }
                else
                {
                    Program.nool = true;
                }
                if (statusList.Count == 0)
                {
                    MessageBox.Show("Необходимо сначало добавить состояние устройства в системе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.nool = false;
                }
                else
                {
                    Program.nool = true;
                }
                foreach (var item in typeList)
                {
                    listBox1.Items.Add(item.Item2);
                }
                foreach (var item in userList)
                {
                    listBox2.Items.Add(item.Item2);
                }
                foreach (var item in placeList)
                {
                    listBox3.Items.Add(item.Item2);
                }
                foreach (var item in statusList)
                {
                    listBox4.Items.Add(item.Item2);
                }
            }
            //кнопка изменить
            if (Program.flag == false)
            {

                foreach (var item in typeList)
                {
                    listBox1.Items.Add(item.Item2);
                }
                foreach (var item in userList)
                {
                    listBox2.Items.Add(item.Item2);
                }
                foreach (var item in placeList)
                {
                    listBox3.Items.Add(item.Item2);
                }
                foreach (var item in statusList)
                {
                    listBox4.Items.Add(item.Item2);
                }
                foreach (DataGridViewRow row in Program.f1.guna2DataGridView1.SelectedRows)
                {
                    equipId = row.Cells[0].Value.ToString();
                    guna2TextBox1.Text = row.Cells[1].Value.ToString();
                    listBox1.SelectedItem = row.Cells[2].Value.ToString();
                    guna2TextBox2.Text = row.Cells[3].Value.ToString();
                    guna2TextBox3.Text = row.Cells[4].Value.ToString();
                    guna2TextBox4.Text = row.Cells[5].Value.ToString();
                    listBox2.SelectedItem = row.Cells[6].Value.ToString();
                    listBox3.SelectedItem = row.Cells[7].Value.ToString();
                    dateTimePicker1.Text = row.Cells[8].Value.ToString();
                    listBox4.SelectedItem = row.Cells[9].Value.ToString();


                    // лист с компьютерами куда установлен
                    //foreach (var item in collection)
                    //{

                    //}

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
            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]*$").Match(guna2TextBox1.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректное наименование", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать тип техники", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox2.Text == "")
            {
                MessageBox.Show("Инвентарный номер не должен быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]{4,49}$").Match(guna2TextBox2.Text).Success)
            {

            }
            else
            {
                MessageBox.Show("Некорректный инвентарный номер", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (guna2TextBox3.Text!= "")
            {
                if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]{4,49}$").Match(guna2TextBox3.Text).Success)
                {

                }
                else
                {
                    MessageBox.Show("Некорректное сетевое имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (guna2TextBox4.Text != "")
            {
                if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]{4,49}$").Match(guna2TextBox4.Text).Success)
                {

                }
                else
                {
                    MessageBox.Show("Некорректный IP-адрес", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (listBox4.SelectedItem == null)
            {
                MessageBox.Show("Необходимо выбрать статус устройства", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //if (listBox3.SelectedItem == null)
            //{
            //    MessageBox.Show("Необходимо выбрать место размещения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            //if (listBox2.SelectedItem == null)
            //{
            //    MessageBox.Show("Необходимо выбрать пользователя устройства", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            if (Program.flag == true)
            {
                foreach (var item in equipNameList)
                {
                    if (item.Item1.ToString() == guna2TextBox1.Text)
                    {
                        MessageBox.Show("Наименование уже используются в системе", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;


        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (check() == true)
            {
                if (Program.flag == true)//добавление
                {
                    int i = 0;
                    string typeId = null;
                    string userId = null;
                    string placeId = null;
                    string statusId = null;
                    foreach (var item in typeList)
                    {
                        if (item.Item2 == listBox1.SelectedItem.ToString())
                        {
                            typeId = item.Item1.ToString();
                            break;
                        }
                    }
                    if (listBox2.SelectedItem!=null)
                    {
                        foreach (var item in userList)
                        {
                            if (item.Item2 == listBox2.SelectedItem.ToString())
                            {
                                userId = item.Item1.ToString();
                                break;
                            }
                        }
                    }
                    if (listBox3.SelectedItem!=null)
                    {


                        foreach (var item in placeList)
                        {
                            if (item.Item2 == listBox3.SelectedItem.ToString())
                            {
                                placeId = item.Item1.ToString();
                                break;
                            }
                        }
                    }
                    foreach (var item in statusList)
                    {
                        if (item.Item2 == listBox4.SelectedItem.ToString())
                        {
                            statusId = item.Item1.ToString();
                            break;
                        }
                    }
                    if (guna2TextBox3 != null && guna2TextBox4 != null && userId != null && placeId != null)
                        i = 1;
                    if (guna2TextBox3 == null || guna2TextBox4 == null)
                        i = 2;
                    if (placeId == null)
                        i = 3;
                    if (userId == null)
                        i = 4;
                    if (userId == null && placeId == null)
                        i = 5;
                    if (guna2TextBox3 == null || guna2TextBox4 == null && placeId == null)
                        i = 6;
                    if (guna2TextBox3 == null || guna2TextBox4 == null && userId == null)
                        i = 7;
                    if (guna2TextBox3 == null || guna2TextBox4 == null && placeId == null && userId == null)
                        i = 8;

                    switch (i)
                    {
                        //все поля заполнены
                        case 1:
                            equipment.CompQuery("INSERT INTO Computer_equipment (equipment_name, equipment_type, equipment_inventory_name, equipment_webname, equipment_IP, equipment_user, equipment_place, equipment_date, equipment_status ) VALUES ('" + guna2TextBox1.Text + "','" + typeId + "','" + guna2TextBox2.Text + "' , '" + guna2TextBox3.Text + "' , '" + guna2TextBox4.Text + "' , '" + userId + "' , '" + placeId + "' , '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + statusId + "') ");
                            break;

                        //отсутствует веб имя
                        case 2:
                            equipment.CompQuery("INSERT INTO Computer_equipment (equipment_name, equipment_type, equipment_inventory_name,  equipment_user, equipment_place, equipment_date, equipment_status ) VALUES ('" + guna2TextBox1.Text + "','" + typeId + "','" + guna2TextBox2.Text + "' , '" + userId + "' , '" + placeId + "' , '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + statusId + "') ");


                            break;//отсутствует место размещения
                        case 3:
                            equipment.CompQuery("INSERT INTO Computer_equipment (equipment_name, equipment_type, equipment_inventory_name, equipment_webname, equipment_IP, equipment_user,  equipment_date, equipment_status ) VALUES ('" + guna2TextBox1.Text + "','" + typeId + "','" + guna2TextBox2.Text + "' , '" + guna2TextBox3.Text + "' , '" + guna2TextBox4.Text + "' , '" + userId + "' , '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + statusId + "') ");


                            break;
                        case 4://отсутствует пользователь
                            equipment.CompQuery("INSERT INTO Computer_equipment (equipment_name, equipment_type, equipment_inventory_name, equipment_webname, equipment_IP,  equipment_place, equipment_date, equipment_status ) VALUES ('" + guna2TextBox1.Text + "','" + typeId + "','" + guna2TextBox2.Text + "' , '" + guna2TextBox3.Text + "' , '" + guna2TextBox4.Text + "' , '" + placeId + "' , '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + statusId + "') ");


                            break;
                        case 5://отсутствует пользователь и место размещения
                            equipment.CompQuery("INSERT INTO Computer_equipment (equipment_name, equipment_type, equipment_inventory_name, equipment_webname, equipment_IP,  equipment_date, equipment_status ) VALUES ('" + guna2TextBox1.Text + "','" + typeId + "','" + guna2TextBox2.Text + "' , '" + guna2TextBox3.Text + "' , '" + guna2TextBox4.Text + "' , '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + statusId + "') ");


                            break;
                        case 6://отсутствует сетевое имя и место размещения
                            equipment.CompQuery("INSERT INTO Computer_equipment (equipment_name, equipment_type, equipment_inventory_name,  equipment_user, equipment_date, equipment_status ) VALUES ('" + guna2TextBox1.Text + "','" + typeId + "','" + guna2TextBox2.Text + "' , '" + userId + "' , '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + statusId + "') ");

                            break;
                        case 7://отсутствует сетевое имя и пользователь
                            equipment.CompQuery("INSERT INTO Computer_equipment (equipment_name, equipment_type, equipment_inventory_name,  equipment_place, equipment_date, equipment_status ) VALUES ('" + guna2TextBox1.Text + "','" + typeId + "','" + guna2TextBox2.Text + "' , '" + placeId + "' , '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + statusId + "') ");

                            break;// отсутствует сетевое имя пользователь и место размещения
                        case 8:
                            equipment.CompQuery("INSERT INTO Computer_equipment (equipment_name, equipment_type, equipment_inventory_name,  equipment_date, equipment_status ) VALUES ('" + guna2TextBox1.Text + "','" + typeId + "','" + guna2TextBox2.Text + "' , '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + statusId + "') ");

                            break;
                        default:
                            break;
                    }
                    equipment.FillEquipInfo("SELECT e.equipment_id as 'ID', e.equipment_name as 'Наименование', t.type_name as 'Тип устройства', e.equipment_inventory_name as 'Инвентарный номер', e.equipment_webname as 'Сетевое имя', e.equipment_IP as 'IP - адрес', u.login as 'Пользователь', p.place_name as 'Место размещения', e.equipment_date as 'Дата поступления', s.status_name as 'Статус' FROM Computer_equipment as e INNER JOIN Type_equipment as t ON (e.equipment_type=t.type_id) LEFT JOIN Users as u ON (e.equipment_user=u.user_id) LEFT JOIN Place as p ON (e.equipment_place = p.place_id) INNER JOIN Status_equipment as s ON (e.equipment_status=s.status_id)");
                    Close();
                }
                // если изменение
                if (Program.flag == false)
                {
                    int i = 0;
                    string typeId = "";
                    string userId = "";
                    string placeId = "";
                    string statusId = "";
                    foreach (var item in typeList)
                    {
                        if (item.Item2 == listBox1.SelectedItem.ToString())
                        {
                            typeId = item.Item1.ToString();
                            break;
                        }
                    }
                    if (listBox2.SelectedItem != null)
                    {
                        foreach (var item in userList)
                        {
                            if (item.Item2 == listBox2.SelectedItem.ToString())
                            {
                                userId = item.Item1.ToString();
                                break;
                            }
                        }
                    }

                    if (listBox3.SelectedItem != null)
                    {
                        foreach (var item in placeList)
                        {
                            if (item.Item2 == listBox3.SelectedItem.ToString())
                            {
                                placeId = item.Item1.ToString();
                                break;
                            }
                        }
                    }
                    
                    foreach (var item in statusList)
                    {
                        if (item.Item2 == listBox4.SelectedItem.ToString())
                        {
                            statusId = item.Item1.ToString();
                            break;
                        }
                    }
                    if (guna2TextBox3 != null && guna2TextBox4 != null && userId != null && placeId != null)
                        i = 1;
                    if (guna2TextBox3 == null || guna2TextBox4 == null)
                        i = 2;
                    if (placeId == null)
                        i = 3;
                    if (userId == null)
                        i = 4;
                    if (userId == null && placeId == null)
                        i = 5;
                    if (guna2TextBox3 == null || guna2TextBox4 == null && placeId == null)
                        i = 6;
                    if (guna2TextBox3 == null || guna2TextBox4 == null && userId == null)
                        i = 7;
                    if (guna2TextBox3 == null || guna2TextBox4 == null && placeId == null && userId == null)
                        i = 8;

                    switch (i)
                    {
                        //все поля заполнены
                        case 1:
                            equipment.CompQuery("UPDATE Computer_equipment set equipment_name = '" + guna2TextBox1.Text + "' , equipment_type ='" + typeId + "' , equipment_inventory_name = '" + guna2TextBox2.Text + "' , equipment_webname = '" + guna2TextBox3.Text + "', equipment_IP = '" + guna2TextBox4.Text + "', equipment_user = '" + userId + "', equipment_place = '" + placeId + "', equipment_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', equipment_status = '" + statusId + "' WHERE equipment_id = '"+ equipId +"'");
                            break;

                        //отсутствует веб имя
                        case 2:
                            equipment.CompQuery("UPDATE Computer_equipment set equipment_name = '" + guna2TextBox1.Text + "' , equipment_type ='" + typeId + "' , equipment_inventory_name = '" + guna2TextBox2.Text + "' ,  equipment_user = '" + userId + "', equipment_place = '" + placeId + "', equipment_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', equipment_status = '" + statusId + "'WHERE equipment_id = '" + equipId + "'");

                            break;//отсутствует место размещения
                        case 3:
                            equipment.CompQuery("UPDATE Computer_equipment set equipment_name = '" + guna2TextBox1.Text + "' , equipment_type ='" + typeId + "' , equipment_inventory_name = '" + guna2TextBox2.Text + "' , equipment_webname = '" + guna2TextBox3.Text + "', equipment_IP = '" + guna2TextBox4.Text + "', equipment_user = '" + userId + "',  equipment_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', equipment_status = '" + statusId + "'WHERE equipment_id = '" + equipId + "'");

                            break;
                        case 4://отсутствует пользователь
                            equipment.CompQuery("UPDATE Computer_equipment set equipment_name = '" + guna2TextBox1.Text + "' , equipment_type ='" + typeId + "' , equipment_inventory_name = '" + guna2TextBox2.Text + "' , equipment_webname = '" + guna2TextBox3.Text + "', equipment_IP = '" + guna2TextBox4.Text + "',  equipment_place = '" + placeId + "', equipment_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', equipment_status = '" + statusId + "'WHERE equipment_id = '" + equipId + "'");

                            break;
                        case 5://отсутствует пользователь и место размещения
                            equipment.CompQuery("UPDATE Computer_equipment set equipment_name = '" + guna2TextBox1.Text + "' , equipment_type ='" + typeId + "' , equipment_inventory_name = '" + guna2TextBox2.Text + "' , equipment_webname = '" + guna2TextBox3.Text + "', equipment_IP = '" + guna2TextBox4.Text + "',  equipment_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', equipment_status = '" + statusId + "'WHERE equipment_id = '" + equipId + "'");

                            break;
                        case 6://отсутствует сетевое имя и место размещения
                            equipment.CompQuery("UPDATE Computer_equipment set equipment_name = '" + guna2TextBox1.Text + "' , equipment_type ='" + typeId + "' , equipment_inventory_name = '" + guna2TextBox2.Text + "' , equipment_user = '" + userId + "',  equipment_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', equipment_status = '" + statusId + "'WHERE equipment_id = '" + equipId + "'");

                            break;
                        case 7://отсутствует сетевое имя и пользователь
                            equipment.CompQuery("UPDATE Computer_equipment set equipment_name = '" + guna2TextBox1.Text + "' , equipment_type ='" + typeId + "' , equipment_inventory_name = '" + guna2TextBox2.Text + "' ,  equipment_place = '" + placeId + "', equipment_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', equipment_status = '" + statusId + "'WHERE equipment_id = '" + equipId + "'");

                            break;// отсутствует сетевое имя пользователь и место размещения
                        case 8:
                            equipment.CompQuery("UPDATE Computer_equipment set equipment_name = '" + guna2TextBox1.Text + "' , equipment_type ='" + typeId + "' , equipment_inventory_name = '" + guna2TextBox2.Text + "' , equipment_date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', equipment_status = '" + statusId + "'WHERE equipment_id = '" + equipId + "'");

                            break;
                        default:
                            break;
                    }
                    equipment.FillEquipInfo("SELECT e.equipment_id as 'ID', e.equipment_name as 'Наименование', t.type_name as 'Тип устройства', e.equipment_inventory_name as 'Инвентарный номер', e.equipment_webname as 'Сетевое имя', e.equipment_IP as 'IP - адрес', u.login as 'Пользователь', p.place_name as 'Место размещения', e.equipment_date as 'Дата поступления', s.status_name as 'Статус' FROM Computer_equipment as e INNER JOIN Type_equipment as t ON (e.equipment_type=t.type_id) LEFT JOIN Users as u ON (e.equipment_user=u.user_id) LEFT JOIN Place as p ON (e.equipment_place = p.place_id) INNER JOIN Status_equipment as s ON (e.equipment_status=s.status_id)");
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
    
