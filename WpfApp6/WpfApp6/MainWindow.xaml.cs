using System.Windows;

namespace WpfApp6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTheme_Click(object sender, RoutedEventArgs e)
        {
            ThemeHelper.Toggle();
        }
    }
}