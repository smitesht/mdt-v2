using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepOneImprovedV1.Model
{
	public class Dataset
	{
        public ObservableCollection<double> dataset { get; set; }

        public Dataset()
        {
            dataset = new ObservableCollection<double>();
        }
    }
}
