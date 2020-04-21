namespace OptGui.ViewModels
{
    using OptGui.Services;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="MainWindowViewModel" />.
    /// </summary>
    public class MainWindowViewModel : BindableBase
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

        /// <summary>
        /// Defines the _maxValue.
        /// </summary>
        private double? _maxValue;

        /// <summary>
        /// Gets or sets the MaxValue.
        /// </summary>
        public double? MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                this.RaisePropertyChanged("MaxValue");
                CommandSolve.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Defines the _optimalValue.
        /// </summary>
        private double? _optimalValue;

        /// <summary>
        /// Gets or sets the OptimalValue.
        /// </summary>
        public double? OptimalValue
        {
            get => _optimalValue;
            set
            {
                _optimalValue = value;
                this.RaisePropertyChanged("OptimalValue");
            }
        }

        /// <summary>
        /// Defines the _optimalCounts.
        /// </summary>
        private DataTable _optimalCounts;

        /// <summary>
        /// Gets or sets the OptimalCounts.
        /// </summary>
        public DataTable OptimalCounts
        {
            get => _optimalCounts;
            set
            {
                _optimalCounts = value;
                this.RaisePropertyChanged("OptimalCounts");
            }
        }

        /// <summary>
        /// Gets or sets the NumericalFallback.
        /// </summary>
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
            CommandRemoveSelectedRow = new DelegateCommand(RemoveSelectedRow, CanRemoveSelectedRow);
            this.Name = null;
            this.Weight = null;
            this.Value = null;
            this.MaxValue = null;
            this.OptimalValue = null;
            this.RecursiveReturnsObservableCollections = new ObservableCollection<ObservableCollection<double>>();
            this.DecisionObservableCollections = new ObservableCollection<ObservableCollection<int>>();
        }

        /// <summary>
        /// The AddRow.
        /// </summary>
        internal void AddRow()
        {
            this.Rows.Add(new KnapsackRow(this.Name, this.Weight, this.Value));
            CommandSolve.RaiseCanExecuteChanged();
            ResetVals();
        }

        /// <summary>
        /// The ResetVals.
        /// </summary>
        internal void ResetVals()
        {
            this.Name = null;
            this.Weight = null;
            this.Value = null;
        }

        /// <summary>
        /// The CanAddRow.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        internal bool CanAddRow()
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
        internal void Solve()
        {
            List<List<double>> nRows = new List<List<double>>();

            for (int i = 0; i < this.Rows.Count; i++)
            {
                int divisible = 0;
                List<double> nRow = new List<double>();
                for (int j = 0; j < (int)this.MaxValue + 1; j++)
                {
                    if (j != 0 && j % this.Rows[i].Weight.Value == 0)
                    {
                        divisible++;
                    }
                    nRow.Add(Rows[i].Value.Value * divisible);
                }
                nRows.Add(nRow);
            }

            List<List<double>> mRows = new List<List<double>>();
            List<List<int>> dRows = new List<List<int>>();
            int fIndex = nRows.Count - 1;
            int mIndex = -1;
            List<double> maxes = new List<double>();
            List<int> decisions = new List<int>();
            List<double> mRow = new List<double>();

            // Initialize a dummy list to start running the recursive process.
            for (int i = 0; i <= this.MaxValue.Value; i++) { mRow.Add(0); }
            Recurse(nRows[nRows.Count - 1], mRow, (int)this.MaxValue.Value, ref maxes, ref decisions);
            mRows.Add(maxes);
            dRows.Add(decisions);

            while (fIndex > 0)
            {
                fIndex--;
                mIndex++;
                maxes = new List<double>();
                decisions = new List<int>();
                Recurse(nRows[fIndex], mRows[mIndex], (int)this.MaxValue.Value, ref maxes, ref decisions);
                mRows.Add(maxes);
                dRows.Add(decisions);
            }
            mRows.Reverse();
            dRows.Reverse();

            foreach (var row in mRows)
            {
                RecursiveReturnsObservableCollections.Add(new ObservableCollection<double>(row));
            }
            foreach (var row in dRows)
            {
                DecisionObservableCollections.Add(new ObservableCollection<int>(row));
            }
            var table = new ObservableCollection<ObservableCollection<string>>();
            OptimalValue = mRows[0][mRows[0].Count - 1];
            ChooseOptimalPolicy(mRows, dRows);
            SetList(mRows, dRows);
        }

        /// <summary>
        /// The SetList.
        /// </summary>
        /// <param name="mRows">The mRows<see cref="List{List{double}}"/>.</param>
        /// <param name="dRows">The dRows<see cref="List{List{int}}"/>.</param>
        internal void SetList(List<List<double>> mRows, List<List<int>> dRows)
        {
            DataTable dt = new DataTable();
            List<object[]> mObjList = new List<object[]>();
            List<object[]> dObjList = new List<object[]>();
            dt.Columns.Add("Functions");
            for (int i = 0; i < mRows[0].Count; i++)
            {
                dt.Columns.Add($"{i}");
            }
            for (int i = 0; i < mRows.Count; i++)
            {
                object[] mrow = new object[mRows[i].Count];
                object[] drow = new object[mRows[i].Count];
                mrow[0] = $"m:{i + 1}";
                drow[0] = $"d:{i + 1}";
                for (int j = 1; j < mRows[i].Count; j++)
                {
                    if (i == 0 && j != mRows[i].Count - 1)
                    {
                        mrow[j] = "*";
                        drow[j] = "*";
                    }
                    else
                    {
                        mrow[j] = mRows[i][j].ToString();
                        drow[j] = dRows[i][j].ToString();
                    }
                }
                mObjList.Add(mrow);
                dObjList.Add(drow);
            }
            mObjList.Reverse();
            dObjList.Reverse();
            for (int i = 0; i < mObjList.Count; i++)
            {
                dt.Rows.Add(mObjList[i]);
                dt.Rows.Add(dObjList[i]);
            }
            this.StageTable = dt;
        }

        /// <summary>
        /// The Recurse.
        /// </summary>
        /// <param name="nRow">The nRow<see cref="List{double}"/>.</param>
        /// <param name="mRow">The mRow<see cref="List{double}"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="maxes">The maxes<see cref="List{double}"/>.</param>
        /// <param name="decisions">The decisions<see cref="List{int}"/>.</param>
        internal void Recurse(List<double> nRow, List<double> mRow, int index, ref List<double> maxes, ref List<int> decisions)
        {
            if (index > -1)
            {
                List<double> values = new List<double>();
                for (int i = 0; i <= index; i++)
                {
                    values.Add(nRow[i] + mRow[index - i]);
                }
                int maxIndex = values.IndexOf(values.Max());
                maxes.Add(values[maxIndex]);
                decisions.Add(maxIndex);
                Recurse(nRow, mRow, index - 1, ref maxes, ref decisions);
            }
            else
            {
                // When we're finished, we reverse the lists for consistency with the book.
                maxes.Reverse();
                decisions.Reverse();
            }
        }

        /// <summary>
        /// The ChooseOptimalPolicy.
        /// </summary>
        /// <param name="mRows">The mRows<see cref="List{List{double}}"/>.</param>
        /// <param name="dRows">The dRows<see cref="List{List{double}}"/>.</param>
        internal void ChooseOptimalPolicy(List<List<double>> mRows, List<List<int>> dRows)
        {
            double copyLimit = this.MaxValue.Value;
            DataTable dt = new DataTable();
            dt.Columns.Add($"Optimal Value");
            foreach (var r in this.Rows)
            {
                try
                {
                    dt.Columns.Add($"{r.Name}");
                }
                catch (DuplicateNameException dne)
                {
                    dt.Columns.Add($"{r.Name}(copy)");
                }              
            }     
            List<double> itemsCount = new List<double>();
            for (int i = 0; i < mRows.Count; i++)
            {
                double weight = (double)dRows[i][(int)copyLimit];
                copyLimit -= weight;
                itemsCount.Add(weight / this.Rows[i].Weight.Value);
            }
            object[] row = new object[dt.Columns.Count];
            row[0] = this.OptimalValue.Value.ToString();
            for (int i = 1; i <= itemsCount.Count; i++)
            {
                row[i] = itemsCount[i - 1];
            }
            dt.Rows.Add(row);

            this.OptimalCounts = dt;
        }

        /// <summary>
        /// The CanSolve.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        internal bool CanSolve()
        {
            return this.Rows.Count > 0 && this.MaxValue.HasValue;
        }

        /// <summary>
        /// The RemoveSelectedRow.
        /// </summary>
        internal void RemoveSelectedRow()
        {
            Rows.RemoveAt(Rows.IndexOf(SelectedRow));
            SelectedRow = null;
            CommandRemoveSelectedRow.RaiseCanExecuteChanged();
            CommandSolve.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// The CanRemoveSelectedRow.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        internal bool CanRemoveSelectedRow()
        {
            return SelectedRow != null;
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
        public ObservableCollection<ObservableCollection<int>> DecisionObservableCollections { get; set; }

        /// <summary>
        /// Gets or sets the RecursiveReturnsObservableCollections.
        /// </summary>
        public ObservableCollection<ObservableCollection<double>> RecursiveReturnsObservableCollections { get; set; }

        /// <summary>
        /// Gets the StageTable..
        /// </summary>
        internal DataTable _stageTable;

        /// <summary>
        /// Gets or sets the StageTable.
        /// </summary>
        public DataTable StageTable
        {
            get => _stageTable;
            set
            {
                _stageTable = value;
                this.RaisePropertyChanged("StageTable");
            }
        }

        /// <summary>
        /// Defines the _selectedRow.
        /// </summary>
        private KnapsackRow _selectedRow = null;

        /// <summary>
        /// Gets or sets the SelectedRow.
        /// </summary>
        public KnapsackRow SelectedRow
        {
            get => _selectedRow;
            set
            {
                if (SetProperty(ref _selectedRow, value))
                {
                    Debug.WriteLine($"{ _selectedRow}" ?? "No Row Selected");
                }
                CommandRemoveSelectedRow.RaiseCanExecuteChanged();
            }
        }
    }
}
