using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StepTwoImprovedV1.Command
{
	public class RelayCommand : ICommand
	{
		public event EventHandler? CanExecuteChanged;
		private Action<object> _execute;
		private Predicate<object> _predicate;

        public RelayCommand(Action<object> execute, Predicate<object> predicate)
        {
            _execute = execute;
			_predicate = predicate;
        }

        public bool CanExecute(object? parameter)
		{
			return _predicate(parameter);
		}

		public void Execute(object? parameter)
		{
			_execute(parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
