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
using Logic.Interface;
using Logic.Services;
using Logic.Entities;
using GUI.Tools;

namespace GUI.Admin.Workshop
{
    /// <summary>
    /// Interaction logic for AddCase.xaml
    /// </summary>
    public partial class AddCase : Page
    {
        readonly Orders orders = new Orders();
        string valueOfVehicle;
        string valueOfMechanic;
        List<string> listOfMechanics = new List<string>();
        ILogic adminService = new AdminService();
        IValid valid = new ValidService();

        public AddCase()
        {
            InitializeComponent();
        }
        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }
        private void Button_Start(object sender, RoutedEventArgs e)
        {
            HomePageAdmin homePageAdmin = new HomePageAdmin();
            this.NavigationService.Navigate(homePageAdmin);
        }

        private void Button_Dela(object sender, RoutedEventArgs e) //Till ChangeCase
        {
            ChangeCase changeCase = new ChangeCase();
            this.NavigationService.Navigate(changeCase);
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
        //Om detta är true så blir string värdet det som skrivs nedan och sedan skickas det in i RefreshMethod som är förklarad längre ned.
        private void Bromsar_Checked(object sender, RoutedEventArgs e)
        {
            
            orders.Brakes = true;

            if (Bromsar.IsChecked == true)
            {
                string value = "Brakes";
                RefreshMethod(value);
            }
            cbxMechanic.Items.Refresh();
        }
        //Om detta är true så blir string värdet det som skrivs nedan och sedan skickas det in i RefreshMethod som är förklarad längre ned.
        private void Tire_Checked(object sender, RoutedEventArgs e)
        {
           
            orders.Tire = true;
            if (Tire.IsChecked == true)
            {
                string value = "Tires";
                RefreshMethod(value);
            }
            cbxMechanic.Items.Refresh();
        }
        //Om detta är true så blir string värdet det som skrivs nedan och sedan skickas det in i RefreshMethod som är förklarad längre ned.
        private void Vindrutor_Checked(object sender, RoutedEventArgs e)
        {
            
            orders.BrokeWindow = true;
            if (vindrutor.IsChecked == true)
            {
                string value = "Window";
                RefreshMethod(value);
            }
            cbxMechanic.Items.Refresh();
        }


        //Om detta är true så blir string värdet det som skrivs nedan och sedan skickas det in i RefreshMethod som är förklarad längre ned.
        private void Motor_Checked(object sender, RoutedEventArgs e)
        {
            
            orders.Engine = true;
            if (Motor.IsChecked == true)
            {
                string value = "Engine";
                RefreshMethod(value);
            }
            cbxMechanic.Items.Refresh();
        }
        //Om detta är true så blir string värdet det som skrivs nedan och sedan skickas det in i RefreshMethod som är förklarad längre ned.
        private void Kaross_Checked(object sender, RoutedEventArgs e)
        {
            
            orders.Kaross = true;
            if (Kaross.IsChecked == true)
            {
                string value = "Kaross";
                RefreshMethod(value);
            }
            cbxMechanic.Items.Refresh();
        }

        //En ComboBox som visar alla fordon.
        private void ComboBoxVehicle_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> listOfVehicles = new List<string>();
            
            listOfVehicles = adminService.GetVehicles();
            var combo = sender as ComboBox;
            combo.ItemsSource = listOfVehicles;
            combo.SelectedIndex = 0;
        }


        //ButtonSAVE_Click sparar ärendet i en dictionary. 
        private void ButtonSAVE_Click(object sender, RoutedEventArgs e) 
        {

            List<Orders> newOrder = new List<Orders>();
           
            //Kontrollerar att alla värden är ifyllda. 
            if (valid.ValidOrder(orderDesc.Text, valueOfVehicle, valueOfMechanic,
            ModelName.Text, RegNum.Text, matare.Text, dateOfReg.Text, orders.Fuel, specificQ.Text, specificQ2.Text, orderID.Text))
            {
                //FindNumber metoden tar ut alla siffror som finns i strängen MechanicID, detta är nämnligen ID:t för mekanikern.
             string mechanicID=valid.FindNumber(valueOfMechanic);

                //skickar in allt i en ny instans av klassen Order.
             newOrder.Add(new Orders( orderDesc.Text, (bool)Bromsar.IsChecked, (bool)vindrutor.IsChecked,(bool)Motor.IsChecked, (bool)Kaross.IsChecked, (bool)Tire.IsChecked, valueOfVehicle, valueOfMechanic,
                                        ModelName.Text, RegNum.Text, matare.Text, dateOfReg.Text, orders.Fuel, specificQ.Text, specificQ2.Text, orderID.Text, mechanicID));

               //NewOrder är en metod som sparar orderID som Key och listan av ordern som en lista i en dictionary.
             adminService.NewOrder(orderID.Text, newOrder);
                //GiveMechanicOrder ger den valda mekaniker jobbet, och sparar då denna lista på den specifika mekanikers ID.
             adminService.GiveMechanicOrder(mechanicID, newOrder);
             MessageBox.Show("Ett nytt ärende är nu tillagt!", "", MessageBoxButton.OK);
             CaseOptions caseOptions = new CaseOptions();
             this.NavigationService.Navigate(caseOptions);
            }
            else{MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);}
        }

        //Beroende på vad som blir valt i denna ComboBox så kommer det visas olika frågor för användaren angående fordonet som hen har valt. 
        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string valueOfVehicleSelected = cboType.SelectedItem.ToString();
            valueOfVehicle = valueOfVehicleSelected;

           if (valueOfVehicleSelected == "Bil")
            {
                specificQ.Visibility = Visibility.Hidden; 
                specificQ2.Visibility = Visibility.Hidden; 
                cartypelabel.Visibility = Visibility.Visible; 
                cartypecombo.Visibility = Visibility.Visible; 
                cartowbarlabel.Visibility = Visibility.Visible; 
                cartowbarcombo.Visibility = Visibility.Visible;
                specificQ.Text = cartypecombo.SelectedValue.ToString();
                specificQ2.Text = cartowbarcombo.SelectedValue.ToString();

            }
            else if (valueOfVehicleSelected == "Motorcykel")
            {
                specificQ.Visibility = Visibility.Hidden;
                cartypelabel.Visibility = Visibility.Hidden;
                cartypecombo.Visibility = Visibility.Hidden;
                cartowbarlabel.Visibility = Visibility.Hidden;
                cartowbarcombo.Visibility = Visibility.Hidden;
                specificQ.Text = "--------------";
                specificQ2.Text = "-------------";

            }
            else if (valueOfVehicleSelected == "Lastbil")
            {

                specificQ.Visibility = Visibility.Visible;
                cartypelabel.Visibility = Visibility.Hidden;
                cartypecombo.Visibility = Visibility.Hidden;
                cartowbarlabel.Visibility = Visibility.Hidden;
                cartowbarcombo.Visibility = Visibility.Hidden;
                specificQ.Text = "Vad är maxlast på lastbilen?";
                specificQ2.Text = "-------------";

            }
            else if (valueOfVehicleSelected == "Buss")
            {

                specificQ.Visibility = Visibility.Visible;
                cartypelabel.Visibility = Visibility.Hidden;
                cartypecombo.Visibility = Visibility.Hidden;
                cartowbarlabel.Visibility = Visibility.Hidden;
                cartowbarcombo.Visibility = Visibility.Hidden;
                specificQ.Text = "Hur många passagerare tar bussen?";
                specificQ2.Text = "-------------";

            }

        }

        //Dennna metod kollar ifall listan av mekaniker som är tillgängliga för jobbet inte är lika med noll.
        //Ifall det är lika med noll så läggs det till en sträng som enbart säger att det inte finns några mekaniker tillgängliga.
        //Metoden GetMechanicForTheJob tar in ett värde i form av en string som i sin tur matchas med resterande krav på mekanikern och ser
        //ifall det finns någon mekaniker som kan utföra arbetet.
        private void RefreshMethod(string value)
        {
            listOfMechanics = adminService.GetMechanicForTheJob(value);
            if(listOfMechanics.Count == 0)
            {
                listOfMechanics.Add("Finns inga mekaniker för jobbet.");

            }
            cbxMechanic.ItemsSource = listOfMechanics;
            cbxMechanic.SelectedItem = orders.Mechanic;
            if(cbxMechanic.SelectedItem == null)
            {
                cbxMechanic.SelectedItem = "Ingen mekaniker vald";
            }
            cbxMechanic.Items.Refresh();
        }

        private void el_Checked(object sender, RoutedEventArgs e)
        {
            orders.Fuel = "El";
        }

        private void gasoline_Checked(object sender, RoutedEventArgs e)
        {
            orders.Fuel = "Bensin";
        }

        private void etanol_Checked(object sender, RoutedEventArgs e)
        {
            orders.Fuel = "Etanol";
        }

        private void diesel_Checked(object sender, RoutedEventArgs e)
        {
            orders.Fuel = "Diesel";
        }


        //Denna metod körs ifall du ändra mekaniker i ComboBoxen och sparar det i valueOfMechanic strängen som är deklarerad högst upp. 
        private void cbxMechanic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {///-----------------------------------------------------------------------------------------------Här med!

            if (cbxMechanic.SelectedIndex > -1)
            {
                string valueOfMechanicSelected = cbxMechanic.SelectedItem.ToString();
                valueOfMechanic = valueOfMechanicSelected;
            }
            else
            {
                MessageBox.Show("Du har valt att ändra komponent, glöm inte att du måste välja en ny mekaniker.");
            }

        }

        //Denna metod körs ifall du ändrar vilken typ av bil det är. 
        private void cartypecombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cartypecombo.SelectedIndex > -1)
            {
                string newCarType = (string)cartypecombo.SelectedValue;

                

                specificQ.Text = newCarType;
            }


        }
        //Denna metod körs ifall du ändrar om bilen har dragkrok eller ej.
        private void cartowbarcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cartowbarcombo.SelectedIndex > -1)
            {
                

                string haveTowBarString = (string)cartowbarcombo.SelectedValue;

                specificQ2.Text = haveTowBarString;
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AllCase allCase = new AllCase();
            this.NavigationService.Navigate(allCase);
        }
    }
}

