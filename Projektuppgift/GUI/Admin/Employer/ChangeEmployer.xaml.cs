using GUI.Admin.Home;
using GUI.Admin.User;
using GUI.Admin.Workshop;
using GUI.Login;
using GUI.Tools;
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

namespace GUI.Admin.Employer
{
    /// <summary>
    /// Interaction logic for ChangeEmployer.xaml
    /// </summary>
    public partial class ChangeEmployer : Page
    {

        ILogic adminService = new AdminService();
        Mechanic mechanic = new Mechanic();
        IValid valid = new ValidService();
      
        ILogicUser logicUser = new UserService();
    
        public ChangeEmployer()
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
            AddEmployer addEmployer = new AddEmployer();
            this.NavigationService.Navigate(addEmployer);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }

        //Button_Click_7 visar en anställds värden. 
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //ActivUser kollar ifall detta är en aktiv användare som användaren söker efter.
            if ((valid.ActivUser(employerIdSearch.Text)))
            {
                List<Mechanic> DeklareraLista = adminService.GetMechanic(employerIdSearch.Text);

                All_Loaded();
                Activ_Loaded();

                foreach (var item in DeklareraLista)
                {
                    firstName.Text = item.FirstNameOfMechanic;
                    lastname.Text = item.LastNameOfMechanic;
                    DateTime Birth = item.BirthdayOfMechanic;
                    DateTime employ = item.DateOfEmploymentOfMechanic;
                    employerId.Content = employerIdSearch.Text;
                    Motor.IsChecked= item.Engine;
                    Däck.IsChecked = item.Tire;
                    vindrutor.IsChecked = item.Window;
                    Bromsar.IsChecked = item.Brakes;
                    Kaross.IsChecked = item.Kaross;

                    dateOfBirth.Text = Birth.ToString("yyyy-MM-dd");
                    dateOfEmployment.Text = employ.ToString("yyyy-MM-dd");
                }
            }

            //Ifall ActivUser retunerar false så visas detta. 
            else
            {
                MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
    

        //Button_Click_8 tar bort en aktiv mekaniker. 
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            //ActivUser kollar ifall detta är en aktiv användare som användaren söker efter.
            if ((valid.ActivUser(employerIdSearch.Text)))
            {

                //Om användaren klickar JA så körs metoden DeleteMechanic som tar bort den valde anställda. 
                if (MessageBox.Show("Är du säker på att du vill ta bort anställd!", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    adminService.DeleteMechanic(employerIdSearch.Text);   
                }
            }
            else
            {
                MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
             
            }
            ChangeEmployer changeEmployer = new ChangeEmployer();
            this.NavigationService.Navigate(changeEmployer);
        }


        //Button_Click_9 Ändrar en mekanikers värde. 
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            //Börjar med att den kör metoden ValidMechanic för att se så att alla värden är ifyllda. 
            if (valid.ValidMechanic(firstName.Text, lastname.Text, dateOfBirth.Text, dateOfEmployment.Text)&&valid.ValidMechanicName(firstName.Text) && valid.ValidMechanicName(lastname.Text))
            {
                //ActivUser kollar ifall detta är en aktiv användare som användaren söker efter.
                if ((valid.ActivUser(employerIdSearch.Text)))
                {
                    //Ifall detta stämmer så ändras mekanikern i metoden ChangeMechanic. 
                    adminService.ChangeMechanic(firstName.Text, lastname.Text, DateTime.Parse(dateOfBirth.Text), DateTime.Parse(dateOfEmployment.Text),
                        (bool)Motor.IsChecked, (bool)Däck.IsChecked, (bool)vindrutor.IsChecked, (bool)Bromsar.IsChecked, (bool)Kaross.IsChecked, employerIdSearch.Text);
     
                 MessageBox.Show("Mekaniker är nu ändrad!", "", MessageBoxButton.OK);

                }
                else 
                {
                    MessageBox.Show("Kontrollera att allt är ifyllt korrekt!\n Datum ska anges i formatet(yyyy-mm-dd)\nNamn får inte innehålla siffror!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            else
            {
                MessageBox.Show("Kontrollera att allt är ifyllt korrekt!\n Datum ska anges i formatet(yyyy-mm-dd)", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            ChangeEmployer changeEmployer = new ChangeEmployer();
            this.NavigationService.Navigate(changeEmployer);
        }

        private void employerIdSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (employerIdSearch.Text == "Anställnings-ID")
            {
                employerIdSearch.Text = string.Empty;
            }
        }

        //En ComboBox som visar alla aktiva mekanikers nycklar. 
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> DeklareraLista = new List<string>();
         
            DeklareraLista = adminService.GetKey();
            var combo = sender as ComboBox;
            combo.ItemsSource = DeklareraLista;
            combo.SelectedIndex = 0;     
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
        //laddar alla aktiva ärenden som denna mekaniker har. 
        private void Activ_Loaded()
        {
            if ((valid.ActivUser(employerIdSearch.Text)))
            {
                List<string> orderLista = new List<string>();
                orderLista = logicUser.GetOrder(employerIdSearch.Text);
                Activ.ItemsSource = orderLista;
            }
        }

        //laddar alla färdiga ärenden som denna mekaniker har. 
        private void All_Loaded()
        {
            if ((valid.ActivUser(employerIdSearch.Text)))
            {
                List<string> orderLista = new List<string>();
                orderLista = logicUser.GetfinishedOrder(employerIdSearch.Text);
                All.ItemsSource = orderLista;    
            }
        }
    }
 }

