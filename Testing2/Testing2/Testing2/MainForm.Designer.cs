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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        button = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // button
        // 
        button.BackColor = System.Drawing.Color.Transparent;
        button.BackgroundImage = ((System.Drawing.Image)resources.GetObject("button.BackgroundImage"));
        button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button.Image = ((System.Drawing.Image)resources.GetObject("button.Image"));
        button.Location = new System.Drawing.Point(12, 11);
        button.Name = "button";
        button.Size = new System.Drawing.Size(70, 70);
        button.TabIndex = 0;
        button.Text = "Tub to my";
        button.UseVisualStyleBackColor = false;
        button.Click += button_Click;
        button.MouseEnter += button_MouseEnter;
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
        Controls.Add(button);
        MinimumSize = new System.Drawing.Size(822, 506);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Catch the weed";
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button button;

    #endregion
}