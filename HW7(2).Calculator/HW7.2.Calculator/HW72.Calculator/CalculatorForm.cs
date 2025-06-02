// <copyright file="CalculatorForm.cs" author="AV-Bog">
// under MIT License
// Free use, modification, and distribution are permitted,
// provided that the attribution and license notice are preserved.
// more detailed: https://github.com/AV-Bog/HomeworkCsharp/blob/main/LICENSE
// </copyright>

namespace HW72.Calculator;

/// <summary>
/// Represents the main form of the calculator application.
/// </summary>
public partial class CalculatorForm : Form
{
    private readonly CalculatorEngine calculator = new CalculatorEngine();

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorForm"/> class.
    /// Initializes a new instance of the calculator form.
    /// </summary>
    public CalculatorForm()
    {
        this.InitializeComponent();
        this.InitializeButtonEvents();

        this.KeyPreview = true;
        this.KeyDown += this.Form1_KeyDown!;
    }

    private void InitializeButtonEvents()
    {
        this.button1.Click += (_, _) => this.ProcessInput("1");
        this.button2.Click += (_, _) => this.ProcessInput("2");
        this.button3.Click += (_, _) => this.ProcessInput("3");
        this.button4.Click += (_, _) => this.ProcessInput("4");
        this.button5.Click += (_, _) => this.ProcessInput("5");
        this.button6.Click += (_, _) => this.ProcessInput("6");
        this.button7.Click += (_, _) => this.ProcessInput("7");
        this.button8.Click += (_, _) => this.ProcessInput("8");
        this.button9.Click += (_, _) => this.ProcessInput("9");
        this.button0.Click += (_, _) => this.ProcessInput("0");

        this.buttonPlus.Click += (_, _) => this.ProcessInput("+");
        this.buttonMinus.Click += (_, _) => this.ProcessInput("-");
        this.buttonMultiply.Click += (_, _) => this.ProcessInput("*");
        this.buttonShare.Click += (_, _) => this.ProcessInput("/");

        this.buttonEquals.Click += (_, _) => this.ProcessInput("=");
        this.buttonC.Click += (_, _) => this.ProcessInput("C");
        this.buttonDot.Click += (_, _) => this.ProcessInput(".");
    }

    private void ProcessInput(string input)
    {
        try
        {
            this.calculator.EventHandler(input);
            this.UpdateDisplay();
        }
        catch (Exception ex)
        {
            this.textBox1.Text = ex.Message;
        }
    }

    private void UpdateDisplay()
    {
        this.textBox1.Text = this.calculator.Output;
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        e.SuppressKeyPress = true;

        switch (e.KeyCode)
        {
            case Keys.D1 when !e.Shift: this.ProcessInput("1"); break;
            case Keys.D2 when !e.Shift: this.ProcessInput("2"); break;
            case Keys.D3 when !e.Shift: this.ProcessInput("3"); break;
            case Keys.D4 when !e.Shift: this.ProcessInput("4"); break;
            case Keys.D5 when !e.Shift: this.ProcessInput("5"); break;
            case Keys.D6 when !e.Shift: this.ProcessInput("6"); break;
            case Keys.D7 when !e.Shift: this.ProcessInput("7"); break;
            case Keys.D8 when !e.Shift: this.ProcessInput("8"); break;
            case Keys.D9 when !e.Shift: this.ProcessInput("9"); break;
            case Keys.D0 when !e.Shift: this.ProcessInput("0"); break;

            case Keys.NumPad1: this.ProcessInput("1"); break;
            case Keys.NumPad2: this.ProcessInput("2"); break;
            case Keys.NumPad3: this.ProcessInput("3"); break;
            case Keys.NumPad4: this.ProcessInput("4"); break;
            case Keys.NumPad5: this.ProcessInput("5"); break;
            case Keys.NumPad6: this.ProcessInput("6"); break;
            case Keys.NumPad7: this.ProcessInput("7"); break;
            case Keys.NumPad8: this.ProcessInput("8"); break;
            case Keys.NumPad9: this.ProcessInput("9"); break;
            case Keys.NumPad0: this.ProcessInput("0"); break;

            case Keys.Add or Keys.Oemplus when e.Shift: this.ProcessInput("+"); break;
            case Keys.Subtract or Keys.OemMinus: this.ProcessInput("-"); break;
            case Keys.Multiply or Keys.D8 when e.Shift: this.ProcessInput("*"); break;
            case Keys.Divide or Keys.OemQuestion when e.Shift: this.ProcessInput("/"); break;

            case Keys.Enter: this.ProcessInput("="); break;
            case Keys.Escape: this.ProcessInput("C"); break;
            case Keys.Decimal or Keys.OemPeriod: this.ProcessInput("."); break;

            default:
                e.SuppressKeyPress = false;
                return;
        }

        this.UpdateDisplay();
    }

    private void TextBox1_KeyDown(object sender, KeyEventArgs e)
    {
        this.Form1_KeyDown(sender, e);
    }
}