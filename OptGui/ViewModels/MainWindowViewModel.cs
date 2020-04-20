namespace OptGui.ViewModels
{
    using OptGui.Services;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    /// Defines the <see cref="MainWindowViewModel" />.
    /// </summary>
    public class MainWindowViewModel : BindableBase, INotifyPropertyChanged
    {
        
        /// <summary>
        /// Defines the _name.
        /// </summary>
        private string _name;

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get => _name; 
            set 
            { 
                _name = value;
                this.RaisePropertyChanged("Name");
                CommandAddRow.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Defines the _weight.
        /// </summary>
        private double? _weight;

        /// <summary>
        /// Gets or sets the Weight.
        /// </summary>
        public double? Weight 
        { 
            get => _weight; 
            set 
            {
                _weight = value;
                this.RaisePropertyChanged("Weight");
                CommandAddRow.RaiseCanExecuteChanged();
            } 
        }

        /// <summary>
        /// Defines the _value.
        /// </summary>
        private double? _value;

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public double? Value 
        { 
            get => _value; 
            set 
            {
                _value = value;
                this.RaisePropertyChanged("Value");
                CommandAddRow.RaiseCanExecuteChanged();
            } 
        }

        public double? NumericalFallback { get; set; }
        /// <summary>
        /// Gets the CommandAddRow.
        /// </summary>
        public DelegateCommand CommandAddRow { get; private set; }

        /// <summary>
        /// Gets the CommandRemoveSelectedRow.
        /// </summary>
        public DelegateCommand CommandRemoveSelectedRow { get; private set; }

        /// <summary>
        /// Gets the CommandSolve.
        /// </summary>
        public DelegateCommand CommandSolve { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.Rows = new ObservableCollection<KnapsackRow>();
            CommandAddRow = new DelegateCommand(AddRow, CanAddRow);
            CommandSolve = new DelegateCommand(Solve, CanSolve);
            this.Name = null;
            this.Weight = null;
            this.Value = null;
        }

        /// <summary>
        /// The AddRow.
        /// </summary>
        void AddRow()
        {
            this.Rows.Add(new KnapsackRow(this.Name, this.Weight, this.Value));
            ResetVals();
        }

        void ResetVals()
        {
            this.Name = null;
            this.Weight = null;
            this.Value = null;
        }
        /// <summary>
        /// The CanAddRow.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool CanAddRow()
        {
            return (!string.IsNullOrEmpty(this.Name) 
                && this.Weight.HasValue 
                && this.Value.HasValue
                && this.Weight != 0
                && this.Value != 0);
        }

        /// <summary>
        /// The Solve.
        /// </summary>
        void Solve()
        {
            Debug.WriteLine("Solved!");
        }

        /// <summary>
        /// The CanSolve.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool CanSolve()
        {
            return this.Rows.Count > 0;
        }

        /// <summary>
        /// Gets or sets the Rows.
        /// </summary>
        public ObservableCollection<KnapsackRow> Rows { get; set; }

        /// <summary>
        /// Gets or sets the StageCount.
        /// </summary>
        public int StageCount { set; get; }

        /// <summary>
        /// Gets or sets the DecisionObservableCollections.
        /// </summary>
        public ObservableCollection<ObservableCollection<double>> DecisionObservableCollections { get; set; }

        /// <summary>
        /// Gets or sets the RecursiveReturnsObservableCollections.
        /// </summary>
        public ObservableCollection<ObservableCollection<double>> RecursiveReturnsObservableCollections { get; set; }

        /// <summary>
        /// Gets the StageTable.
        /// </summary>
        public ObservableCollection<ObservableCollection<double>> StageTable
        {
            get
            {
                var table = new ObservableCollection<ObservableCollection<double>>();

                for (int i = this.StageCount; i >= 0; i--)
                {
                    table.Add(this.RecursiveReturnsObservableCollections[i]);
                    table.Add(this.DecisionObservableCollections[i]);
                }

                return table;
            }
        }

        /// <summary>
        /// Gets the OptimalPolicy.
        /// </summary>
        public double OptimalPolicy { get; }

        /// <summary>
        /// Defines the _selectedRowProblemType1918.
        /// </summary>
        private string _selectedRow = null;

        /// <summary>
        /// Gets or sets the SelectedRowProblemType1918.
        /// </summary>
        public string SelectedRow
        {
            get => _selectedRow;
            set
            {
                if (SetProperty(ref _selectedRow, value))
                {
                    Debug.WriteLine(_selectedRow ?? "No Row Selected");
                }
            }
        }
    }
}
