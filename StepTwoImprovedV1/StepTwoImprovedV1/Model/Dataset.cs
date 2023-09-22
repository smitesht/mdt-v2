using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepTwoImprovedV1.Model
{
	/* 
     * 
     * This class is correspondence to dataset of the JSON and use in ViewModel as a Model for dataset
     *
     * The class also takes dataset values in double list and convert into string for easy represent in 
     * View. 
     * 
     * The dataset values convert to string with comma delimeter. 
     * for example {1,3,4,5} ==> "1,3,4,5"
     * 
     */

	public class Dataset
	{
        public ObservableCollection<double> dataset { get; set; } 

		private string _values = "";
		public string Values
		{
			get { return _values; }
			set { _values = value; convertStrintToArry(_values); }
		}

        public Dataset()
        {
            
        }

        public Dataset(ObservableCollection<double> _dataset)
        {
            dataset = _dataset;
			Values = string.Join(",", dataset);
        }

		private void convertStrintToArry(string str)
		{
			string[] arr = str.Split(',');

			ObservableCollection<double> darr = new ObservableCollection<double>();

			foreach (string item in arr)
			{
				try
				{
					double value = Convert.ToDouble(item);
					darr.Add(value);
				}
				catch(Exception e) 
				{
					Console.WriteLine(e.ToString());
				}
				
			}

			dataset = darr;
		}
	}
}
