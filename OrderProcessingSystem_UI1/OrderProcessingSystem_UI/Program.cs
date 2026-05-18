using System;
using System.Windows.Forms;
using OrderProcessingSystem_UI.Forms;

namespace OrderProcessingSystem_UI
{
    internal static class Program
    {
        /// <summary>
        /// نقطة الدخول الرئيسية للتطبيق
        /// </summary>
        [STAThread]
        static void Main()
        {
            // تطبيق إعدادات الواجهة
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // تشغيل النافذة الرئيسية
            Application.Run(new MainForm());
        }
    }
}