using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        T_Users user;
        public Profile(T_Users user)
        {
            InitializeComponent();
            this.user = user; 
            tbName.Text = user.Name; 
            tbSurname.Text = user.Surname;
			tbPatronymic.Text = user.Patronymic;
			List<T_Userphoto> u = ClassBase.BD.T_Userphoto.Where(x => x.id_user == user.id_user).ToList();
			if (u != null)
			{
				byte[] Bar = u[u.Count - 1].photoBinary;
				showImage(Bar, imUser);
			}
		}

        private void showImage(byte[] Barray, System.Windows.Controls.Image img)
        {
			BitmapImage BI = new BitmapImage();
			using (MemoryStream m = new MemoryStream(Barray)) 
			{
				BI.BeginInit();  
				BI.StreamSource = m;  
				BI.CacheOption = BitmapCacheOption.OnLoad;  
				BI.EndInit();  
			}
			img.Source = BI;  
			img.Stretch = Stretch.Uniform;
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			WindowProfile windowProfile = new WindowProfile(user);  
			windowProfile.ShowDialog(); 
			Class1.Mfrm.Navigate(new Profile(user));
		}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
			try
			{
				T_Userphoto u = new T_Userphoto();  // создание объекта для добавления записи в таблицу, где хранится фото
				u.id_user = user.id_user;  // присваиваем значение полю idUser (id авторизованного пользователя)

				OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
															//OFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);  // выбор папки для открытия
				OFD.ShowDialog();  // открываем диалоговое окно             
				string path = OFD.FileName;  // считываем путь выбранного изображения
				System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);  // создаем объект для загрузки изображения в базу
				ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
				byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
				u.photoBinary = Barray;  // заполяем поле photoBinary полученным байтовым массивом
				ClassBase.BD.T_Userphoto.Add(u);  // добавляем объект в таблицу БД
				ClassBase.BD.SaveChanges();  // созраняем изменения в БД
				MessageBox.Show("Фото добавлено");
				Class1.Mfrm.Navigate(new Profile(user)); // перезагружаем страницу

			}
			catch
			{
				MessageBox.Show("ОШИБКА");
			}
		}

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
			try
			{
				OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
				OFD.Multiselect = true;  // открытие диалогового окна с возможностью выбора нескольких элементов
				if (OFD.ShowDialog() == true)  // пока диалоговое окно открыто, будет в цикле записывать каждое выбранное изображение в БД
				{
					foreach (string file in OFD.FileNames)  // цикл организован по именам выбранных файлов
					{
						T_Userphoto u = new T_Userphoto();  // создание объекта для добавления записи в таблицу, где хранится фото
						u.id_user = user.id_user;  // присваиваем значение полю idUser (id авторизованного пользователя)
						string path = file;  // считываем путь выбранного изображения
						System.Drawing.Image SDI = System.Drawing.Image.FromFile(file);  // создаем объект для загрузки изображения в базу
						ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
						byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
						u.photoBinary = Barray;  // заполяем поле photoBinary полученным байтовым массивом
						ClassBase.BD.T_Userphoto.Add(u);  // добавляем объект в таблицу БД
					}
					ClassBase.BD.SaveChanges();
					MessageBox.Show("Фото добавлены");
				}
			}
			catch
			{
				MessageBox.Show("ОШИБКА");
			}
		}

		int n = 0;
		private void Button_Click_3(object sender, RoutedEventArgs e)
        {
			spGallery.Visibility = Visibility.Visible;
			List<T_Userphoto> u = ClassBase.BD.T_Userphoto.Where(x => x.id_user == user.id_user).ToList();
			if (u != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
			{

				byte[] Bar = u[n].photoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
				showImage(Bar, imgGallery);  // отображаем картинку
			}
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			List<T_Userphoto> u = ClassBase.BD.T_Userphoto.Where(x => x.id_user == user.id_user).ToList();
			n--;
			if (Next.IsEnabled == false)
			{
				Next.IsEnabled = true;
			}
			if (u != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
			{

				byte[] Bar = u[n].photoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных)
				BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
				showImage(Bar, imgGallery);
			}
			if (n == 0)
			{
				Back.IsEnabled = false;
			}
		}

		private void Next_Click(object sender, RoutedEventArgs e)
		{
			List<T_Userphoto> u = ClassBase.BD.T_Userphoto.Where(x => x.id_user == user.id_user).ToList();
			n++;
			if (Back.IsEnabled == false)
			{
				Back.IsEnabled = true;
			}
			if (u != null)  
			{

				byte[] Bar = u[n].photoBinary;   
				showImage(Bar, imgGallery);
			}
			if (n == u.Count - 1)
			{
				Next.IsEnabled = false;
			}
		}

		private void btnOld_Click(object sender, RoutedEventArgs e)
		{
			List<T_Userphoto> u = ClassBase.BD.T_Userphoto.Where(x => x.id_user == user.id_user).ToList();
			byte[] Bar = u[n].photoBinary;  
			showImage(Bar, imUser);  
		}
	}
}
