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
using System.Windows.Shapes;

namespace Diary
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string actualPass = "1";
        string actualName = "Mariia";
        public Login()
        {
            InitializeComponent();
        }

        private void MakeWhiteColor()
        {
            textBoxName.Foreground = Brushes.Black;
            textBoxPass.Foreground = Brushes.Black;         
        }
        private void MakeNullTip()
        {
            textBoxName.ToolTip = null;
            textBoxPass.ToolTip = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text.Trim();
            string pass = textBoxPass.Password.Trim();

            if(name != actualName)
            {
                MakeWhiteColor();
                textBoxName.ToolTip = "Wrong name!";
                textBoxName.Foreground = Brushes.DarkRed;
            }
            else if (pass != actualPass)
            {
                MakeWhiteColor();
                textBoxPass.ToolTip = "Wrong pass!";
                textBoxPass.Foreground = Brushes.DarkRed;
            }
            else
            {
                MainWindow m = new MainWindow();
                m.Show();
                Close();
            }
        }
    }
}
