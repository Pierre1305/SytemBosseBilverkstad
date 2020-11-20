using GUI.Login;
using GUI.Mechanics.Home;
using GUI.Mechanics.User;
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

namespace GUI.Mechanics.Workshop
{
    /// <summary>
    /// Interaction logic for ActivCaseMechanic.xaml
    /// </summary>
    public partial class ActivCaseMechanic : Page
    {
        ILogicUser logicUser = new UserService();
        public ActivCaseMechanic()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomePageMechanic homePageMechanic = new HomePageMechanic();
            this.NavigationService.Navigate(homePageMechanic);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            this.NavigationService.Navigate(profile);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CaseOptionMechanics caseOptionMechanic = new CaseOptionMechanics();
            this.NavigationService.Navigate(caseOptionMechanic);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            LogginPage logginPage = new LogginPage();
            this.NavigationService.Navigate(logginPage);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AllCaseMechanic allCaseMechanic = new AllCaseMechanic();
            this.NavigationService.Navigate(allCaseMechanic);
        }

        //Button_Click_5 ändrar en order från aktiv till klar. 
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //Kontrollerar att det är en aktiv order.
            if (logicUser.ActivOrder(ChangeStatus.Text))
            {
                //Kör metoden finishedOrder för att ändra ärendet. 
                logicUser.finishedOrder(ChangeStatus.Text);
                MessageBox.Show("Färdigt", "", MessageBoxButton.OK);
            }
            else { MessageBox.Show(StringTools._inputError, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
            CaseOptionMechanics caseOptionMechanic = new CaseOptionMechanics();
            this.NavigationService.Navigate(caseOptionMechanic);
        }

        //ComboBox_Loaded visar alla aktiva ärenden för mekanikern.
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> orderLista = new List<string>();
           
            orderLista = logicUser.GetOrder();
            var combo = sender as ComboBox;
            combo.ItemsSource = orderLista;
            combo.SelectedIndex = 0;
            
        }

        private void ChangeStatus_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ChangeStatus.Text == "Ärende-ID") { ChangeStatus.Text = string.Empty; }
        }
    }
}
