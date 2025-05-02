// <copyright file="MainForm.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace Testing2;

/// <summary>
/// The main application form with the "escape" button
/// </summary>
public partial class MainForm : Form
{
    /// <summary>
    /// Initializes a new instance of the main form
    /// </summary>
    public MainForm()
    {
        InitializeComponent();
    }

    private void startButton_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void startButton_MouseEnter(object sender, EventArgs e)
    {
        var random = new Random();
        startButton.Left = random.Next(0, ClientSize.Width - startButton.Width);
        startButton.Top = random.Next(0, ClientSize.Height - startButton.Height);
    }
}