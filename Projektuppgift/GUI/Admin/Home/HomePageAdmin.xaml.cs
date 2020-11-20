using GUI.Admin.Employer;
using GUI.Admin.User;
using GUI.Admin.Workshop;
using GUI.Login;
using GUI.Tools;
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

namespace GUI.Admin.Home
{
    /// <summary>
    /// Interaction logic for HomePageAdmin.xaml
    /// </summary>
    public partial class HomePageAdmin : Page
    {
        public HomePageAdmin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// To user options menu
        /// </summary>       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CaseOption userOptions = new CaseOption();
            this.NavigationService.Navigate(userOptions);      
        }

        /// <summary>
        /// Back to login Menu
        /// </summary>   
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }
        /// <summary>
        /// To employer menu
        /// </summary>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EmployerOptions employerOptions = new EmployerOptions();
            this.NavigationService.Navigate(employerOptions);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CaseOptions caseOptions = new CaseOptions();
            this.NavigationService.Navigate(caseOptions);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //--------------------------------------------------------------Nyheter För framtid
        }
    }
}
