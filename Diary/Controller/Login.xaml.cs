using System.Windows;
using System.Windows.Media;

namespace Diary
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string actualPass = "pass12345";
        string actualName = "User";
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
