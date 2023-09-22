using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepOneImprovedV1.Model
{
	public class GeneratorItem
	{
		public string name { get; set; }
		public string operation { get; set; }
		public int interval { get; set; }

		public GeneratorItem()
		{

		}
		public GeneratorItem(string name, string opration, int interval)
		{
			this.name = name;
			this.operation = opration;
			this.interval = interval;
		}
	}
}
