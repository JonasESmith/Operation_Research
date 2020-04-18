using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StochSolver
{
    public partial class solverForm : Form
    {
        Solver solver = new Solver();

        public solverForm()
        {
            InitializeComponent();
        }

        private void inputDataGrid_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = string.Format("Player {0}", e.Row.Index + 1);
        }

        private void StochasticForm_Load(object sender, EventArgs e)
        {
            inputDataGrid.Rows.Add("Player 1", 0.83, 0.92, 0.91);
            inputDataGrid.Rows.Add("Player 2", 0.89, 0.83, 0.85);
            inputDataGrid.Rows.Add("Player 3", 0.91, 0.93, 0.93);

            solver.contractors.Add(new Contractor("Player 1", new List<double> { 0.83, 0.92, 0.91 }));
            solver.contractors.Add(new Contractor("Player 2", new List<double> { 0.89, 0.83, 0.85 }));
            solver.contractors.Add(new Contractor("Player 3", new List<double> { 0.91, 0.93, 0.93 }));
        }

        // Cell validation.
        private void inputDataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue == null || e.FormattedValue.ToString() == "") return;

            string headerText = inputDataGrid.Columns[e.ColumnIndex].HeaderText;

            double value;
            if (headerText.Contains("Component"))
                // Value must be a double that is between 0 and 1.
                if (!double.TryParse(e.FormattedValue.ToString(), out value) || value < 0.0 || value > 1.0)
                {
                    MessageBox.Show("Value must be a double between 0 and 1!");
                    e.Cancel = true;
                }
        }

        // Cell value changes.
        private void inputDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow row = inputDataGrid.Rows[e.RowIndex];

                string name = row.Cells[0].Value.ToString();

                // Probabilities in column configuration.
                List<double> probabilities = new List<double>();
                for (int i = 1; i < inputDataGrid.ColumnCount; i++)
                {
                    double prob;
                    double.TryParse((row.Cells[i].Value ?? "0.0").ToString(), out prob);
                    probabilities.Add(prob);
                }

                // Store this created contractor for later.
                if (solver.contractors.Count == e.RowIndex)
                {
                    // Create a new contractor.
                    Contractor contracter = new Contractor(name, probabilities);
                    solver.contractors.Add(contracter);
                    clearBackgroundColorOfCells();
                }
                else
                    // Update the old contractor's values.
                    solver.contractors[e.RowIndex].probabilities = probabilities;
            }
        }

        private void inputDataGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            solver.contractors.RemoveAt(e.RowIndex);
            clearBackgroundColorOfCells();
        }

        // Solve button \\

        private void solveButton_Click(object sender, EventArgs e)
        {
            clearBackgroundColorOfCells();

            List<int> result = solver.solve();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] >= 0)
                    inputDataGrid.Rows[result[i]].Cells[i + 1].Style.BackColor = Color.Green;
            }
        }

        // Add && Remove buttons \\

        private void addColumnButton_Click(object sender, EventArgs e)
        {
            string headerText = string.Format("Component {0}", inputDataGrid.Columns.Count);
            string name = string.Format("component{0}Column", inputDataGrid.Columns.Count);

            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = name;
            column.HeaderText = headerText;
            column.Width = 80;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            inputDataGrid.Columns.Add(column);

            foreach (Contractor contracter in solver.contractors)
            {
                contracter.probabilities.Add(0);
            }
        }

        private void removeColumnButton_Click(object sender, EventArgs e)
        {
            inputDataGrid.Columns.Remove(inputDataGrid.Columns.GetLastColumn(DataGridViewElementStates.None, DataGridViewElementStates.ReadOnly));

            foreach (Contractor contracter in solver.contractors)
                contracter.probabilities.RemoveAt(contracter.probabilities.Count - 1);
        }

        /***********************/
        /* Helpers             */
        /***********************/

        private void clearBackgroundColorOfCells()
        {
            foreach (DataGridViewRow row in inputDataGrid.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                    if (!cell.ReadOnly)
                        cell.Style.BackColor = Color.White;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
