namespace HW7._2.Calculator;

/// <summary>
/// Represents the main form of the calculator application.
/// </summary>
public partial class Form1 : Form
{
    private readonly CalculatorEngine _calculator = new CalculatorEngine();

    /// <summary>
    /// Initializes a new instance of the calculator form.
    /// </summary>
    public Form1()
    {
        InitializeComponent();
        InitializeButtonEvents();

        this.KeyPreview = true;
        this.KeyDown += Form1_KeyDown!;
    }

    private void InitializeButtonEvents()
    {
        button1.Click += (_, _) => ProcessInput("1");
        button2.Click += (_, _) => ProcessInput("2");
        button3.Click += (_, _) => ProcessInput("3");
        button4.Click += (_, _) => ProcessInput("4");
        button5.Click += (_, _) => ProcessInput("5");
        button6.Click += (_, _) => ProcessInput("6");
        button7.Click += (_, _) => ProcessInput("7");
        button8.Click += (_, _) => ProcessInput("8");
        button9.Click += (_, _) => ProcessInput("9");
        button10.Click += (_, _) => ProcessInput("0");

        button17.Click += (_, _) => ProcessInput("+");
        button16.Click += (_, _) => ProcessInput("-");
        button15.Click += (_, _) => ProcessInput("*");
        button14.Click += (_, _) => ProcessInput("/");

        button13.Click += (_, _) => ProcessInput("=");
        button12.Click += (_, _) => ProcessInput("C");
        button11.Click += (_, _) => ProcessInput(".");
    }

    private void ProcessInput(string input)
    {
        try
        {
            _calculator.EventHandler(input);
            UpdateDisplay();
        }
        catch (Exception ex)
        {
            textBox1.Text = ex.Message;
        }
    }

    private void UpdateDisplay()
    {
        textBox1.Text = _calculator.Output;
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        e.SuppressKeyPress = true;

        switch (e.KeyCode)
        {
            case Keys.D1 when !e.Shift: ProcessInput("1"); break;
            case Keys.D2 when !e.Shift: ProcessInput("2"); break;
            case Keys.D3 when !e.Shift: ProcessInput("3"); break;
            case Keys.D4 when !e.Shift: ProcessInput("4"); break;
            case Keys.D5 when !e.Shift: ProcessInput("5"); break;
            case Keys.D6 when !e.Shift: ProcessInput("6"); break;
            case Keys.D7 when !e.Shift: ProcessInput("7"); break;
            case Keys.D8 when !e.Shift: ProcessInput("8"); break;
            case Keys.D9 when !e.Shift: ProcessInput("9"); break;
            case Keys.D0 when !e.Shift: ProcessInput("0"); break;

            case Keys.NumPad1: ProcessInput("1"); break;
            case Keys.NumPad2: ProcessInput("2"); break;
            case Keys.NumPad3: ProcessInput("3"); break;
            case Keys.NumPad4: ProcessInput("4"); break;
            case Keys.NumPad5: ProcessInput("5"); break;
            case Keys.NumPad6: ProcessInput("6"); break;
            case Keys.NumPad7: ProcessInput("7"); break;
            case Keys.NumPad8: ProcessInput("8"); break;
            case Keys.NumPad9: ProcessInput("9"); break;
            case Keys.NumPad0: ProcessInput("0"); break;

            case Keys.Add or Keys.Oemplus when e.Shift: ProcessInput("+"); break;
            case Keys.Subtract or Keys.OemMinus: ProcessInput("-"); break;
            case Keys.Multiply or Keys.D8 when e.Shift: ProcessInput("*"); break;
            case Keys.Divide or Keys.OemQuestion when e.Shift: ProcessInput("/"); break;

            case Keys.Enter: ProcessInput("="); break;
            case Keys.Escape: ProcessInput("C"); break;
            case Keys.Decimal or Keys.OemPeriod: ProcessInput("."); break;

            default:
                e.SuppressKeyPress = false;
                return;
        }

        UpdateDisplay();
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
        Form1_KeyDown(sender, e);
    }
}