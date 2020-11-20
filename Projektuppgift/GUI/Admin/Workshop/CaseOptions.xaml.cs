using GUI.Admin.Home;
using GUI.Admin.User;
using GUI.Admin.Employer;
using GUI.Login;
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
    /// Interaction logic for CaseOptions.xaml
    /// </summary>
    public partial class CaseOptions : Page
    {
        public CaseOptions()
        {
            InitializeComponent();
        }
        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomePageAdmin homePageAdmin = new HomePageAdmin();
            this.NavigationService.Navigate(homePageAdmin);
        }

        private void Button_Dela(object sender, RoutedEventArgs e)
        {
            ChangeCase changeCase = new ChangeCase();
            this.NavigationService.Navigate(changeCase);
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            AddCase addCase = new AddCase();
            this.NavigationService.Navigate(addCase);
        }
        private void Button_Workshop(object sender, RoutedEventArgs e) //Till CaseOptions (om man vill rensa)
        {
            CaseOptions caseOptions = new CaseOptions();
            this.NavigationService.Navigate(caseOptions);
        }

        private void Button_List(object sender, RoutedEventArgs e)
        {
            EmployerOptions employerOptions = new EmployerOptions();
            this.NavigationService.Navigate(employerOptions);
        }

        private void Button_Users(object sender, RoutedEventArgs e) //Dras til UserOptions. Kallas CaseOption..
        {
            CaseOption userOptions = new CaseOption();
            this.NavigationService.Navigate(userOptions);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AllCase allCase = new AllCase();
            this.NavigationService.Navigate(allCase);

        }
    }
}
