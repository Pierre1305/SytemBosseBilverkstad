using GUI.Login;
using GUI.Mechanics.User;
using GUI.Mechanics.Workshop;
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

namespace GUI.Mechanics.Home
{
    /// <summary>
    /// Interaction logic for HomePageMechanic.xaml
    /// </summary>
    public partial class HomePageMechanic : Page
    {
        public HomePageMechanic()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CaseOptionMechanics caseOptionMechanic = new CaseOptionMechanics();
            this.NavigationService.Navigate(caseOptionMechanic);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            this.NavigationService.Navigate(profile);
        }
    }
}
