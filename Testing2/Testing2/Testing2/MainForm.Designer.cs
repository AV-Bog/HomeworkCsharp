// <copyright file="MainForm.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace Testing2;

/// <summary>
/// Main application form with an interactive escapeButton that moves when hovered
/// </summary>
partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        escapeButton = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // escapeButton
        // 
        escapeButton.BackColor = System.Drawing.Color.Transparent;
        escapeButton.BackgroundImage = ((System.Drawing.Image)resources.GetObject("escapeButton.BackgroundImage"));
        escapeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        escapeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        escapeButton.Image = ((System.Drawing.Image)resources.GetObject("escapeButton.Image"));
        escapeButton.Location = new System.Drawing.Point(12, 11);
        escapeButton.Name = "escapeButton";
        escapeButton.Size = new System.Drawing.Size(70, 70);
        escapeButton.TabIndex = 0;
        escapeButton.Text = "Tub to my";
        escapeButton.UseVisualStyleBackColor = false;
        escapeButton.Click += EscapeButton_Click;
        escapeButton.MouseEnter += EscapeButton_MouseEnter;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        BackColor = System.Drawing.Color.Green;
        BackgroundImage = ((System.Drawing.Image)resources.GetObject("$this.BackgroundImage"));
        BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(escapeButton);
        MinimumSize = new System.Drawing.Size(822, 506);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Catch the weed";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button escapeButton;

    #endregion
}