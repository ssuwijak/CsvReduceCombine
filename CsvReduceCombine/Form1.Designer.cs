namespace CsvReduceCombine
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			progressBar1 = new ProgressBar();
			groupBox1 = new GroupBox();
			textBox1 = new TextBox();
			label1 = new Label();
			button1 = new Button();
			groupBox2 = new GroupBox();
			button4 = new Button();
			label3 = new Label();
			textBox3 = new TextBox();
			textBox2 = new TextBox();
			label2 = new Label();
			button2 = new Button();
			label4 = new Label();
			button3 = new Button();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			SuspendLayout();
			// 
			// progressBar1
			// 
			progressBar1.Dock = DockStyle.Bottom;
			progressBar1.Location = new Point(0, 192);
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new Size(549, 18);
			progressBar1.TabIndex = 4;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(textBox1);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(button1);
			groupBox1.Location = new Point(9, 5);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(532, 59);
			groupBox1.TabIndex = 9;
			groupBox1.TabStop = false;
			groupBox1.Text = "Input";
			// 
			// textBox1
			// 
			textBox1.Location = new Point(96, 20);
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.Size = new Size(346, 23);
			textBox1.TabIndex = 6;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(53, 23);
			label1.Name = "label1";
			label1.Size = new Size(37, 15);
			label1.TabIndex = 5;
			label1.Text = "Path :";
			// 
			// button1
			// 
			button1.Location = new Point(448, 20);
			button1.Name = "button1";
			button1.Size = new Size(75, 23);
			button1.TabIndex = 4;
			button1.Text = "Browse";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(button4);
			groupBox2.Controls.Add(label3);
			groupBox2.Controls.Add(textBox3);
			groupBox2.Controls.Add(textBox2);
			groupBox2.Controls.Add(label2);
			groupBox2.Controls.Add(button2);
			groupBox2.Location = new Point(9, 70);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(532, 84);
			groupBox2.TabIndex = 9;
			groupBox2.TabStop = false;
			groupBox2.Text = "Output";
			// 
			// button4
			// 
			button4.Location = new Point(448, 50);
			button4.Name = "button4";
			button4.Size = new Size(75, 23);
			button4.TabIndex = 14;
			button4.Text = "Clear";
			button4.UseVisualStyleBackColor = true;
			button4.Click += button4_Click;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(6, 53);
			label3.Name = "label3";
			label3.Size = new Size(87, 15);
			label3.TabIndex = 13;
			label3.Text = "Max. Line/File :";
			// 
			// textBox3
			// 
			textBox3.Location = new Point(96, 50);
			textBox3.Name = "textBox3";
			textBox3.Size = new Size(100, 23);
			textBox3.TabIndex = 12;
			// 
			// textBox2
			// 
			textBox2.Location = new Point(96, 20);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(346, 23);
			textBox2.TabIndex = 11;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(53, 23);
			label2.Name = "label2";
			label2.Size = new Size(37, 15);
			label2.TabIndex = 10;
			label2.Text = "Path :";
			// 
			// button2
			// 
			button2.Location = new Point(448, 22);
			button2.Name = "button2";
			button2.Size = new Size(75, 23);
			button2.TabIndex = 9;
			button2.Text = "Browse";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(15, 167);
			label4.Name = "label4";
			label4.Size = new Size(38, 15);
			label4.TabIndex = 10;
			label4.Text = "label4";
			// 
			// button3
			// 
			button3.Location = new Point(457, 163);
			button3.Name = "button3";
			button3.Size = new Size(75, 23);
			button3.TabIndex = 11;
			button3.Text = "Start";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(549, 210);
			Controls.Add(button3);
			Controls.Add(label4);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			Controls.Add(progressBar1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MaximizeBox = false;
			Name = "Form1";
			Text = "Form1";
			FormClosing += Form1_Closing;
			FormClosed += Form1_Closed;
			Load += Form1_Load;
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private ProgressBar progressBar1;
		private GroupBox groupBox1;
		private TextBox textBox1;
		private Label label1;
		private Button button1;
		private GroupBox groupBox2;
		private Label label3;
		private TextBox textBox3;
		private TextBox textBox2;
		private Label label2;
		private Button button2;
		private Label label4;
		private Button button3;
		private Button button4;
	}
}
