namespace HW7.Calculator;

partial class Form1
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        button3 = new System.Windows.Forms.Button();
        button6 = new System.Windows.Forms.Button();
        panel1 = new System.Windows.Forms.Panel();
        button1 = new System.Windows.Forms.Button();
        panel2 = new System.Windows.Forms.Panel();
        panel3 = new System.Windows.Forms.Panel();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        button2 = new System.Windows.Forms.Button();
        panel1.SuspendLayout();
        panel2.SuspendLayout();
        panel3.SuspendLayout();
        SuspendLayout();
        // 
        // button3
        // 
        button3.Location = new System.Drawing.Point(409, 333);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(167, 127);
        button3.TabIndex = 2;
        button3.Text = "button3";
        button3.UseVisualStyleBackColor = true;
        // 
        // button6
        // 
        button6.Location = new System.Drawing.Point(355, 222);
        button6.Name = "button6";
        button6.Size = new System.Drawing.Size(127, 93);
        button6.TabIndex = 5;
        button6.Text = "button6";
        button6.UseVisualStyleBackColor = true;
        // 
        // panel1
        // 
        panel1.BackColor = System.Drawing.Color.Transparent;
        panel1.Controls.Add(button1);
        panel1.Dock = System.Windows.Forms.DockStyle.Top;
        panel1.Location = new System.Drawing.Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(264, 30);
        panel1.TabIndex = 6;
        // 
        // button1
        // 
        button1.Dock = System.Windows.Forms.DockStyle.Right;
        button1.FlatAppearance.BorderSize = 0;
        button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        button1.Location = new System.Drawing.Point(234, 0);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(30, 30);
        button1.TabIndex = 0;
        button1.Text = "✕";
        button1.UseVisualStyleBackColor = true;
        // 
        // panel2
        // 
        panel2.BackColor = System.Drawing.Color.Transparent;
        panel2.Controls.Add(label1);
        panel2.Dock = System.Windows.Forms.DockStyle.Top;
        panel2.Location = new System.Drawing.Point(0, 30);
        panel2.Name = "panel2";
        panel2.Size = new System.Drawing.Size(264, 43);
        panel2.TabIndex = 7;
        // 
        // panel3
        // 
        panel3.BackColor = System.Drawing.Color.Transparent;
        panel3.Controls.Add(label2);
        panel3.Dock = System.Windows.Forms.DockStyle.Top;
        panel3.Location = new System.Drawing.Point(0, 73);
        panel3.Name = "panel3";
        panel3.Size = new System.Drawing.Size(264, 46);
        panel3.TabIndex = 8;
        // 
        // label1
        // 
        label1.Dock = System.Windows.Forms.DockStyle.Right;
        label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label1.Location = new System.Drawing.Point(0, 0);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(264, 43);
        label1.TabIndex = 0;
        label1.Text = "label1";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label2
        // 
        label2.Dock = System.Windows.Forms.DockStyle.Right;
        label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        label2.Location = new System.Drawing.Point(0, 0);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(264, 46);
        label2.TabIndex = 0;
        label2.Text = "label2";
        label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // button2
        // 
        button2.BackColor = System.Drawing.Color.Transparent;
        button2.Location = new System.Drawing.Point(16, 399);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(50, 50);
        button2.TabIndex = 9;
        button2.Text = "1";
        button2.UseVisualStyleBackColor = false;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(((int)((byte)255)), ((int)((byte)255)), ((int)((byte)192)));
        BackgroundImage = ((System.Drawing.Image)resources.GetObject("$this.BackgroundImage"));
        BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        ClientSize = new System.Drawing.Size(264, 470);
        Controls.Add(button2);
        Controls.Add(panel3);
        Controls.Add(panel2);
        Controls.Add(panel1);
        Controls.Add(button6);
        Controls.Add(button3);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        MinimumSize = new System.Drawing.Size(264, 470);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Form1";
        panel1.ResumeLayout(false);
        panel2.ResumeLayout(false);
        panel3.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button button2;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.Panel panel1;

    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button6;

    #endregion
}