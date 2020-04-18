using System;

namespace StochSolver
{
    partial class solverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            this.inputDataGrid = new System.Windows.Forms.DataGridView();
            this.locationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.probabilityFirstColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.probabilitySecondColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.probabilityThirdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.solveButton = new System.Windows.Forms.Button();
            this.addColumn = new System.Windows.Forms.Button();
            this.removeColumn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.stochasticPage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGrid)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.stochasticPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputDataGrid
            // 
            this.inputDataGrid.AllowUserToResizeColumns = false;
            this.inputDataGrid.AllowUserToResizeRows = false;
            this.inputDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.locationColumn,
            this.probabilityFirstColumn,
            this.probabilitySecondColumn,
            this.probabilityThirdColumn});
            this.inputDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputDataGrid.Location = new System.Drawing.Point(3, 30);
            this.inputDataGrid.Name = "inputDataGrid";
            this.inputDataGrid.Size = new System.Drawing.Size(428, 152);
            this.inputDataGrid.TabIndex = 0;
            this.inputDataGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.inputDataGrid_CellValidating);
            this.inputDataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.inputDataGrid_CellValueChanged);
            this.inputDataGrid.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.inputDataGrid_DefaultValuesNeeded);
            this.inputDataGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.inputDataGrid_RowsRemoved);
            // 
            // locationColumn
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locationColumn.DefaultCellStyle = dataGridViewCellStyle25;
            this.locationColumn.HeaderText = "";
            this.locationColumn.Name = "locationColumn";
            this.locationColumn.ReadOnly = true;
            this.locationColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.locationColumn.Width = 60;
            // 
            // probabilityFirstColumn
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.probabilityFirstColumn.DefaultCellStyle = dataGridViewCellStyle26;
            this.probabilityFirstColumn.HeaderText = "Component 1";
            this.probabilityFirstColumn.MaxInputLength = 15;
            this.probabilityFirstColumn.Name = "probabilityFirstColumn";
            this.probabilityFirstColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.probabilityFirstColumn.Width = 80;
            // 
            // probabilitySecondColumn
            // 
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.probabilitySecondColumn.DefaultCellStyle = dataGridViewCellStyle27;
            this.probabilitySecondColumn.HeaderText = "Component 2";
            this.probabilitySecondColumn.MaxInputLength = 15;
            this.probabilitySecondColumn.Name = "probabilitySecondColumn";
            this.probabilitySecondColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.probabilitySecondColumn.Width = 80;
            // 
            // probabilityThirdColumn
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.probabilityThirdColumn.DefaultCellStyle = dataGridViewCellStyle28;
            this.probabilityThirdColumn.HeaderText = "Component 3";
            this.probabilityThirdColumn.MaxInputLength = 15;
            this.probabilityThirdColumn.Name = "probabilityThirdColumn";
            this.probabilityThirdColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.probabilityThirdColumn.Width = 80;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input Data";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // solveButton
            // 
            this.solveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.solveButton.Location = new System.Drawing.Point(158, 0);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(112, 23);
            this.solveButton.TabIndex = 2;
            this.solveButton.Text = "Solve";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // addColumn
            // 
            this.addColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addColumn.Location = new System.Drawing.Point(0, 0);
            this.addColumn.Name = "addColumn";
            this.addColumn.Size = new System.Drawing.Size(119, 27);
            this.addColumn.TabIndex = 3;
            this.addColumn.Text = "Add Component";
            this.addColumn.UseVisualStyleBackColor = true;
            this.addColumn.Click += new System.EventHandler(this.addColumnButton_Click);
            // 
            // removeColumn
            // 
            this.removeColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeColumn.Location = new System.Drawing.Point(0, 0);
            this.removeColumn.Name = "removeColumn";
            this.removeColumn.Size = new System.Drawing.Size(119, 27);
            this.removeColumn.TabIndex = 4;
            this.removeColumn.Text = "Remove Component";
            this.removeColumn.UseVisualStyleBackColor = true;
            this.removeColumn.Click += new System.EventHandler(this.removeColumnButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.stochasticPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(442, 211);
            this.tabControl1.TabIndex = 5;
            // 
            // stochasticPage
            // 
            this.stochasticPage.Controls.Add(this.panel7);
            this.stochasticPage.Controls.Add(this.inputDataGrid);
            this.stochasticPage.Controls.Add(this.panel1);
            this.stochasticPage.Location = new System.Drawing.Point(4, 22);
            this.stochasticPage.Name = "stochasticPage";
            this.stochasticPage.Padding = new System.Windows.Forms.Padding(3);
            this.stochasticPage.Size = new System.Drawing.Size(434, 185);
            this.stochasticPage.TabIndex = 0;
            this.stochasticPage.Text = "Stochastic";
            this.stochasticPage.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 27);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.addColumn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(119, 27);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(119, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(39, 27);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.removeColumn);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(309, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(119, 27);
            this.panel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(270, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(39, 27);
            this.panel5.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(158, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(112, 27);
            this.panel6.TabIndex = 3;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.solveButton);
            this.panel7.Controls.Add(this.panel9);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(3, 159);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(428, 23);
            this.panel7.TabIndex = 3;
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(158, 23);
            this.panel8.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(270, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(158, 23);
            this.panel9.TabIndex = 1;
            // 
            // solverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 211);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(455, 250);
            this.Name = "solverForm";
            this.ShowIcon = false;
            this.Text = "Solver";
            this.Load += new System.EventHandler(this.StochasticForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGrid)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.stochasticPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView inputDataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn probabilityFirstColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn probabilitySecondColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn probabilityThirdColumn;
        private System.Windows.Forms.Button addColumn;
        private System.Windows.Forms.Button removeColumn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage stochasticPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
    }
}

