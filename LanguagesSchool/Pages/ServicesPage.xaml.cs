using LanguagesSchool.ADOapp;
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

namespace LanguagesSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        String crntSort = "";
        String crntFind = "";
        String crntFilter = "";
        
        List<Service> crntListService = new List<Service>();

        List<Service> filteristList = new List<Service>();
        List<Service> sortList = new List<Service>();

        public ServicesPage()
        {
            InitializeComponent();
            listTemplate.ItemsSource = App.Connrction.Service.ToList();

            

            List<String> strSort = new List<string> {"Все", "По Возрастанию", "По убыванию"};
            cmbSort.ItemsSource = strSort;

            List<String> strFilter = new List<string> { "Все", "От 0% до 5%", "От 5% до 15%", "От 15% до 30%", "От 30% до 70%", "От 70% до 100%"};
            cmbFilter.ItemsSource = strFilter;
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            crntSort = cmbSort.SelectedItem as String;
            Sort();
        }

        void Sort() {

            if (crntListService.Count == 0 && crntFilter == "")
            {
                crntListService = App.Connrction.Service.ToList();
            }

            if (crntSort == "Все")
            {
              // crntListService = App.Connrction.Service.ToList();
            }

            if (crntSort == "По Возрастанию")
            {
                crntListService = crntListService.OrderByDescending(o => o.Cost).ToList();
            }

            if (crntSort == "По убыванию")
            {
                crntListService = crntListService.ToList().OrderBy(o => o.Cost).ToList();
            }

            


            listTemplate.ItemsSource = crntListService;

  
        }

        private void FilterMethodChange()
        {
            switch (crntFilter)
            {
                case "Все":
                    crntListService = App.Connrction.Service.ToList();
                    break;
                case "От 0% до 5%":
                    crntListService = App.Connrction.Service.ToList().Where(x => x.Discount >= 0 && x.Discount < 5).ToList();
                    break;
                case "От 5% до 15%":
                    crntListService = App.Connrction.Service.ToList().Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
                    break;
                case "От 15% до 30%":
                    crntListService = App.Connrction.Service.ToList().Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
                    break;
                case "От 30% до 70%":
                    crntListService = App.Connrction.Service.ToList().Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
                    break;
                case "От 70% до 100%":
                    crntListService = App.Connrction.Service.ToList().Where(x => x.Discount >= 70 && x.Discount < 100).ToList();
                    break;
              
            }
            // listTemplate.ItemsSource = crntListService;
            SortAndFilter();
        }


        private void edFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            crntFind = edFind.Text;
            
            
            SortAndFilter();
        }

        void Find()
        {
            if (crntFind != "")
            {
                if ((crntFilter == "" || crntFilter == "Все") && (crntSort == "" || crntSort == "Все"))
                {
                    crntListService = App.Connrction.Service.ToList().Where(x => x.Title.Contains(crntFind)).ToList(); ;
                }
                else
                {
                    crntListService = App.Connrction.Service.ToList();
                }
            }
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            crntFilter = cmbFilter.SelectedItem as String;
            FilterMethodChange();
        }

        void SortAndFilter()
        {
            Find();
            Sort();
            listTemplate.ItemsSource = crntListService;
        }
    }
}
