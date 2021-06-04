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

namespace Diary
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        DateTime dt;
        List<User> list = new List<User>();
        List<User> endedTaskList = new List<User>();

        List<User> newtemplist = new List<User>();
        List<User> templist = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();

            InitializeTime();
            

            InitializePlansForToday();
            InitializeNearestTask();
            InitializeEndTask();

        }
        private void InitializeTime()
        {
            dt = DateTime.Now;
            UpdateTime.Text = "Last update: " + dt.Day + "/" + dt.Month + "/" + dt.Year + " " + dt.Hour + ":" + dt.Minute;
        }
        private void InitializeList()
        {
            list.Clear();
            foreach (var item in db.Users)
            {
                if (item.year == dt.Year && item.month == dt.Month && item.date == dt.Day)
                {
                    list.Add(item);
                }
            }
        }

        private void InitializePlansForToday()
        {
            list.Clear();
            InitializeList();
            text.Text = "Plans for today," + dt.Day + "/" + dt.Month + "/" + dt.Year;

            if (list.Count != 0)
            {
                var newlist = list.OrderBy(x => x.year).ThenBy(x => x.month).ThenBy(x => x.date).ThenBy(x => x.hour).ThenBy(x => x.minute).ToList();
                listOfTasksToday.ItemsSource = newlist;
            }
            else
            {
                List<string> temp = new List<string>(1) { "It seems nothing to do today =D" };

                listOfTasksToday.ItemsSource = temp;
                NextText.Text = "Nearest";
                var templist2 = new List<string>(1) { "It seems nothing to do in next one hour =D" };
                listOfTasksNext.ItemsSource = templist2;

            }
        }

        public void InitializeNearestTask()
        {
            newtemplist.Clear();
            foreach (var item in list)
            {
                if (item.hour == dt.Hour + 1 || (item.hour == dt.Hour && item.minute >= dt.Minute))
                    newtemplist.Add(item);
            }
            templist.Clear();
            templist = newtemplist.OrderBy(x => x.year).ThenBy(x => x.month).ThenBy(x => x.date).ThenBy(x => x.hour).ThenBy(x => x.minute).ToList();
            if (templist.Count != 0)
            {
                listOfTasksNext.ItemsSource = templist;
                if (templist[0].hour == dt.Hour && dt.Minute <= templist[0].minute)
                {
                    NextText.Text = "Nearest in " + (templist[0].minute - dt.Minute) + " minutes";
                }
                else if (templist[0].hour > dt.Hour && dt.Minute <= templist[0].minute)
                {
                    NextText.Text = "Nearest in " + 1 + " hour " + (templist[0].minute - dt.Minute) + " minutes";
                }
                else if (templist[0].hour > dt.Hour && dt.Minute == templist[0].minute)
                {
                    NextText.Text = "Nearest in " + 1 + " hour";
                }
                else
                {
                    NextText.Text = "Nearest in " + 0 + " hour " + (60 - dt.Minute + templist[0].minute) + " minutes";
                }
            }
            else
            {
                var templist2 = new List<string>(1) { "It seems nothing to do in next one hour =D" };
                listOfTasksNext.ItemsSource = templist2;
                NextText.Text = "Nearest";
            }
        }

        private void InitializeEndTask()
        {
            endedTaskList.Clear();
            foreach (var item in db.Users)
            {
                if (item.year < dt.Year)
                    endedTaskList.Add(item);
                else if (item.year == dt.Year && item.month < dt.Month)
                    endedTaskList.Add(item);
                else if (item.year == dt.Year && item.month == dt.Month && item.date < dt.Day)
                    endedTaskList.Add(item);
                else if (item.year == dt.Year && item.month == dt.Month && item.date == dt.Day && item.hour < dt.Hour)
                    endedTaskList.Add(item);
                else
                    continue;
            }
            if (endedTaskList.Count != 0)
            {
                listOfTasksEnded.ItemsSource = endedTaskList.OrderBy(x => x.year).ThenBy(x => x.month).ThenBy(x => x.date).ThenBy(x => x.hour).ThenBy(x => x.minute).ToList();
            }
            else
            {
                var templist3 = new List<string>(1) { "It seems nothing ended =D" };
                listOfTasksEnded.ItemsSource = templist3;
            }
        }

        private void addTask_Click(object sender, RoutedEventArgs e)
        {
            AddTask task = new AddTask();
            task.Show();
        }

        private void viewAll_Click(object sender, RoutedEventArgs e)
        {
            ShowTasks tasks = new ShowTasks();
            tasks.Show();
        }

        private void calendar1_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = calendar1.SelectedDate;
            var data = new DateTime(selectedDate.Value.Year, selectedDate.Value.Month, selectedDate.Value.Day);
            ShowTasks cal = new ShowTasks(data);
            cal.Show();
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            var desicion = MessageBox.Show("Are you sure, you want to delete all ended tasks?", "Delete ended tasks", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (desicion)
            {
                case MessageBoxResult.Yes:
                    foreach (var item in endedTaskList)
                    {
                        db.Users.Remove(item);
                        list.Remove(item);
                    }
                    db.SaveChanges();
                    var templist3 = new List<string>(1) { "It seems nothing ended =D" };
                    listOfTasksEnded.ItemsSource = templist3;

                    InitializePlansForToday();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            InitializeTime();
            InitializeList();
            InitializeNearestTask();
            InitializePlansForToday();
            InitializeEndTask();
        }

        private void listOfTasksEnded_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                User user = (User)listOfTasksEnded.SelectedItem;
                var desicion = MessageBox.Show($"You selected: \n{user.ToString()} \nEdit it?", "Selected item", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (desicion)
                {
                    case MessageBoxResult.Yes:
                        AddTask ed = new AddTask(user);
                        ed.Show();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
