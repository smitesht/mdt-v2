using Microsoft.Win32;
using StepTwoImprovedV1.Model;
using StepTwoImprovedV1.ViewModel;
using System;
using System.IO;
using System.Text.Json;
using System.Windows;


namespace StepTwoImprovedV1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainViewModel viewModel;
		public MainWindow()
		{
			InitializeComponent();
			viewModel = new MainViewModel();
			this.DataContext = viewModel;

		}

		private void btnFileBrowse_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "JSON files (*.json)|*.json";

			if (openFileDialog.ShowDialog() == true)
			{
				try
				{
					string jsonFilePath = openFileDialog.FileName;

					string jsonString = File.ReadAllText(jsonFilePath);
					JsonData dataJson = JsonSerializer.Deserialize<JsonData>(jsonString);
					if (dataJson == null || dataJson.datasets == null || dataJson.generators == null)
					{
						MessageBox.Show("Please use correct data.json file");
					}
					else
					{
						viewModel.InitData(dataJson.generators, dataJson.datasets);						
					}

				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

			}
		}
    }
}
