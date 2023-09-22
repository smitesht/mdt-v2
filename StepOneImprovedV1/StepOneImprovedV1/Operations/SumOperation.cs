using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepOneImprovedV1.Operations
{
	internal class SumOperation : IOperation
	{
		public double Do(ObservableCollection<double> dataset)
		{
			return dataset.Sum();
		}
	}
	
}
