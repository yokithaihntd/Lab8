using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace App1
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IDbRepository<Film> _repository;
        public MainPage()
        {
            this.InitializeComponent();
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .Options;
            var dbContext = new DataContext(dbContextOptions);
            _repository = new FilmRepository<Film>(dbContext);
            _ = LoadData();
        }
        private async Task LoadData()
        {
            var films = await _repository.GetAll().ToListAsync();
            filmListView.ItemsSource = films;
        }

        private async void AddFilmButton_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string director = txtDirector.Text;
            string writer = txtWriter.Text;
            string genre = txtGenre.Text;
            int year = 0;
            if (!int.TryParse(txtYear.Text, out year) || string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(director) || string.IsNullOrWhiteSpace(writer) || string.IsNullOrWhiteSpace(genre))
            {
                var dialog = new ContentDialog
                {
                    Title = "Помилка",
                    Content = "Будь ласка, заповніть всі поля і введіть коректний рік.",
                    CloseButtonText = "Закрити"
                };
                await dialog.ShowAsync();
                return;
            }

            Film newFilm = new Film(title, director, writer, genre, year);

            await _repository.Add(newFilm);
            await LoadData();
        }
        private async void DeleteFilmButton_Click(object sender, RoutedEventArgs e)
        {
            if (filmListView.SelectedIndex != -1)
            {
                Film selectedItem = filmListView.SelectedItem as Film;

                if (selectedItem != null)
                {
                    await _repository.Remove(selectedItem);
                }
            }
            _ = LoadData();
        }

        private void txtYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var newText = string.Empty;

            foreach (var ch in textBox.Text)
            {
                if (char.IsDigit(ch))
                {
                    newText += ch;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = textBox.Text.Length;
        }
    }
}
