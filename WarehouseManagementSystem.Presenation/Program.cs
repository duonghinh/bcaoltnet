using QuestPDF.Infrastructure;
using WarehouseManagementSystem.Presenation.Forms;

namespace WarehouseManagementSystem.Presenation
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            using (var login = new LoginForm())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;
            }

            Application.Run(new MainForm());
        }
    }
}