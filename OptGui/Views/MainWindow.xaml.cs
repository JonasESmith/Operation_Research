using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OptGui.Services;

namespace OptGui.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            // Resize ListView Columns Equally Proportional
            double remainingSpace = DataList.ActualWidth;

            if (remainingSpace > 0)
            {
                double spacing = Math.Ceiling(remainingSpace / (DataList.View as GridView).Columns.Count - 1);
                foreach (var col in (DataList.View as GridView).Columns)
                {
                    col.Width = spacing;
                }
            }
        }
    }
}
