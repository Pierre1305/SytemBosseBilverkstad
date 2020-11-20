using Logic.DAL;
using Logic.Entities;
using Logic.MyExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            JsonGetFile jsonGetFile = new JsonGetFile();
            try
            {
                jsonGetFile.GetJson();
            }
            catch (Exception )
            {
                MessageBox.Show("Filen kunde inte läsasas korrekt!" +
                    "\n" +
                    "\n Avsluta och starta om programet!");
            }


        }

        private void NavigationWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }
    }
}
