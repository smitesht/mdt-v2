using StepOneImprovedV1.Model;
using StepOneImprovedV1.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StepOneImprovedV1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
				string filename = @"data.json";

				if (File.Exists(filename))
				{
					string jsonString = File.ReadAllText(@"data.json");
					JsonData dataJson = JsonSerializer.Deserialize<JsonData>(jsonString);
					if (dataJson == null || dataJson.datasets.Count <= 0 || dataJson.generators.Count <= 0)
					{
						Console.WriteLine("Please use correct data.json file");
						Console.ReadLine();
					}
					MainViewModel mainViewModel = new MainViewModel(dataJson);
					mainViewModel.StartCommand.Execute(null);
				}
				else
				{
					Console.WriteLine("Not able to find data.json file, please place the data.json file at current directory");
					Console.ReadLine();
				}


			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.ReadLine();
			}
		}
	}
}
