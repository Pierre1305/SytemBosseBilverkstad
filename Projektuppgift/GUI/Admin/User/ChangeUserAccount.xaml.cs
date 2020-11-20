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
    /// Interaction logic for ChangeUserAccount.xaml
    /// </summary>
    public partial class ChangeUserAccount : Page
    {
        ILogic adminService = new AdminService();
        IValid valid = new ValidService();
        public ChangeUserAccount()
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
            //--------------------------------------------------------------Nyheter För framtid
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CaseOptions caseOptions = new CaseOptions();
            this.NavigationService.Navigate(caseOptions);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            EmployerOptions employerOptions = new EmployerOptions();
            this.NavigationService.Navigate(employerOptions);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            CaseOption userOptions = new CaseOption();
            this.NavigationService.Navigate(userOptions);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            this.NavigationService.Navigate(addUser);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }

        private void searchAfterUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RemoveUser.Text == "Användar-ID") { RemoveUser.Text = string.Empty; }
        }

        //En ComboBox som visar alla aktiva användare i systemet.
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> DeklareraLista = new List<string>();
        
            DeklareraLista = adminService.GetActivUser();
            var combo = sender as ComboBox;
            combo.ItemsSource = DeklareraLista;
            combo.SelectedIndex = 0;
        }

        //Button_Click_7 tar bort ett användarekonto.
        private void Button_Click_7(object sender, RoutedEventArgs e)
        { 
            //Kollar så att det är en Active User som kan tas bort och att RemoveUser texten inte är tom för att undvika buggar.
            if (RemoveUser.Text != String.Empty&& valid.ActivUser(RemoveUser.Text))
            {    
                //Metoden DeleteLogin körs som tar bort den valda Användaren. 
                adminService.DeletLogin(RemoveUser.Text);
                MessageBox.Show("Användarekonto är nu borttaget!", "", MessageBoxButton.OK);
                ChangeUserAccount changeUserAccount = new ChangeUserAccount();
                this.NavigationService.Navigate(changeUserAccount);
            }
            else{MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }
    }
}
