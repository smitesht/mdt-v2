using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepTwoImprovedV1.Operations
{
	// Perform Min operation
	internal class MinOperation : IOperation
	{
		public double Do(ObservableCollection<double> dataset)
		{
			return dataset.Min();
		}
	}
}
