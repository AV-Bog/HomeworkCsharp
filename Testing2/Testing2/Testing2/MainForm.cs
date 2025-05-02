// <copyright file="MainForm.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace Testing2;

/// <summary>
/// The main application form with the "escape" escapeButton
/// </summary>
public partial class MainForm : Form
{
    /// <summary>
    /// Initializes a new instance of the main form
    /// </summary>
    public MainForm()
    {
        this.InitializeComponent();
        if (this.escapeButton.Image != null)
        {
            this.escapeButton.Image = new Bitmap(this.escapeButton.Image, this.escapeButton.Width, this.escapeButton.Height);
        }
    }

    /// <summary>
    /// Handles escapeButton click to close the form
    /// </summary>
    private void EscapeButton_Click(object sender, EventArgs e)
    {
        this.Close();
    }
    
    /// <summary>
    /// Moves escapeButton to random position when mouse enters
    /// </summary>
    private void EscapeButton_MouseEnter(object sender, EventArgs e)
    {
        var random = new Random();
        this.escapeButton.Left = random.Next(0, this.ClientSize.Width - this.escapeButton.Width);
        this.escapeButton.Top = random.Next(0, this.ClientSize.Height - this.escapeButton.Height);
    }
}