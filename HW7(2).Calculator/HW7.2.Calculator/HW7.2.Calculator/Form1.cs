namespace HW7._2.Calculator;

public partial class Form1 : Form
{
    private CalculatorEngine calculator = new CalculatorEngine();

    public Form1()
    {
        InitializeComponent();
        InitializeButtonEvents();
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
}