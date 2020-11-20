using GUI.Admin.Employer;
using GUI.Admin.Home;
using GUI.Admin.User;
using GUI.Login;
using Logic.Interface;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Admin.Workshop
{
    /// <summary>
    /// Interaction logic for AllCase.xaml
    /// </summary>
    public partial class AllCase : Page
    {
        ILogic adminService = new AdminService();
        public AllCase()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomePageAdmin homePageAdmin = new HomePageAdmin();
            this.NavigationService.Navigate(homePageAdmin);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CaseOptions caseOptions = new CaseOptions();
            this.NavigationService.Navigate(caseOptions);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EmployerOptions employerOptions = new EmployerOptions();
            this.NavigationService.Navigate(employerOptions);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CaseOption userOptions = new CaseOption();
            this.NavigationService.Navigate(userOptions);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddCase addCase = new AddCase();
            this.NavigationService.Navigate(addCase);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ChangeCase changeCase = new ChangeCase();
            this.NavigationService.Navigate(changeCase);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);

        }

        //Visar en lista på alla avslutade ärenden.
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> orderLista = new List<string>();

            orderLista = adminService.GetfinishedOrder();
            var combo = sender as ComboBox;
            combo.ItemsSource = orderLista;
            combo.SelectedIndex = 0;
        }
    }
}
