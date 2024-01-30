using Microsoft.Extensions.Configuration;

namespace DataFilesCombiner.App;

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
		Input_btn = new Button();
		Output_btn = new Button();
		SuspendLayout();
		// 
		// Input_btn
		// 
		Input_btn.Location = new Point(12, 12);
		Input_btn.Name = "Input_btn";
		Input_btn.Size = new Size(200, 30);
		Input_btn.TabIndex = 0;
		Input_btn.Text = "Open Input Folder";
		Input_btn.UseVisualStyleBackColor = true;
		Input_btn.Click += Input_btn_Click;
		// 
		// Output_btn
		// 
		Output_btn.Location = new Point(222, 12);
		Output_btn.Name = "Output_btn";
		Output_btn.Size = new Size(200, 30);
		Output_btn.TabIndex = 1;
		Output_btn.Text = "Open Output Folder";
		Output_btn.UseVisualStyleBackColor = true;
		Output_btn.Click += Output_btn_Click;
		// 
		// Form1
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(434, 161);
		Controls.Add(Output_btn);
		Controls.Add(Input_btn);
		Name = "Form1";
		Text = "Form1";
		ResumeLayout(false);
	}

	#endregion

	private Button Input_btn;
	private Button Output_btn;
}
