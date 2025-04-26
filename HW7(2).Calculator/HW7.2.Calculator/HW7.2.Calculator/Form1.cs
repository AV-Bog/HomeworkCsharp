namespace HW7._2.Calculator;

public partial class Form1 : Form
{
    private CalculatorEngine calculator = new CalculatorEngine();

    public Form1()
    {
        InitializeComponent();
        InitializeButtonEvents();

        this.KeyPreview = true;
        this.KeyDown += Form1_KeyDown;
    }

    private void InitializeButtonEvents()
    {
        button1.Click += (s, e) => ProcessInput("1");
        button2.Click += (s, e) => ProcessInput("2");
        button3.Click += (s, e) => ProcessInput("3");
        button4.Click += (s, e) => ProcessInput("4");
        button5.Click += (s, e) => ProcessInput("5");
        button6.Click += (s, e) => ProcessInput("6");
        button7.Click += (s, e) => ProcessInput("7");
        button8.Click += (s, e) => ProcessInput("8");
        button9.Click += (s, e) => ProcessInput("9");
        button10.Click += (s, e) => ProcessInput("0");

        button17.Click += (s, e) => ProcessInput("+");
        button16.Click += (s, e) => ProcessInput("-");
        button15.Click += (s, e) => ProcessInput("*");
        button14.Click += (s, e) => ProcessInput("/");

        button13.Click += (s, e) => ProcessInput("=");
        button12.Click += (s, e) => ProcessInput("C");
        button11.Click += (s, e) => ProcessInput(".");
    }

    private void ProcessInput(string input)
    {
        try
        {
            calculator.EventHandler(input);
            UpdateDisplay();
        }
        catch (Exception ex)
        {
            textBox1.Text = ex.Message;
        }
    }

    private void UpdateDisplay()
    {
        textBox1.Text = calculator.CurrentValue.ToString();
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