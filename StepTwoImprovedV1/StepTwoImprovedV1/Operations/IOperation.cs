using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepTwoImprovedV1.Operations
{
	// The interface is created to perform various operation by
	// implementing Do method on the dataset
	public interface IOperation
	{
		double Do(ObservableCollection<double> dataset);
	}
}
