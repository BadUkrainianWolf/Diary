using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Input;

namespace Diary
{
    /// <summary>
    /// Логика взаимодействия для ShowTasks.xaml
    /// </summary>
    public partial class ShowTasks : Window
    {
        ApplicationContext db;
        DateTime data;
        DateTime dt;
        bool flag = true;
        List<User> tasks = new List<User>();
        public ShowTasks()
        {
            InitializeComponent();
            db = new ApplicationContext();
            InitializeList();
            InitializeTime();
            text.Text = "All Tasks";
        }

        public ShowTasks(DateTime dt)
        {
            data = dt;
            InitializeComponent();
            db = new ApplicationContext();
            InitializeTime();
            text.Text = dt.Day + "/" + dt.Month + "/" + dt.Year;

            InitializeList(data);
        }

        private void InitializeList(DateTime data)
        {
            flag = false;
            tasks = new List<User>();
            foreach (var item in db.Users)
            {
                if (item.year == data.Year && item.month == data.Month && item.date == data.Day)
                {
                    tasks.Add(item);
                }
            }
            if (tasks.Count != 0)
            {
                var newlist = tasks.OrderBy(x => x.year).ThenBy(x => x.month).ThenBy(x => x.date).ThenBy(x => x.hour).ThenBy(x => x.minute).ToList();
                listOfTasks.ItemsSource = newlist;
            }
            else
            {
                List<string> temp = new List<string>();
                temp.Add("It seems nothing to do this day =D");
                listOfTasks.ItemsSource = temp;
            }
        }

        private void InitializeTime()
        {
            dt = DateTime.Now;
            UpdateTime.Text = "Last update: " + dt.Day + "/" + dt.Month + "/" + dt.Year + " " + dt.Hour + ":" + dt.Minute;
        }
        private void InitializeList()
        {
            tasks.Clear();
            tasks = db.Users.ToList();
            var list = tasks.OrderBy(x => x.year).ThenBy(x => x.month).ThenBy(x => x.date).ThenBy(x => x.hour).ThenBy(x => x.minute).ToList();
            listOfTasks.ItemsSource = list;
        }
        private void listOfTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                User user = (User)listOfTasks.SelectedItem;
                var desicion = MessageBox.Show($"You selected: \n{user.ToString()} \nYes = Edit it \nNo = Delete", "Selected item", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (desicion)
                {
                    case MessageBoxResult.Yes:
                        AddTask ed = new AddTask(user);
                        ed.Show();
                        break;
                    case MessageBoxResult.No:
                        var res = MessageBox.Show($"Are you sure, you want to delete: \n{user.ToString()} \t?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                        switch (res)
                        {
                            case MessageBoxResult.Yes:
                                db.Users.Remove(user);
                                db.SaveChanges();
                                break;
                            case MessageBoxResult.No:
                                break;
                        }
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            catch (Exception)
            {

            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
            {
                InitializeTime();
                InitializeList();
            }
            else
            {
                InitializeTime();
                InitializeList(data);
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(List<User>));

            using(var file = new FileStream("ListOfTasks.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(file,tasks);
            }
        }
    }
}
