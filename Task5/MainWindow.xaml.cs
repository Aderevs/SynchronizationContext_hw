using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task5
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value = await Task.Run(Addition).ConfigureAwait(false);
                result.Text = value.ToString();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ///Conclusion:
            ///Do not use ConfigureAwait(false) when you work with WPF or WinForms
        }
        private double Addition()
        {
            double x, y;
            if (double.TryParse(XValue.Text, out x) &&
                double.TryParse(YValue.Text, out y))
            {
                return x + y;
            }
            else
            {
                throw new ArgumentException("X & Y must have number value:");
            }
        }
    }
}