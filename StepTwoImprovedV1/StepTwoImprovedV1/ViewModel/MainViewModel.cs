using StepTwoImprovedV1.Command;
using StepTwoImprovedV1.Model;
using StepTwoImprovedV1.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Xml.Linq;

namespace StepTwoImprovedV1.ViewModel
{
	public class MainViewModel: INotifyPropertyChanged
	{
		
		// Generator properties
		private ObservableCollection<GeneratorItem> _generators = new ObservableCollection<GeneratorItem>();
		public ObservableCollection<GeneratorItem> Generators
		{
			get { return _generators; }
			set { _generators = value; }
		}

		// When generator select this property used
		private GeneratorItem selectedGenerator;
		public GeneratorItem SelectedGenerator
		{
			get { return selectedGenerator; }
			set
			{
				selectedGenerator = value;
				OnPropertyChanged(nameof(SelectedGenerator));
			}
		}

		// Datasets properties 
		private ObservableCollection<Dataset> _datasets = new ObservableCollection<Dataset>();
        public ObservableCollection<Dataset> Datasets 
		{ 
			get { return _datasets; }
			set { _datasets = value; } 
		}

		// When dataset select this property used
		private Dataset selectedDataset;
		public Dataset SelectedDataset
		{
			get { return selectedDataset; }
			set { selectedDataset = value; 
			OnPropertyChanged(nameof(SelectedDataset));}
		}

		// This is Output properties to display to View
		private ObservableCollection<Output> _outputs = new ObservableCollection<Output>();
		public ObservableCollection<Output> Outputs
		{
			get { return _outputs; }
			set { _outputs = value; OnPropertyChanged(nameof(Outputs)); }
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		// Event created to perform start/stop of the threads
		private static ManualResetEvent mre = new ManualResetEvent(false);
				

		// Command used to bind Start and Stop button.
		// When Start button click, it create threads if not created and set the event.
        public RelayCommand StartCommand { get; set; }
		public RelayCommand StopCommand { get; set; }

		// This properties used to enable/disable Start/Stop buttons
		private bool _isRunning = true;
		public bool IsRunning 
		{ 
			get { return _isRunning; } 
			set 
			{ 
				_isRunning =  value; 
				OnPropertyChanged(nameof(IsRunning));
				StartCommand.RaiseCanExecuteChanged();
				StopCommand.RaiseCanExecuteChanged();
			} 
		}

		// List of threads
		private List<Thread> threadList = null;


		// The Dictonary used to map operation and its corresponding calculation.
		// Instead of if/else or switch condition map used for easily readable code and execute operations.
		private Dictionary<string, IOperation> mapGeneratorOperation = 
			new Dictionary<string, IOperation>();
		
		// Constructor
        public MainViewModel() {

			// populate generator operation with its calculation.
			mapGeneratorOperation["sum"] = new SumOperation();
			mapGeneratorOperation["min"] = new MinOperation();
			mapGeneratorOperation["max"] = new MaxOperation();
			mapGeneratorOperation["average"] = new AverageOperation();
			

			StartCommand = new RelayCommand(Start, CanStart);
			StopCommand = new RelayCommand(Stop, CanStop);								
			
		}


		// initialize view models properties 
		public void InitData(ObservableCollection<GeneratorItem> generators, ObservableCollection<ObservableCollection<double>> datasets )
		{
			foreach(var d in datasets)
			{
				Datasets.Add(new Dataset(d));
			}

			foreach(var g in generators)
			{
				Generators.Add(g);
			}			
		}

		// Thread function that iterate datasets and perform operation based on given interval.
		private void ThreadOperation(object? obj)
		{
			if (obj == null)
			{
				return;
			}

			try
			{
				Array argArr = new object[2];
				argArr = (Array)obj;

				GeneratorItem g = (GeneratorItem)argArr.GetValue(0);
				ObservableCollection<Dataset> Datasets = (ObservableCollection<Dataset>)argArr.GetValue(1);
				
				// if generator null
				if(g == null) {
					MessageBox.Show("No Generator Defined");
					return;
				}
				else if((Datasets.Count <= 0) || (Datasets == null))
				{
					MessageBox.Show("No Datasets Defined");
					return;
				}

				// perform operation untill stop signal get
				while (mre.WaitOne())
				{
					foreach(var dt in Datasets)
					{
						if (Application.Current != null)
						{
							Application.Current.Dispatcher.Invoke(() =>
							{
								double? result = null;
								IOperation dsOperation = null;

								if(mapGeneratorOperation.TryGetValue(g.operation,out dsOperation))
								{									
									result = dt.dataset.Count == 0 ? 0 : dsOperation.Do(dt.dataset);
								}								
										
								// update Output property for View
								DateTime timestamp = DateTime.Now;
								string strTimestamp = timestamp.ToString("HH:mm:ss");
								strTimestamp = strTimestamp + "      " + g.name + "      " + result;
								Outputs.Add(new Output() { output = strTimestamp });

							});
						}

						Thread.Sleep(g.interval * 1000);
					}				
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());				
			}
		}
				

		private bool CanStop(object obj)
		{
			return !IsRunning;
		}

		private void Stop(object obj)
		{
			mre.Reset(); IsRunning = true;
		}

		private bool CanStart(object obj)
		{
			return IsRunning;
		}
		
		// When Start button clicked
		private void Start(object obj)
		{
			if( threadList == null || threadList.Count <= 0)
			{
				if (Generators.Count <= 0)
				{
					MessageBox.Show("No Generator defined");
					return;
				}
				InitializeThreads();
			}
			

			mre.Set();	
			IsRunning = false;
		}		


		// Create threads and append to threadlist
		private void InitializeThreads()
		{
			threadList = new List<Thread>();
			
			foreach (GeneratorItem g in Generators)
			{
				Thread thread = new Thread(ThreadOperation);
				threadList.Add(thread);
			}

			int counter = 0;
			foreach (GeneratorItem g in Generators)
			{
				object args = new object[2] { g, Datasets };
				threadList[counter].Start(args);
				counter++;
			}
		}

		private void OnPropertyChanged(string propertyName)
		{
			if(propertyName != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}		


	}
}
