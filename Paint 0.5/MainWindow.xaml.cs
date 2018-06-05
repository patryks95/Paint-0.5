using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace Paint_0._5
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            CultureResources.ChangeCulture(Properties.Settings.Default.DefaultCulture);


            InitializeComponent();
            UpdateStatusLabel();

        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            bool setPolishCulture = (sender == PolishItem);

            CultureResources.ChangeCulture(new CultureInfo(setPolishCulture ? "pl" : "en"));
            PolishItem.IsChecked = setPolishCulture;
            EnglishItem.IsChecked = !setPolishCulture;
            UpdateStatusLabel();

        }
        private void UpdateStatusLabel()
        {
            DateTime dt = DateTime.Now;
            StatusBarData.Text = dt.ToString("d", Properties.Resources.Culture) + "    " +
                dt.ToString("t", Properties.Resources.Culture);
        }

        private void ButtonNewColor_Click(object sender, RoutedEventArgs e)
        {

            Window okno = new Window();
            ColorCanvas temp = new ColorCanvas();
            

        }
    }
}
