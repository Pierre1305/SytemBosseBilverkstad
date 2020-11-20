using GUI.Login;
using GUI.Mechanics.Home;
using GUI.Mechanics.Workshop;
using Logic.DAL;
using Logic.Entities;
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

namespace GUI.Mechanics.User
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        Mechanic mechanic = new Mechanic();
        ILogicUser userService = new UserService();
        public Profile()
        {
            InitializeComponent();
            Showinfo();
        }

        //visar all info om den aktuella mekanikern som är inloggad.
        public void Showinfo()
        {
                List<Mechanic> DeklareraLista = userService.GetMechanic(ActivClasses.UserKey);
                foreach (var item in DeklareraLista)
                {
                    firstName.Content = item.FirstNameOfMechanic;
                    lastname.Content = item.LastNameOfMechanic;
                    dateOfBirth.Content = item.BirthdayOfMechanic;
                    dateOfEmployment.Content = item.DateOfEmploymentOfMechanic;
                    employerId.Content = ActivClasses.UserKey;
                    Motor.IsChecked = item.Engine;
                    Tire.IsChecked = item.Tire;
                    vindrutor.IsChecked = item.Window;
                    Bromsar.IsChecked = item.Brakes;
                    Kaross.IsChecked = item.Kaross;
                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }

        private void Bromsar_Checked(object sender, RoutedEventArgs e)
        {
            mechanic.Brakes = true;
        }

        private void Tire_Checked(object sender, RoutedEventArgs e)
        {
            mechanic.Tire = true;
        }

        private void vindrutor_Checked(object sender, RoutedEventArgs e)
        {
            mechanic.Window = true;
        }

        private void Motor_Checked(object sender, RoutedEventArgs e)
        {
            mechanic.Engine = true;
        }

        private void Kaross_Checked(object sender, RoutedEventArgs e)
        {
            mechanic.Kaross = true;
        }


        //Ändrar mekanikerns kompetens.
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            userService.Changes((bool)Motor.IsChecked, (bool)Tire.IsChecked, (bool)vindrutor.IsChecked,
                                    (bool)Bromsar.IsChecked, (bool)Kaross.IsChecked);
            MessageBox.Show("Mekaniker är nu ändrad!", "", MessageBoxButton.OK);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HomePageMechanic homePageMechanic = new HomePageMechanic();
            this.NavigationService.Navigate(homePageMechanic);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           CaseOptionMechanics caseOptionMechanic = new CaseOptionMechanics();
            this.NavigationService.Navigate(caseOptionMechanic);
        }
    }
}
