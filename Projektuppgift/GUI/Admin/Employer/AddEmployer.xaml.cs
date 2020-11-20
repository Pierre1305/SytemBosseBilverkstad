using GUI.Admin.Home;
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
using Logic.Entities;
using Logic.Interface;
using Logic.Services;

namespace GUI.Admin.Employer
{
    
   
    public partial class AddEmployer : Page
    {
        Mechanic mechanic = new Mechanic();
        IValid valid = new ValidService();
        ILogic adminService = new AdminService();

        public AddEmployer()
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
            ChangeEmployer changeEmployer = new ChangeEmployer();
            this.NavigationService.Navigate(changeEmployer);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }

        //Button_Click_7 sparar alla värden och skapar en mekaniker i en dictionary. 
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            List<Mechanic> mechanic = new List<Mechanic>();
            var activeOrder = new Mechanic();
                //validerar så att allt som ska in har text i sig. 
                if (valid.ValidMechanic(firstName.Text, lastname.Text, dateOfBirth.Text, dateOfEmployment.Text, employerId.Text)&& 
                valid.ValidMechanicID(employerId.Text)&& valid.ValidMechanicName(firstName.Text)&& valid.ValidMechanicName(lastname.Text))
                {
                string id = employerId.Text;
                mechanic.Add(new Mechanic(firstName.Text, lastname.Text,
                                                       DateTime.Parse(dateOfBirth.Text), DateTime.Parse(dateOfEmployment.Text),
                                                         (bool)Motor.IsChecked, (bool)Tire.IsChecked,
                                                         (bool)vindrutor.IsChecked, (bool)Bromsar.IsChecked, (bool)Kaross.IsChecked,id));

                   //här skickas listan och nyckeln (som är employerID) till ett dictionary. 
                    adminService.NewMechanic(id, mechanic);
                    MessageBox.Show("Mekaniker är nu tillagt!", "", MessageBoxButton.OK);
                }

                //Ifall ValidMechanic inte stämmer så visas detta
                
                else
                {
                    MessageBox.Show(" Kontrollera att allt är ifyllt korrekt!\n Datum ska anges i formatet(yyyy-mm-dd)\n Namn får inte innehålla siffror!\n Anställnigns-ID får endast bestå utav siffror!",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
        }

        private void firstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (firstName.Text == "Namn") { firstName.Text = string.Empty; }
        }

        private void lastname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (lastname.Text == "Efternamn") { lastname.Text = string.Empty; }
        }

        private void dateOfBirth_GotFocus(object sender, RoutedEventArgs e)
        {
            if (dateOfBirth.Text == "Födelsedatum") { dateOfBirth.Text = string.Empty; }
        }

        private void dateOfEmployment_GotFocus(object sender, RoutedEventArgs e)
        {
            if (dateOfEmployment.Text == "Anställningdatum") { dateOfEmployment.Text = string.Empty; }
        }

        private void employerId_GotFocus(object sender, RoutedEventArgs e)
        {
            if (employerId.Text == "Anställnings-ID") { employerId.Text = string.Empty; }
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
    }
}
