using System;
using System.Collections.Generic;
using System.IO;
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

namespace Lab2_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RegisterCommandHandlers();

            // Встановлюємо фокус на текстове поле при запуску
            Loaded += (s, e) => MainTextBox.Focus();
        }

        private void RegisterCommandHandlers()
        {
            // Команда Save
            CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Save,
                Execute_Save,
                CanExecute_Save));

            // Команда Open
            CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Open,
                Execute_Open,
                CanExecute_Open));

            // Команда Delete
            CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Delete,
                Execute_Delete,
                CanExecute_Delete));

            // Команда Copy (копіювання)
            CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Copy,
                (s, e) => { if (MainTextBox.SelectionLength > 0) MainTextBox.Copy(); },
                (s, e) => e.CanExecute = MainTextBox.SelectionLength > 0));

            // Команда Paste (вставка)
            CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Paste,
                (s, e) => MainTextBox.Paste(),
                (s, e) => e.CanExecute = Clipboard.ContainsText()));
        }

        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(MainTextBox.Text);
        }

        private void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                File.WriteAllText("document.txt", MainTextBox.Text);
                MessageBox.Show("Файл успішно збережено!", "Успіх",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка збереження: {ex.Message}", "Помилка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (File.Exists("document.txt"))
                {
                    MainTextBox.Text = File.ReadAllText("document.txt");
                }
                else
                {
                    MessageBox.Show("Файл не знайдено!", "Помилка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка відкриття: {ex.Message}", "Помилка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CanExecute_Delete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(MainTextBox.Text);
        }

        private void Execute_Delete(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Ви впевнені, що хочете очистити текст?", "Підтвердження",
                              MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MainTextBox.Clear();
            }
        }
    }
}