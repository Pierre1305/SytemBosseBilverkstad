using System;
using GUI.Admin.Home;
using GUI.Admin.User;
using GUI.Login;
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
using GUI.Admin.Employer;
using Logic.Interface;
using Logic.Services;
using Logic.Entities;
using GUI.Tools;
using Logic.DAL;

namespace GUI.Admin.Workshop
{
    /// <summary>
    /// Interaction logic for ChangeCase.xaml
    /// </summary>
    public partial class ChangeCase : Page
    {
        Orders order = new Orders();
        List<string> listOfVehicles = new List<string>();
        List<string> listOfMechanics = new List<string>();
        string valueOfVehicle;
        string valueOfMechanic;
        ILogic adminService = new AdminService();
        IValid valid = new ValidService();
        string QuestionOne;
        string QuestionTwo;
        public ChangeCase()
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


        //Button_ClickSearchOrder visar dig vad en specifik order innehåller. 
        private void Button_ClickSearchOrder(object sender, RoutedEventArgs e)
        {
            
            //ActiveOrder kontrollerar att det är ett aktivt ID som användaren skickar in.
            if ((valid.ActivOrder(OrderIdSearch.Text))) 
            {
                //Kallar på GetOrder vilket retunerar en lista av alla värden för den här ordern.
                List<Orders> listOfSpecificOrder = adminService.GetOrder(OrderIdSearch.Text);
                foreach (var item in listOfSpecificOrder)
                {
                    orderID.Content = OrderIdSearch.Text;
                    orderDesc.Text = item.OrderDescription;
                    ModelName.Text = item.ModelName;
                    RegNum.Text = item.RegNumber;
                    dateOfReg.Text = item.RegDate;
                    matare.Text = item.Matare;
                    specificQ.Content = item.SpecificQuestionAboutVehicle1;
                    specificQ2.Content = item.SpecificQuestionAboutVehicle2;
                   
                    Motor.IsChecked = item.Engine;
                    Däck.IsChecked = item.Tire;
                    Vindrutor.IsChecked = item.BrokeWindow;
                    Bromsar.IsChecked = item.Brakes;
                    Kaross.IsChecked = item.Kaross;
                    vehicleType.Content = item.TypeOfVehicle;
                    valueOfVehicle = item.TypeOfVehicle;
                    currentMehanic.Content = item.Mechanic;
                    cbxMechanic.SelectedItem = item.Mechanic;
                 
                   
                  

                    if (item.Fuel == "El")
                    {
                        el.IsChecked = true;
                    }
                    else if (item.Fuel == "Bensin")
                    {
                        gasoline.IsChecked = true;
                    }
                    else if (item.Fuel == "Etanol")
                    {
                        etanol.IsChecked = true;
                    }
                    else if (item.Fuel == "Diesel")
                    {
                        diesel.IsChecked = true;
                    }

                    

                }
            }
            else
            {
                MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }


       
            
        }
        //Button_Bort tar bort ett aktivt ärende.
        private void Button_Bort(object sender, RoutedEventArgs e)
        {
            
            //Kontrollerar så att det är en aktiv order
            if ((valid.ActivOrder(OrderIdSearch.Text)))
            {

                if (MessageBox.Show("Är du säker på att du vill ta bort ärendet?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                   //Kallar på metoden DeleteOrder vilket tar bort en aktiv order.
                    adminService.DeleteOrder(OrderIdSearch.Text);
                    
                }


            }
            else
            {
                MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            ChangeCase changeCase = new ChangeCase();
            this.NavigationService.Navigate(changeCase);
        }


        //Button_Click_ChangeCase ändrar ett ärende. 
        private void Button_Click_ChangeCase(object sender, RoutedEventArgs e)
        {

            List<Orders> newOrder = new List<Orders>();

         
            //Kontrollerar att alla värden är ifyllda.

            if (valid.ValidOrder(orderDesc.Text, valueOfVehicle, valueOfMechanic, ModelName.Text, RegNum.Text, matare.Text, dateOfReg.Text, order.Fuel))
            {
                //Kontrollerar att det är en aktiv order.
                if (valid.ActivOrder(OrderIdSearch.Text))
                {   
                    //hämtar ID för mekanikern ur stringen.
                 string mechanicID = valid.FindNumber(valueOfMechanic);
                    //Skriver ut de specifika frågorna som är för just detta ärende.
                    foreach (var item in ActivClasses.orderDictionary[OrderIdSearch.Text])
                    {
                        QuestionOne = item.SpecificQuestionAboutVehicle1;
                        QuestionTwo = item.SpecificQuestionAboutVehicle2;
                
                    }
                  
                    //Tar bort den "gamla" ordernn
                 adminService.DeleteOrder(OrderIdSearch.Text);

                    //Lägger till den nya uppdaterade ordern.
                 newOrder.Add(new Orders(orderDesc.Text, (bool)Bromsar.IsChecked, (bool)Vindrutor.IsChecked, (bool)Motor.IsChecked, (bool)Kaross.IsChecked, (bool)Däck.IsChecked, valueOfVehicle, valueOfMechanic,
                             ModelName.Text, RegNum.Text, matare.Text, dateOfReg.Text, order.Fuel, QuestionOne, QuestionTwo, OrderIdSearch.Text, mechanicID));
                    //Lägg till den på nytt i dictionary
                 adminService.NewOrder(OrderIdSearch.Text, newOrder);
                    //Lägger till denn på nytt hos mekanikern. 
                 adminService.GiveMechanicOrder(mechanicID, newOrder);
                 MessageBox.Show("Ärendet är ändrat!", "", MessageBoxButton.OK);
                 ChangeCase changeCase = new ChangeCase();
                 this.NavigationService.Navigate(changeCase);
                }
                else
                {
                    MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        
        private void Bromsar_Checked(object sender, RoutedEventArgs e)
        {
            order.Brakes = true;
            string value = "Brakes";
            RefreshMethod(value);
        }

        private void Däck_Checked(object sender, RoutedEventArgs e)
        {
            order.Tire = true;
            string value = "Tires";
            RefreshMethod(value);
        }
        private void Vindruta_Checked(object sender, RoutedEventArgs e)
        {
            order.BrokeWindow = true;
            string value = "Window";
            RefreshMethod(value);
        }
        private void Motor_Checked(object sender, RoutedEventArgs e)
        {
            order.Engine = true;
            string value = "Engine";
            RefreshMethod(value);
        }
        private void Kaross_Checked(object sender, RoutedEventArgs e)
        {
            order.Kaross = true;
            string value = "Kaross";
            RefreshMethod(value);
        }

     
       

       
        //Visar alla nycklar för det olika aktiva orderns
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {

            List<string> orderLista = new List<string>();
            ILogic adminService = new AdminService();
            orderLista = adminService.GetKeyForOrder();
            var combo = sender as ComboBox;
            combo.ItemsSource = orderLista;
   

        }

        //Se dokumentation om detta i AddCase
        private void RefreshMethod(string value)
        {
            ILogic adminService = new AdminService();
            listOfMechanics = adminService.GetMechanicForTheJob(value);
            if (listOfMechanics.Count == 0)
            {
                listOfMechanics.Add("Finns inga mekaniker för jobbet.");

            }
            cbxMechanic.ItemsSource = listOfMechanics;
            cbxMechanic.Items.Refresh();
        }

        private void cbxMechanic_Loaded(object sender, RoutedEventArgs e)
        {
            cbxMechanic.ItemsSource = listOfMechanics;
            cbxMechanic.SelectedItem = order.Mechanic;

     
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AllCase allCase = new AllCase();
            this.NavigationService.Navigate(allCase);
        }

        //Button_Click_1 skickar ordern till Avslutad. 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (valid.ActivOrder(OrderIdSearch.Text))
            {

                List<Orders> finished = adminService.finishedOrder(OrderIdSearch.Text);

                adminService.MoveFinishedOrder(finished, OrderIdSearch.Text);

                MessageBox.Show("Färdigt", "", MessageBoxButton.OK);
            }
            else { MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
            ChangeCase changeCase = new ChangeCase();
            this.NavigationService.Navigate(changeCase);
        }

        //Körs ifall man ändrar vilket mekaniker det är. 
        private void cbxMechanic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbxMechanic.SelectedIndex > -1)
            {
                string valueOfMechanicSelected = cbxMechanic.SelectedItem.ToString();
                valueOfMechanic = valueOfMechanicSelected;
            }
            else
            {
                
            }





        }
        private void el_Checked(object sender, RoutedEventArgs e)
        {
            order.Fuel = "El";
        }

        private void gasoline_Checked(object sender, RoutedEventArgs e)
        {
            order.Fuel = "Bensin";
        }

        private void etanol_Checked(object sender, RoutedEventArgs e)
        {
            order.Fuel = "Etanol";
        }

        private void diesel_Checked(object sender, RoutedEventArgs e)
        {
            order.Fuel = "Diesel";
        }

    }
}