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
using CompanyDemo.Infrastructure.Data.Model;
using CompanyDemo.Presentation.ViewModels;

namespace CompanyDemo.Presentation.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MainWindowViewModel viewModel;
    public MainWindow()
    {
        InitializeComponent();

        DataContext = viewModel = new MainWindowViewModel()
        {
            ShowOrderDetails = OpenOrderDetailsWindow,
            ShowMessage = message => MessageBox.Show(message)
        };
    }

    private void OpenOrderDetailsWindow()
    {
        new OrderDetailsWindow(viewModel.SelectedOrder).Show();
    }
}