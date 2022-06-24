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
    public partial class Form16 : Form
    {
        Administration administration = new Administration();
        string statusid;
        public Form16()
        {
            InitializeComponent();
            if (Program.flag == true)
            {


            }
            //кнопка изменить
            if (Program.flag == false)
            {

                foreach (DataGridViewRow row in Program.f1.guna2DataGridView13.SelectedRows)
                {
                    statusid = row.Cells[0].Value.ToString();
                    guna2TextBox1.Text = row.Cells[1].Value.ToString();


                }

            }

        }
        private bool check()
        {

            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("Описание не должно быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            return true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (check() == true)
            {

                //если было добавление

                if (Program.flag == true)
                {

                    administration.Query("INSERT INTO Status_equipment (status_name) values ('" + guna2TextBox1.Text + "')");

                    administration.FillStatusInfo("SELECT s.status_id as 'ID', s.status_name as 'Наименование' FROM Status_equipment as s");

                    Close();
                }
                // если изменение
                if (Program.flag == false)
                {

                    administration.Query("UPDATE Status_equipment set status_name = '" + guna2TextBox1.Text + "'WHERE status_id = '"+statusid+"'");
                    administration.FillStatusInfo("SELECT s.status_id as 'ID', s.status_name as 'Наименование' FROM Status_equipment as s");
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
