using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptGui.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private Services.IProblemType1918 _problemType1918 = null;

        public MainWindowViewModel(Services.IProblemType1918 problemType1918)
        {
            _problemType1918 = problemType1918;
        }

        public ObservableCollection<string> ProblemType1918Names { get; private set; } =
            new ObservableCollection<string>();
        public ObservableCollection<double> ProblemType1918Weights { get; private set; } =
            new ObservableCollection<double>();
        public ObservableCollection<double> ProblemType1918Values { get; private set; } =
            new ObservableCollection<double>();

        private string _selectedRowProblemType1918 = null;
        public string SelectedRowProblemType1918
        {
            get => _selectedRowProblemType1918;
            set
            {
                if (SetProperty(ref _selectedRowProblemType1918, value))
                {
                    Debug.WriteLine(_selectedRowProblemType1918 ?? "No Row Selected");
                }
            }
        }

        private DelegateCommand _commandSolve = null;
        public DelegateCommand CommandSolve =>
            _commandSolve ?? (_commandSolve = new DelegateCommand(CommandSolveExecute));

        private void CommandSolveExecute()
        {
            // Not yet implemented
        }
    }
}
