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

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autorizaciya.xaml
    /// </summary>
    public partial class Autorizaciya : Page
    {
        public Autorizaciya()
        {
            InitializeComponent();
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
			int p = pbpas.Password.GetHashCode();
			
            T_Users UsersObject = ClassBase.BD.T_Users.FirstOrDefault(z => z.Login == tblogin.Text && z.Password == p);
            if (UsersObject == null)
            {
                MessageBox.Show("Не верно введены логин или пароль!");
            }
            else
            {
				switch (UsersObject.id_role)  
				{
					case 2:  // администратор                    Логин: Admin      Пароль: 99M@skin
						Class1.Mfrm.Navigate(new MainAdmin(UsersObject)); 
						break;
					case 1:  // пользователь                     Логин: Marina     Пароль: 3Ss!9ru#
						Class1.Mfrm.Navigate(new MainUser());
						break;
					default:
						break;
				}
            }
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            Class1.Mfrm.Navigate(new Registr());
        }

    }

}
