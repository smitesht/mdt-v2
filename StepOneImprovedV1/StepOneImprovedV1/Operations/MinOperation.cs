using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepOneImprovedV1.Operations
{
	public class MinOperation : IOperation
	{
		public double Do(ObservableCollection<double> dataset)
		{
			return dataset.Min();
		}
	}
}
