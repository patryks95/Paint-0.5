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
        private Ellipse elip = null;
        private Point anchorPoint;
        private Rectangle rect = null;
        private Line line = null;
        List<UIElement[]> listBack = new List<UIElement[]>();


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
            canvas.MouseMove += canvas_MouseMove;
            canvas.MouseUp += canvas_MouseUp;
            canvas.MouseDown += canvas_MouseDown;
            SizeCanvas.SelectedIndex = 0;

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
                        foreach (Button temp in ColorsPanel.Children)
                        {
                            temp.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                        }
                    }
                }
            }
        }

        private void countorButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Countor;
            fillButton.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            countorButton.Background = new SolidColorBrush(Color.FromRgb(153, 204, 255));

        }

        private void fillButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedColor = Fill;
            countorButton.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
            fillButton.Background = new SolidColorBrush(Color.FromRgb(153, 204, 255));


        }

        private void Button_Color_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedColor != null) { 
            Button button = (Button)sender;

                if (button.HasContent)
                {
                    if (button.Content is Border)
                    {
                        foreach (Button temp in ColorsPanel.Children)
                        {
                            temp.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                        }
                        button.Background = new SolidColorBrush(Color.FromRgb(153, 204, 255));

                        Border border = (Border)button.Content;
                        if (selectedColor != null)
                        {
                            Console.WriteLine(((SolidColorBrush)border.Background).Color.ToString());
                            selectedColor.Color = ((SolidColorBrush)border.Background).Color;
                        }
                    }
                }
            }

        }


        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (listBack.Count == 10)
            {
                listBack.RemoveAt(0);
            }
            UIElement[] temp = new UIElement[canvas.Children.Count];
            canvas.Children.CopyTo(temp,0);
            listBack.Add(temp);


            anchorPoint = e.MouseDevice.GetPosition(canvas);
            string sizeStroke = ((ComboBoxItem)ComboBoxSize.SelectedItem).Content.ToString();
            if (line != null)
            {
                line = new Line
                {
                    Stroke = Countor.Clone(),
                    StrokeThickness = Convert.ToInt32(sizeStroke),
                    Fill = Fill.Clone()
                };
                canvas.CaptureMouse();

                canvas.Children.Add(line);
                line.Y1 = anchorPoint.Y;
                line.X1 = anchorPoint.X;


            }
            if (rect != null)
            {
                rect = new Rectangle
                {
                    Stroke = Countor.Clone(),
                    StrokeThickness = Convert.ToInt32(sizeStroke),
                    Fill = Fill.Clone()
                };
                canvas.CaptureMouse();

                canvas.Children.Add(rect);

            }
            if (elip != null)
            {
                elip = new Ellipse
                {
                    Stroke = Countor.Clone(),
                    StrokeThickness = Convert.ToInt32(sizeStroke),
                    Fill = Fill.Clone()
                };
                canvas.CaptureMouse();

                canvas.Children.Add(elip);

            }



        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!canvas.IsMouseCaptured)
                return;
            if (line != null)
            {

                Point location = e.MouseDevice.GetPosition(canvas);
                line.Y2 = location.Y;
                line.X2 = location.X;


            }

            if (rect != null)
            {

                Point location = e.MouseDevice.GetPosition(canvas);

                double minX = Math.Min(location.X, anchorPoint.X);
                double minY = Math.Min(location.Y, anchorPoint.Y);
                double maxX = Math.Max(location.X, anchorPoint.X);
                double maxY = Math.Max(location.Y, anchorPoint.Y);

                Canvas.SetTop(rect, minY);
                Canvas.SetLeft(rect, minX);

                double height = maxY - minY;
                double width = maxX - minX;

                rect.Height = Math.Abs(height);
                rect.Width = Math.Abs(width);
            }
            if (elip != null)
            {

                Point location = e.MouseDevice.GetPosition(canvas);

                double minX = Math.Min(location.X, anchorPoint.X);
                double minY = Math.Min(location.Y, anchorPoint.Y);
                double maxX = Math.Max(location.X, anchorPoint.X);
                double maxY = Math.Max(location.Y, anchorPoint.Y);

                Canvas.SetTop(elip, minY);
                Canvas.SetLeft(elip, minX);

                double height = maxY - minY;
                double width = maxX - minX;

                elip.Height = Math.Abs(height);
                elip.Width = Math.Abs(width);
            }

        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            canvas.ReleaseMouseCapture();
            
           

        }

        private void ShapesButton_Click(object sender, RoutedEventArgs e)
        {
            line = null;
            rect = null;
            elip = null; 
            foreach(Button temp in ShapesPanel.Children)
            {
                temp.Background = new SolidColorBrush(Color.FromRgb(221,221,221));
            }
            Button button = (Button)sender;
            button.Background = new SolidColorBrush(Color.FromRgb(153, 204, 255));
            string text = button.Name.ToString();
            if (button.Name.ToString() == "LineButton")
            {
                line = new Line();
            }
            else
                if (button.Name.ToString() == "EllipseButton")
            {
                elip = new Ellipse();
            }
            else if (button.Name.ToString() == "RectangleButton")
            {
                rect = new Rectangle();
            }


        }

        private void ComboBoxSize_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void SizeCanvas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SizeCanvas.SelectedIndex == 0)
            {
                canvas.Height = 600;
                canvas.Width = 800;
            }
            if (SizeCanvas.SelectedIndex == 1)
            {
                canvas.Height = 720;
                canvas.Width = 1280;
            }
            if (SizeCanvas.SelectedIndex == 2)
            {
                canvas.Height = 768;
                canvas.Width = 1366;
            }
        }

        private void DeleteChange_Click(object sender, RoutedEventArgs e)
        {
            if (listBack.Count > 0)
            {
                canvas.Children.Clear();

                UIElement[] temp = listBack.Last();
                foreach (UIElement element in temp) {
                    canvas.Children.Add(element);
                        }
                
                listBack.RemoveAt(listBack.Count - 1);
            }
        }
    }
}
