using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepOneImprovedV1.Model
{
	public class JsonData
	{
		public ObservableCollection<ObservableCollection<double>> datasets { get; set; }
		public List<GeneratorItem> generators { get; set; }
	}
}
