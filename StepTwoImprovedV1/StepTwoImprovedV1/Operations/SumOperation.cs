using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepTwoImprovedV1.Operations
{
	// Perform sum operation
	internal class SumOperation : IOperation
	{
		public double Do(ObservableCollection<double> dataset)
		{
			return dataset.Sum();
		}
	}
}
