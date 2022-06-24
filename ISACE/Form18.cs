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
    public partial class Form18 : Form
    {
        string adminpass = "admin";
        public Form18()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (new Regex(@"^[а-яА-ЯёЁa-zA-Z0-9 ,.!?;:-]*$").Match(guna2TextBox1.Text).Success)
            {
                if (adminpass== guna2TextBox1.Text)
                {
                    MessageBox.Show("Администратор добавлен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.adpass = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Пароль не совпал", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Некорректное наименование", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
