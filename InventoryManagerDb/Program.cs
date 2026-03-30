using System;
using static System.Net.Mime.MediaTypeNames;

namespace InventoryManagerDb;

static class Program
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

        // THIS LINE IS THE KEY
        DatabaseHelper.InitializeDatabase();

        Application.Run(new Form1());
    }
}