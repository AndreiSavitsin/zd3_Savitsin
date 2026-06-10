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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ClassLibrary;

namespace zd3_Savitsin_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Quickly_Click(object sender, RoutedEventArgs e) //Кнопка меню: Срочная дорожная работа
        {
            panel2.Visibility = Visibility.Hidden;
            panel4.Visibility = Visibility.Visible;
        }

        private void Unquickly_Click(object sender, RoutedEventArgs e) //Кнопка меню: Несрочная дорожная работа
        {
            panel2.Visibility = Visibility.Visible;
            panel4.Visibility = Visibility.Hidden;
        }

        private void Exit_Click(object sender, RoutedEventArgs e) //Кнопка меню: Выход
        {
            this.Close();
        }
        private void Add_Click(object sender, RoutedEventArgs e) //Кнопка добавить для несрочной дорожной работы
        {
            try
            {
                Roadworks.AddRoadWork(txtName.Text, txtWidth.Text, txtLength.Text, txtWeight.Text, txtSurface.Text);
                FillListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e) //Кнопка удалить для несрочной дорожной работы
        {
            if (listBox.SelectedIndex != -1)
            {
                Roadworks.DeleteRoadWork(Roadworks.listWorks[listBox.SelectedIndex].RoadName);
                FillListBox();
            }
            else
            {
                MessageBox.Show("Выберите элемент в листбоксе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Calculate_Click(object sender, RoutedEventArgs e) //Кнопка высчитать для несрочной дорожной работы
        {
            if (listBox.SelectedIndex != -1)
            {
                double result =  Roadworks.listWorks[listBox.SelectedIndex].CalculateQ();
                MessageBox.Show($"Полная масса дорожного покрытия: {result}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Выберите элемент в листбоксе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void FillListBox() //Заполнение листбокса
        {
            listBox.Items.Clear();
            foreach (var item in Roadworks.listWorks)
            {
                listBox.Items.Add(item.GetInfo());
            }
        }
        private void Add2_Click(object sender, RoutedEventArgs e) //Кнопка добавить для срочной дорожной работы
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Название дороги не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!double.TryParse(txtWidth.Text, out double CorrectWidth))
            {
                MessageBox.Show("Неверный тип данных ширины дороги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(txtLength.Text, out double CorrectLength))
            {
                MessageBox.Show("Неверный тип данных длины дороги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(txtWeight.Text, out double CorrectWeight))
            {
                MessageBox.Show("Неверный тип данных массы дорожного покрытия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtSurface.Text))
            {
                MessageBox.Show("Тип дорожного покрытия не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(txtFactor.Text, out int factor))
            {
                MessageBox.Show("Коэффициент прочности должен быть целым числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (string.IsNullOrEmpty(txtWeather.Text))
            {
                MessageBox.Show("Погода не может быть пустой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(txtCountDays.Text, out int countDays))
            {
                MessageBox.Show("Количество дней должно быть целым числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (countDays < 0 && countDays > 10)
            {
                MessageBox.Show("Количество дней должно быть от 0 до 10", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RoadCalculations roadworks = new RoadCalculations(txtName.Text, double.Parse(txtWidth.Text), double.Parse(txtLength.Text),
                double.Parse(txtWeight.Text), txtSurface.Text, factor, txtWeather.Text, countDays);

            try
            {
                Roadworks.AddRoadWork(roadworks);
                FillListBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete2_Click(object sender, RoutedEventArgs e) //Кнопка удалить для срочной дорожной работы
        {
            if (listBox.SelectedIndex != -1)
            {
                Roadworks.DeleteRoadWork(listBox.SelectedIndex);
                FillListBox();
            }
            else
            {
                MessageBox.Show("Выберите элемент в листбоксе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Calculate2_Click(object sender, RoutedEventArgs e) //Кнопка высчитать для срочной дорожной работы
        {
            if (listBox.SelectedIndex != -1)
            {
                double result = Roadworks.listWorks[listBox.SelectedIndex].CalculateQ();
                MessageBox.Show($"Полная масса дорожного покрытия: {result}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Выберите элемент в листбоксе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
