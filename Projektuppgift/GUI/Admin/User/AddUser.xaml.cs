using GUI.Admin.Employer;
using GUI.Admin.Home;
using GUI.Admin.Workshop;
using GUI.Login;
using GUI.Tools;
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

namespace GUI.Admin.User
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        IValid valid = new ValidService();
        ILogic adminService = new AdminService();
        public AddUser()
        {
            InitializeComponent();
        }


        //Button_Click skapar ett nytt användarkonto. 
        private void Button_Click(object sender, RoutedEventArgs e)
        { //Kollar så att det är ett valid loginn som innehåller det som behövs och att CreatePassword och MatchPassword är detsamma. 
            if (valid.ValidLogin(NewUser.Text.ToLower(), CreatPassword.Password, MatchPassword.Password, userId.Text)&& (valid.ActivUser(userId.Text)))
            { 
                //Lägger till en ny användare med metoden NewUser.
                adminService.NewUser(NewUser.Text.ToLower(), CreatPassword.Password, userId.Text);
                MessageBox.Show("Användarekonto är nu tillagt!", "", MessageBoxButton.OK);
            }
            //Sker ifall ValidLogin retunerar false. 
            else{ MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }

        private void NewUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NewUser.Text == "Användarnamn") {NewUser.Text = string.Empty; }
        }

        private void userId_GotFocus(object sender, RoutedEventArgs e)
        {
            if (userId.Text == "Anställnings-ID") { userId.Text = string.Empty; }
        }

        private void CreatPassword_GotFocus(object sender, RoutedEventArgs e)
        {
           
       if (CreatPassword.Password == "Password") { CreatPassword.Password = string.Empty; }
              
        }

        private void MatchPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MatchPassword.Password == "Password"){MatchPassword.Password = string.Empty; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //--------------------------------------------------------------Nyheter För framtid
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CaseOptions caseOptions = new CaseOptions();
            this.NavigationService.Navigate(caseOptions);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            EmployerOptions employerOptions = new EmployerOptions();
            this.NavigationService.Navigate(employerOptions);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            CaseOption userOptions = new CaseOption();
            this.NavigationService.Navigate(userOptions);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            HomePageAdmin homePageAdmin = new HomePageAdmin();
            this.NavigationService.Navigate(homePageAdmin);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ChangeUserAccount changeUserAccount = new ChangeUserAccount();
            this.NavigationService.Navigate(changeUserAccount);
        }
    }
}
