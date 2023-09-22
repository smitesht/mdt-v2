using StepOneImprovedV1.Command;
using StepOneImprovedV1.Model;
using StepOneImprovedV1.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;


namespace StepOneImprovedV1.ViewModel
{
	public class MainViewModel
	{
        public ObservableCollection<ObservableCollection<double>> DataSets { get; set; }
        public List<GeneratorItem> Generators { get; set; }

        public ICommand StartCommand { get; set; }

		private List<Thread> threadList = new List<Thread>();

		// Event created to perform start/stop of the threads
		private static ManualResetEvent mre = new ManualResetEvent(false);

		// The Dictonary used to map operation and its corresponding calculation.
		// Instead of if/else or switch condition map used for easily readable code and execute operations.
		private Dictionary<string, IOperation> mapGeneratorOperation =
			new Dictionary<string, IOperation>();

		public MainViewModel(JsonData data)
        {
            DataSets = data.datasets;
            Generators = data.generators;

			// populate generator operation with its calculation.
			mapGeneratorOperation["sum"] = new SumOperation();
			mapGeneratorOperation["min"] = new MinOperation();
			mapGeneratorOperation["max"] = new MaxOperation();
			mapGeneratorOperation["average"] = new AverageOperation();

			StartCommand = new RelayCommand(Start, CanStart);
        }

		private bool CanStart(object obj)
		{
			return true;
		}

		private void Start(object obj)
		{
			foreach (GeneratorItem g in Generators)
			{
				Thread thread = new Thread(ThreadOperation);
				threadList.Add(thread);
			}

			int counter = 0;
			foreach (GeneratorItem g in Generators)
			{
				object args = new object[2] { g, DataSets };
				threadList[counter].Start(args);
				counter++;
			}

			mre.Set();
		}

		private void ThreadOperation(object obj)
		{			

			try
			{
				Array argArr = new object[2];
				argArr = (Array)obj;

				GeneratorItem g = (GeneratorItem)argArr.GetValue(0);
				ObservableCollection<ObservableCollection<double>> Datasets = (ObservableCollection<ObservableCollection<double>>)argArr.GetValue(1);

				// perform operation untill stop signal get
				while (mre.WaitOne())
				{
					foreach (var dt in Datasets)
					{
						
						double? result = null;
						IOperation dsOperation = null;

						if (mapGeneratorOperation.TryGetValue(g.operation, out dsOperation))
						{
							result = dt.Count == 0 ? 0 : dsOperation.Do(dt);
						}

						// update Output property for View
						DateTime timestamp = DateTime.Now;
						string strTimestamp = timestamp.ToString("HH:mm:ss");
						string output_message = strTimestamp + "      " + g.name + "      " + result;
						Console.WriteLine(output_message);							

						Thread.Sleep(g.interval * 1000);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
