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
		Matches_Lable = new Label();
		Generate_Rpt_Btn = new Button();
		Info_Label = new Label();
		Reset_Btn = new Button();
		SuspendLayout();
		// 
		// Input_btn
		// 
		Input_btn.Location = new Point(12, 35);
		Input_btn.Name = "Input_btn";
		Input_btn.Size = new Size(130, 30);
		Input_btn.TabIndex = 0;
		Input_btn.Text = "Open Input Folder";
		Input_btn.UseVisualStyleBackColor = true;
		Input_btn.Click += Input_btn_Click;
		// 
		// Output_btn
		// 
		Output_btn.Location = new Point(292, 35);
		Output_btn.Name = "Output_btn";
		Output_btn.Size = new Size(130, 30);
		Output_btn.TabIndex = 1;
		Output_btn.Text = "Open Output Folder";
		Output_btn.UseVisualStyleBackColor = true;
		Output_btn.Click += Output_btn_Click;
		// 
		// Matches_Lable
		// 
		Matches_Lable.AutoSize = true;
		Matches_Lable.Font = new Font("Segoe UI", 9F);
		Matches_Lable.ImageAlign = ContentAlignment.MiddleLeft;
		Matches_Lable.Location = new Point(12, 70);
		Matches_Lable.Name = "Matches_Lable";
		Matches_Lable.Size = new Size(158, 15);
		Matches_Lable.TabIndex = 2;
		Matches_Lable.Text = "Check matched records here";
		Matches_Lable.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// Generate_Rpt_Btn
		// 
		Generate_Rpt_Btn.Location = new Point(12, 90);
		Generate_Rpt_Btn.Name = "Generate_Rpt_Btn";
		Generate_Rpt_Btn.Size = new Size(410, 30);
		Generate_Rpt_Btn.TabIndex = 3;
		Generate_Rpt_Btn.Text = "Generate Report";
		Generate_Rpt_Btn.UseVisualStyleBackColor = true;
		Generate_Rpt_Btn.Click += Generate_Rpt_Btn_Click;
		// 
		// Info_Label
		// 
		Info_Label.AutoSize = true;
		Info_Label.Location = new Point(12, 9);
		Info_Label.Name = "Info_Label";
		Info_Label.Size = new Size(223, 15);
		Info_Label.TabIndex = 4;
		Info_Label.Text = "Copy data files in the input folder to start";
		// 
		// Reset_Btn
		// 
		Reset_Btn.Location = new Point(152, 35);
		Reset_Btn.Name = "Reset_Btn";
		Reset_Btn.Size = new Size(130, 30);
		Reset_Btn.TabIndex = 5;
		Reset_Btn.Text = "Reset App";
		Reset_Btn.UseVisualStyleBackColor = true;
		Reset_Btn.Click += Reset_Btn_Click;
		// 
		// Form1
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(434, 131);
		Controls.Add(Reset_Btn);
		Controls.Add(Info_Label);
		Controls.Add(Generate_Rpt_Btn);
		Controls.Add(Matches_Lable);
		Controls.Add(Output_btn);
		Controls.Add(Input_btn);
		Name = "Form1";
		Text = "Form1";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private Button Input_btn;
	private Button Output_btn;
	private Label Matches_Lable;
	private Button Generate_Rpt_Btn;
	private Label Info_Label;
	private Button Reset_Btn;
}
