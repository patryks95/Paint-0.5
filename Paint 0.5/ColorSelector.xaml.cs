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

namespace Paint_0._5
{
    /// <summary>
    /// Logika interakcji dla klasy ColorSelector.xaml
    /// </summary>
    public partial class ColorSelector : Window
    {
        public Color? selectedColor;

        public ColorSelector()
        {
            InitializeComponent();
        
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            selectedColor = null;
            this.Close();
        }

        private void Selected_Click(object sender, RoutedEventArgs e)
        {
            selectedColor = selector.SelectedColor;
            this.Close();
        }
    }
}
