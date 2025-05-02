// <copyright file="MainForm.cs" author="AV-Bog">
// under MIT License
// </copyright>

namespace Testing2;

/// <summary>
/// Main application form with an interactive button that moves when hovered
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
        startButton = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // startButton
        // 
        startButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
        startButton.Location = new System.Drawing.Point(12, 11);
        startButton.Name = "startButton";
        startButton.Size = new System.Drawing.Size(70, 70);
        startButton.TabIndex = 0;
        startButton.Text = "Tub to my";
        startButton.UseVisualStyleBackColor = false;
        startButton.Click += startButton_Click;
        startButton.MouseEnter += startButton_MouseEnter;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(startButton);
        MinimumSize = new System.Drawing.Size(822, 506);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Progress Bar Demo";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button startButton;

    #endregion
}