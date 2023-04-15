using LanguagesSchool.ADOapp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LanguagesSchool
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static EnglishEntities Connrction = new EnglishEntities();
        
        public static bool IsAdministratorMode { get; set; } = false;
        public static Visibility AdminVisibility => IsAdministratorMode ? Visibility.Visible : Visibility.Hidden;
       
    }
}
