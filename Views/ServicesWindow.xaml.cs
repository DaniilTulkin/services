using System.Windows;

namespace services
{
    public partial class ServicesWindow : Window
    {
        public ServicesWindow()
        {
            InitializeComponent();
            DataContext = new ServiceViewModel();
        }
    }
}
