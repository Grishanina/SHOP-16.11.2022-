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

namespace WpfApp1
{
	/// <summary>
	/// Логика взаимодействия для MainAdmin.xaml
	/// </summary>
	public partial class MainAdmin : Page
	{

		T_Users user; 
		public MainAdmin(T_Users user)
		{
			InitializeComponent();
			this.user = user;
			dgUsers.ItemsSource = ClassBase.BD.T_Users.ToList();
		}

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
			Class1.Mfrm.Navigate(new Product());
		}

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
			Class1.Mfrm.Navigate(new Profile(user));
		}

        private void btnPrivateUser_Click(object sender, RoutedEventArgs e)
        {
			dgUsers.Visibility = Visibility.Visible;
			btnSpUsers.Visibility = Visibility.Collapsed;
			btnPrivateUser.Visibility = Visibility.Visible;
		}

        private void btnSpUsers_Click(object sender, RoutedEventArgs e)
        {
			dgUsers.Visibility = Visibility.Visible;
			btnSpUsers.Visibility = Visibility.Collapsed;
			btnPrivateUser.Visibility = Visibility.Visible;
		}
    }
}
