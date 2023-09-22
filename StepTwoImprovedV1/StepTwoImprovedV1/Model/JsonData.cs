using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StepTwoImprovedV1.Model
{
	public class JsonData
	{
		public ObservableCollection<ObservableCollection<double>> datasets { get; set; }
		public ObservableCollection<GeneratorItem> generators { get; set; }
	}
}
