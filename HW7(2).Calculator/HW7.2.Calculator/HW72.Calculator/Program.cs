// <copyright file="Program.cs" author="AV-Bog">
// under MIT License
// Free use, modification, and distribution are permitted,
// provided that the attribution and license notice are preserved.
// more detailed: https://github.com/AV-Bog/HomeworkCsharp/blob/main/LICENSE
// </copyright>

namespace HW72.Calculator;

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
        Application.Run(new CalculatorForm());
    }
}