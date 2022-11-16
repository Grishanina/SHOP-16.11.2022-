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
using System.Windows.Shapes;

namespace WpfApp1
{
	/// <summary>
	/// Логика взаимодействия для WindowProfile.xaml
	/// </summary>
	public partial class WindowProfile : Window
	{
		T_Users user;
		public WindowProfile(T_Users user)
		{
			InitializeComponent();
			this.user = user;
			tbName.Text = user.Name;
			tbSurname.Text = user.Surname;
			tbPatronymic.Text = user.Patronymic;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			user.Name = tbName.Text;  // изменяем имя пользователя в БД
			user.Surname = tbSurname.Text;  // изменяем фамилию пользователя в БД
			tbPatronymic.Text = user.Patronymic;
			ClassBase.BD.SaveChanges();  // сохраняем изменения в БД
			this.Close();  // закрываем это окно
		}
	}
}
