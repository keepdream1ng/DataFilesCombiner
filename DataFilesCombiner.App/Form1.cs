using DataFileCombiner.ClassLibrary.Utility;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace DataFilesCombiner.App;

public partial class Form1 : Form
{
	private readonly IConfiguration _configuration;

	public Form1(IConfiguration configuration)
	{
		InitializeComponent();
		_configuration = configuration;
	}

	private void Input_btn_Click(object sender, EventArgs e)
	{
		Process.Start("explorer.exe", WorkingFolders.GetExistingFullPath(_configuration["InputDirectoryPath"]));
	}
}
