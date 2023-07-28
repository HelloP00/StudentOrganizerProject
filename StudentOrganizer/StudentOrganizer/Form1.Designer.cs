namespace StudentOrganizer
{
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            groupBox1 = new GroupBox();
            listBox1 = new ListBox();
            panel1 = new Panel();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            textBox3 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            textBox4 = new TextBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(button4);
            splitContainer1.Panel1.Controls.Add(button3);
            splitContainer1.Panel1.Controls.Add(button2);
            splitContainer1.Panel1.Controls.Add(button1);
            splitContainer1.Panel1.Controls.Add(groupBox1);
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Controls.Add(textBox4);
            splitContainer1.Panel2.Controls.Add(listBox1);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Size = new Size(2029, 1443);
            splitContainer1.SplitterDistance = 472;
            splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 67);
            label1.Name = "label1";
            label1.Size = new Size(104, 41);
            label1.TabIndex = 3;
            label1.Text = "Name:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(0, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(469, 308);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Student Profile";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // listBox1
            // 
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 41;
            listBox1.Location = new Point(115, 747);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(300, 205);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.Location = new Point(452, 747);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 250);
            panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Menu;
            textBox1.Location = new Point(220, 61);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(194, 47);
            textBox1.TabIndex = 4;
            textBox1.Text = "John Wick";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 132);
            label2.Name = "label2";
            label2.Size = new Size(60, 41);
            label2.TabIndex = 5;
            label2.Text = "YR:";
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.MenuBar;
            textBox2.Location = new Point(220, 130);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(49, 47);
            textBox2.TabIndex = 6;
            textBox2.Text = "12";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 209);
            label3.Name = "label3";
            label3.Size = new Size(165, 41);
            label3.TabIndex = 7;
            label3.Text = "Student ID:";
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.MenuBar;
            textBox3.Location = new Point(220, 213);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(141, 47);
            textBox3.TabIndex = 8;
            textBox3.Text = "1892274";
            // 
            // button1
            // 
            button1.Location = new Point(-2, 332);
            button1.Name = "button1";
            button1.Size = new Size(472, 207);
            button1.TabIndex = 2;
            button1.Text = "Home";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(0, 545);
            button2.Name = "button2";
            button2.Size = new Size(472, 207);
            button2.TabIndex = 3;
            button2.Text = "Timetable";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(-3, 758);
            button3.Name = "button3";
            button3.Size = new Size(472, 207);
            button3.TabIndex = 4;
            button3.Text = "Calendar";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(-3, 971);
            button4.Name = "button4";
            button4.Size = new Size(472, 207);
            button4.TabIndex = 5;
            button4.Text = "Settings";
            button4.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(382, 27);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(615, 47);
            textBox4.TabIndex = 1;
            textBox4.Text = "Your day at a glance...";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(302, 348);
            label4.Name = "label4";
            label4.Size = new Size(97, 41);
            label4.TabIndex = 2;
            label4.Text = "label4";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2029, 1443);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private Label label1;
        private ListBox listBox1;
        private Panel panel1;
        private TextBox textBox1;
        private Button button2;
        private Button button1;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox2;
        private Label label2;
        private Button button4;
        private Button button3;
        private TextBox textBox4;
        private Label label4;
    }
}