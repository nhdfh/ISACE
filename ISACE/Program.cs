using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISACE
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// 
        /// </summary>
        public static MainForm f1;
        public static ReportForm f10;
        public static bool flag;
        public static bool nool;
        public static bool adpass;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
