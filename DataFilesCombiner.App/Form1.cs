using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Utility;
using DataFilesCombiner.ClassLibrary.ExtensionMethods;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace DataFilesCombiner.App;

public partial class Form1 : Form
{
	private readonly IConfiguration _configuration;
	private readonly IMatchingDataService _dataService;
	public string matchesInfo => $"{_dataService.CheckedMatchesCount} matched records found";

	public Form1(
		IConfiguration configuration,
		IMatchingDataService dataService
		)
	{
		InitializeComponent();
		_configuration = configuration;
		_dataService = dataService;
		_dataService.NewMatchesFound += OnNewMatchesFound;
		Matches_Lable.Text = matchesInfo;
		Generate_Rpt_Btn.Enabled = false;
	}

	private void Input_btn_Click(object sender, EventArgs e)
	{
		Process.Start("explorer.exe", _configuration.GetInputDirectoryExistingPath());
	}

	private void Output_btn_Click(object sender, EventArgs e)
	{
		Process.Start("explorer.exe", _configuration.GetOutputDirectoryExistingPath());
	}

	private void Generate_Rpt_Btn_Click(object sender, EventArgs e)
	{
	}
	private void OnNewMatchesFound(object sender, EventArgs e)
	{
		// Need to run this on UI thread.
		this.Invoke((MethodInvoker)delegate
		{
			Matches_Lable.Text = matchesInfo;
			Generate_Rpt_Btn.Enabled = (_dataService.CheckedMatchesCount > 0);
		});
	}
}
