namespace OptGui.Services
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    /// <summary>
    /// Defines the <see cref="IProblemType1918" />.
    /// </summary>
    public interface IProblemType1918
    {
        /// <summary>
        /// Gets or sets the Rows.
        /// </summary>
        ObservableCollection<KnapsackRow> Rows { get; set; }

        /// <summary>
        /// Gets or sets the StageCount.
        /// </summary>
        int StageCount { set; get; }

        /// <summary>
        /// Gets or sets the DecisionObservableCollections.
        /// </summary>
        ObservableCollection<ObservableCollection<double>> DecisionObservableCollections { get; set; }

        /// <summary>
        /// Gets or sets the RecursiveReturnsObservableCollections.
        /// </summary>
        ObservableCollection<ObservableCollection<double>> RecursiveReturnsObservableCollections { get; set; }

        /// <summary>
        /// Gets the StageTable.
        /// </summary>
        ObservableCollection<ObservableCollection<double>> StageTable { get; }

        /// <summary>
        /// Gets the OptimalPolicy.
        /// </summary>
        double OptimalPolicy { get; }
    }

    /// <summary>
    /// Defines the <see cref="KnapsackSolver" />.
    /// </summary>
    public class KnapsackSolver : IProblemType1918
    {
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
        /// Initializes a new instance of the <see cref="KnapsackSolver"/> class.
        /// </summary>
        public KnapsackSolver()
        {
            this.Rows = new ObservableCollection<KnapsackRow>();
        }

        /// <summary>
        /// Adds a  <see cref="KnapsackRow"/> to the ObservableCollection of data points.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="weight">The weight<see cref="double"/>.</param>
        /// <param name="value">The value<see cref="double"/>.</param>
        public void AddRow(string name, double weight, double value)
        {
            this.Rows.Add(new KnapsackRow(name, weight, value));
        }

        /// <summary>
        /// Removes a <see cref="KnapsackRow"/> from the ObservableCollection of data points by a given index.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        public void RemoveRow(int index)
        {
            this.Rows.RemoveAt(index);
        }
    }
}
