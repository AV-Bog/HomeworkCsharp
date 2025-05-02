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
        button.Image = new Bitmap(button.Image, button.Width, button.Height);
    }

    private void button_Click(object sender, EventArgs e)
    {
        this.Close();
    }
    
    private void button_MouseEnter(object sender, EventArgs e)
    {
        var random = new Random();
        button.Left = random.Next(0, ClientSize.Width - button.Width);
        button.Top = random.Next(0, ClientSize.Height - button.Height);
    }
}