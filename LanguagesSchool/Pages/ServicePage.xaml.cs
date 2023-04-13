﻿using LanguagesSchool.ADOapp;
using Microsoft.Win32;
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

namespace LanguagesSchool.Pages
{
    public partial class ServicePage : Page
    {
        private Service _service;
        private bool _isEdit;
        public ServicePage(Service service)
        {
            InitializeComponent();
            _service = service;
            if (_service != null)
            {
                _isEdit = true;
            }
            else
            {
                _service = new Service();
            }
        }

        private void MainImageChangeButtonClick(object sender, RoutedEventArgs e)
        {
            var window = new OpenFileDialog();
            if (window.ShowDialog() == true)
            {
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
                var fileName = Path.GetFileName(window.FileName);
                var newPath = projectDirectory + $@"\Услуги школы\{fileName}";

                File.Copy(window.FileName, newPath, true);
                _service.MainImagePath = $@"Услуги школы\{fileName}";

                InvalidateVisual();

                MessageBox.Show($"Успешно");
            }
        }

        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            if (tbCost.Text == "" || tbDiscount.Text == "" || tbName.Text == ""
                || tbDuration.Text == "" || _service.MainImagePath == "")
            {
                MessageBox.Show("Необходимо заполнить все поля", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int duration = 0;
            if (!int.TryParse(tbDuration.Text, out duration) || !int.TryParse(tbDiscount.Text, out int discount))
            {
                MessageBox.Show("Скидка и длительность должны быть целыми числами", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (duration / 3600 > 4)
            {
                MessageBox.Show("Длительность проведения услуги не может быть больше 4 часов", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_isEdit)
            {
                var checkServiceExists = App.Connrction.Service.Any(x => x.Title == _service.Title);
                if (checkServiceExists)
                {
                    MessageBox.Show("Услуга с таким названием уже существует", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            App.Connrction.Service.AddOrUpdate(_service);
            App.Connrction.SaveChanges();
            MessageBox.Show("Успешно", "Сообщение", MessageBoxButton.OK,
                MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void AddNewImageBtnClick(object sender, RoutedEventArgs e)
        {
            var window = new OpenFileDialog();
            if (window.ShowDialog() == true)
            {
                MessageBox.Show($"Успешно{window.FileName}");
                //var newPath = Environment.CurrentDirectory + $@"\Услуги школы\\{Path.GetFileName(window.FileName)}";

                //File.Copy(window.FileName, newPath);
                //_service.MainImagePath = $@"Услуги школы\{Path.GetFileName(window.FileName)}";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _service;
            tblID.Visibility = _isEdit ? Visibility.Visible : Visibility.Collapsed;
            tbID.Visibility = _isEdit ? Visibility.Visible : Visibility.Collapsed;
        }

        private void RemoveServiceImageBtnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
