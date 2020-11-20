using GUI.Login;
using GUI.Mechanics.Home;
using GUI.Mechanics.User;
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
    /// Interaction logic for AllCaseMechanic.xaml
    /// </summary>
    public partial class AllCaseMechanic : Page
    {
        ILogicUser logicUser = new UserService();

        public AllCaseMechanic()
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
            ActivCaseMechanic activCaseMechanic = new ActivCaseMechanic();
            this.NavigationService.Navigate(activCaseMechanic);
        }

        //ComboBox_Loaded_1 visar alla avslutade ärenden för den specifika mekanikern.
        private void ComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            List<string> orderLista = new List<string>();

            orderLista = logicUser.GetfinishedOrder();
            var combo = sender as ComboBox;
            combo.ItemsSource = orderLista;
            combo.SelectedIndex = 0;
        }
    }
}
