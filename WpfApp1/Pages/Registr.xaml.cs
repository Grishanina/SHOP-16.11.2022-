using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : Page
    {
        public Registr()
        {
            InitializeComponent();

			cbRole.ItemsSource = ClassBase.BD.T_Role.ToList();
			cbRole.SelectedValuePath = "id_role";  // 
			cbRole.DisplayMemberPath = "Role";
			cbRole.SelectedIndex = 0;
		}

		private void Btn33_Click(object sender, RoutedEventArgs e)
		{
			Class1.Mfrm.GoBack();
		}
		private void Btn22_Click(object sender, RoutedEventArgs e)
		{
			int g = 0;
			if (rbMen.IsChecked == true) g = 1;
			if (rbWomen.IsChecked == true) g = 2;

			//проверка пароля на соответствие требованиям
			var password = pbpas.Password;
			Regex regex = new Regex("(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8,}$");
			if (regex.IsMatch(password))
			{
				T_Users UsersObject = new T_Users()
			   {
				Surname = tbsurname.Text,
				Name = tbname.Text,
				Patronymic = tbpatronymic.Text,
				Birthday = Convert.ToDateTime(tbbirthday.SelectedDate),
				Login = tblogin.Text,
				Password = pbpas.Password.GetHashCode(),
				id_pol = g,
				id_role = cbRole.SelectedIndex + 1
			   };

			ClassBase.BD.T_Users.Add(UsersObject);
			ClassBase.BD.SaveChanges();
			MessageBox.Show("Пользователь добавлен");
			Class1.Mfrm.GoBack();
			}
			else
			{
				//требования к паролю
				MessageBox.Show("Пароль должен содержать: \n" +
					" - минимум 1 заглавную латинскую букву; \n" +
					" - минимум 3 строчные латинские буквы; \n" +
					" - минимум 2 цыфры; \n" +
					" - минимум 1 спец. символ (!@#$%^&*()+=); \n" +
					" - минимум 8 символов."
					);

			}

		}
	}
}
