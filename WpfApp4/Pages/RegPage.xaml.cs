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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {

        

        public RegPage()
        {
            InitializeComponent();
            DataContext = new Account();
        }

        private void Reg(object sender, RoutedEventArgs e)
        {
            try {
                MainWindow.Connection.Accounts.Add(DataContext as Account);
                MainWindow.Connection.SaveChanges();

                NavigationService.Navigate(new Author());
                }
            catch(Exception ex)
            {
                MessageBox.Show("Не все поля заполнены");
            }
            
        }

        private void GoReg(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Author());
        }
    }
}
