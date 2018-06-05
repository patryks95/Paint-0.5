using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private SolidColorBrush selectedColor = null;
        private SolidColorBrush countor =new SolidColorBrush(Color.FromRgb(54, 5, 8));
        private SolidColorBrush fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        public SolidColorBrush Countor
        {
            get { return countor; }
            set { countor.Color = value.Color; }
        }
        public SolidColorBrush Fill {
            get { return fill; }
            set { fill.Color = value.Color;

            }
        }

        public SolidColorBrush SelectedColor
        {
            get { return selectedColor; }
            set { selectedColor = value; }
        }

        public MainWindow()
        {
            CultureResources.ChangeCulture(Properties.Settings.Default.DefaultCulture);
            
            InitializeComponent();
            UpdateStatusLabel();
            fillBorder.Background = Fill;
            countorBorder.Background = Countor;
            ComboBoxSize.SelectedIndex = 0;
            ComboBoxType.SelectedIndex = 0;

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

            ColorSelector selector = new ColorSelector();
            
            if (selector.ShowDialog()==false)
            {
                if (selector.selectedColor.HasValue)
                {
                    if (SelectedColor != null)
                    {
                        SelectedColor.Color = selector.selectedColor.Value;
                    }
                }
            }
        }

        private void countorButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Countor;
        }

        private void fillButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Fill;
        }

        private void Button_Color_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.HasContent) {
                if(button.Content is Border)
                {

                    Border border = (Border) button.Content;
                    if (selectedColor != null)
                    {
                        Console.WriteLine(((SolidColorBrush)border.Background).Color.ToString());
                        selectedColor.Color = ((SolidColorBrush)border.Background).Color; 
                    }
                }
            }

        }
    }
}
