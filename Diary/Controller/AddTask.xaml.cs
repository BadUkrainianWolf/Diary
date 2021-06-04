using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Diary
{
    /// <summary>
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {

        ApplicationContext db;
        List<User> list;
        int id = -1;
        User user;
        public AddTask()
        {
            InitializeComponent();
            db = new ApplicationContext();
            back.IsEnabled = false;
            ToDo.Text = "Add task";
        }

        public AddTask(User ed)
        {
            InitializeComponent();
            db = new ApplicationContext();
            id = ed.id;
            ToDo.Text = "Edit task";

            textBoxYear.Text = ed.year + "";
            textBoxMonth.Text = ed.month + "";
            textBoxDate.Text = ed.date + "";
            textBoxHour.Text = ed.hour + "";
            textBoxMinute.Text = ed.minute + "";
            textBoxDuration.Text = ed.duration + "";
            textBoxPlace.Text = ed.place;
            textBoxTask.Text = ed.task;
        }

        private void MakeWhiteColor()
        {
            textBoxYear.Foreground = Brushes.Black;
            textBoxMonth.Foreground = Brushes.Black;
            textBoxDate.Foreground = Brushes.Black;
            textBoxHour.Foreground = Brushes.Black;
            textBoxMinute.Foreground = Brushes.Black;
            textBoxDuration.Foreground = Brushes.Black;
            textBoxPlace.Foreground = Brushes.Black;
            textBoxTask.Foreground = Brushes.Black;
        }
        private void MakeNullTip()
        {
            textBoxYear.ToolTip = null;
            textBoxMonth.ToolTip = null;
            textBoxDate.ToolTip = null;
            textBoxHour.ToolTip = null;
            textBoxMinute.ToolTip = null;
            textBoxDuration.ToolTip = null;
            textBoxPlace.ToolTip = null;
            textBoxTask.ToolTip = null;
        }
        private void Button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            string year = textBoxYear.Text.Trim();
            string month = textBoxMonth.Text.Trim();
            string date = textBoxDate.Text.Trim();
            string hour = textBoxHour.Text.Trim();
            string minute = textBoxMinute.Text.Trim();
            string duration = textBoxDuration.Text.Trim();
            string place = textBoxPlace.Text.Trim();
            string task = textBoxTask.Text.Trim();

            DateTime moment = DateTime.Now;

            int years = 0;
            int months = 0;
            int days = 0;
            int hours = 0;
            int minutes = 0;
            int durations = 0;

            bool flagyear = true;
            bool flagmonth = true;
            bool flagdate = true;
            bool flaghour = true;
            bool flagminute = true;
            bool flagduration = true;
            //bool flagcorrectdate = false;
            //DateTime correctdate;

            void SaveInBase()
            {
                MessageBox.Show("Done");
                user = new User(years, months, days, hours, minutes, durations, place, task);
                db.Users.Add(user);
                db.SaveChanges();
                MakeNullTip();
                MakeWhiteColor();
            }

            void ChangeBase()
            {
                MessageBox.Show("Done!");
                user = new User(years, months, days, hours, minutes, durations, place, task);
                var remove = db.Users.Find(id);
                db.Users.Remove(remove);
                db.Users.Add(user);
                db.SaveChanges();
                Close();
            }

            if (year.Length == 0 || year.Length < 4)
            {
                year += moment.Year;
            }
            if (month.Length == 0)
            {
                month += moment.Month;
            }
            if (date.Length == 0)
            {
                date += moment.Day;
            }
            if (duration.Length == 0)
            {
                duration += 60;
            }
            if (place.Length == 0)
            {
                place += "Home";
            }
            if (hour.Length == 0)
            {
                hour += moment.Hour;
            }
            if (minute.Length == 0)
            {
                minute += moment.Minute;
            }

            try
            {
                years = Convert.ToInt32(year);
            }
            catch (Exception)
            {
                flagyear = false;
            }
            try
            {
                months = Convert.ToInt32(month);
            }
            catch (Exception)
            {
                flagmonth = false;
            }

            try
            {
                days = Convert.ToInt32(date);
            }
            catch (Exception)
            {
                flagdate = false;
            }

            try
            {
                hours = Convert.ToInt32(hour);
               
            }
            catch (Exception)
            {
                flaghour = false;
            }
            try
            {
                minutes = Convert.ToInt32(minute);

            }
            catch (Exception)
            {
                flagminute = false;
            }

            try
            {
                durations = Convert.ToInt32(duration);
            }
            catch (Exception)
            {
                flagduration = false;
            }

            //try
            //{
            //    correctdate = new DateTime(years, months, days);
            //}
            //catch (Exception)
            //{
            //    flagcorrectdate = false;
            //}

            if (year.Length > 4)
            {
                MakeWhiteColor();
                textBoxYear.ToolTip = "year can't be longer then 4 numbers";
                textBoxYear.Foreground = Brushes.DarkRed;
            }
            else if (month.Length > 2)
            {
                MakeWhiteColor();
                textBoxMonth.ToolTip = "month can't be longer then 2 numbers";
                textBoxMonth.Foreground = Brushes.DarkRed;
            }
            else if (date.Length > 2)
            {
                MakeWhiteColor();
                textBoxDate.ToolTip = "date can't be longer then 2 numbers";
                textBoxDate.Foreground = Brushes.DarkRed;
            }
            else if (hour.Length > 2)
            {
                MakeWhiteColor();
                textBoxDate.ToolTip = "hours can't be longer then 2 numbers";
                textBoxDate.Foreground = Brushes.DarkRed;
            }
            else if (minute.Length > 2)
            {
                MakeWhiteColor();
                textBoxDate.ToolTip = "time can't be longer then 2 numbers";
                textBoxDate.Foreground = Brushes.DarkRed;
            }
            else if (task.Length == 0)
            {
                MakeWhiteColor();
                textBoxTask.ToolTip = "Task can't be empty";
                textBoxTask.Foreground = Brushes.DarkRed;
            }
            else if(!flagyear)
            {
                MakeWhiteColor();
                textBoxYear.ToolTip = "year must contain only numbers";
                textBoxYear.Foreground = Brushes.DarkRed;
            }
            else if (!flagmonth)
            {
                MakeWhiteColor();
                textBoxYear.ToolTip = "month must contain only numbers";
                textBoxYear.Foreground = Brushes.DarkRed;
            }
            else if (!flagdate)
            {
                MakeWhiteColor();
                textBoxDate.ToolTip = "date must contain only numbers";
                textBoxDate.Foreground = Brushes.DarkRed;
            }
            else if (!flaghour)
            {
                MakeWhiteColor();
                textBoxHour.ToolTip = "Hours must contain only numbers!";
                textBoxHour.Foreground = Brushes.DarkRed;
            }
            else if (!flagminute)
            {
                MakeWhiteColor();
                textBoxMinute.ToolTip = "Hour must contain only numbers!";
                textBoxMinute.Foreground = Brushes.DarkRed;
            }
            else if (!flagduration)
            {
                MakeWhiteColor();
                textBoxDuration.ToolTip = "duration must contain only numbers!";
                textBoxDuration.Foreground = Brushes.DarkRed;
            }
            //else if (!flagcorrectdate)
            //{
            //    textBoxTask.Foreground = Brushes.Black;
            //    textBoxTask.ToolTip = null;
            //    textBoxDate.ToolTip = "This data doesn't exist!";
            //    textBoxDate.Foreground = Brushes.DarkRed;
            //}
            else if (months > 12)
            {
                MakeWhiteColor();
                textBoxMonth.ToolTip = "Months can't be bigger then 12!";
                textBoxMonth.Foreground = Brushes.DarkRed;
            }
            else if (months <= 0)
            {
                MakeWhiteColor();
                textBoxMonth.ToolTip = "Months can't be less then 0!";
                textBoxMonth.Foreground = Brushes.DarkRed;
            }
            else if (days <= 0)
            {
                MakeWhiteColor();
                textBoxDate.ToolTip = "Days can't be less then 0!";
                textBoxDate.Foreground = Brushes.DarkRed;
            }
            else if (days > 31)
            {
                MakeWhiteColor();
                textBoxDate.ToolTip = "Days can't be bigger then 31!";
                textBoxDate.Foreground = Brushes.DarkRed;
            }
            else if (hours > 24)
            {
                MakeWhiteColor();
                textBoxHour.ToolTip = "Hours can't be bigger then 24!";
                textBoxYear.Foreground = Brushes.DarkRed;
            }
            else if (hours <= 0)
            {
                MakeWhiteColor();
                textBoxHour.ToolTip = "Hours can't be less then 1!";
                textBoxYear.Foreground = Brushes.DarkRed;
            }
            else if (minutes > 59)
            {
                MakeWhiteColor();
                textBoxMinute.ToolTip = "Minutes can't be bigger then 59!";
                textBoxYear.Foreground = Brushes.DarkRed;
            }
            else if (minutes <= -1)
            {
                MakeWhiteColor();
                textBoxMinute.ToolTip = "Minutes can't be less then 0!";
                textBoxMinute.Foreground = Brushes.DarkRed;
            }
            else if (Intersections.IsChecked != true)
            {

                MakeWhiteColor();
                List<User> tasks = db.Users.ToList();

                user = new User(years, months, days, hours, minutes, durations, place, task);
                tasks.Add(user);

                list = tasks.OrderBy(x => x.year).ThenBy(x => x.month).ThenBy(x => x.date).ThenBy(x => x.hour).ThenBy(x => x.minute).ToList();

                int index = list.IndexOf(user);

                User Before()
                {
                    var before = list[index - 1];

                    int EndBeforeTaskyear = before.year;
                    int EndBeforeTaskmonth = before.month;
                    int EndBeforeTaskdate = before.date;
                    int EndBeforeTaskhour = before.hour;
                    int EndBeforeTaskminute = before.minute;

                    if (before.duration > 60)
                    {
                        int x = before.duration;
                        while (x > 60)
                        {
                            EndBeforeTaskhour++;
                            x -= 60;
                        }
                        if (EndBeforeTaskminute + x >= 60)
                        {
                            EndBeforeTaskhour++;
                            EndBeforeTaskminute += (x - 60);
                        }
                        else
                        {
                            EndBeforeTaskminute += x;
                        }
                        if (EndBeforeTaskhour > 24)
                        {
                            while (EndBeforeTaskhour >= 24)
                            {
                                EndBeforeTaskdate++;
                                EndBeforeTaskhour -= 24;
                            }
                            if (EndBeforeTaskdate > 31)
                            {
                                while (EndBeforeTaskdate > 31)
                                {
                                    EndBeforeTaskmonth++;
                                    EndBeforeTaskdate -= 31;
                                }
                                if (EndBeforeTaskmonth > 12)
                                {
                                    while (EndBeforeTaskmonth > 12)
                                    {
                                        EndBeforeTaskyear++;
                                        EndBeforeTaskmonth -= 12;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (EndBeforeTaskminute + before.duration >= 60)
                        {
                            int x = before.duration;
                            EndBeforeTaskhour++;
                            EndBeforeTaskminute += (x - 60);
                        }
                        else
                        {
                            EndBeforeTaskminute += before.duration;
                        }
                        if (EndBeforeTaskhour > 24)
                        {
                            while (EndBeforeTaskhour >= 24)
                            {
                                EndBeforeTaskdate++;
                                EndBeforeTaskhour -= 24;
                            }
                            if (EndBeforeTaskdate > 31)
                            {
                                while (EndBeforeTaskdate > 31)
                                {
                                    EndBeforeTaskmonth++;
                                    EndBeforeTaskdate -= 31;
                                }
                                if (EndBeforeTaskmonth > 12)
                                {
                                    while (EndBeforeTaskmonth > 12)
                                    {
                                        EndBeforeTaskyear++;
                                        EndBeforeTaskmonth -= 12;
                                    }
                                }
                            }
                        }
                    }
                    return new User(EndBeforeTaskyear, EndBeforeTaskmonth, EndBeforeTaskdate, EndBeforeTaskhour, EndBeforeTaskminute, 0, before.place, before.task);
                }

                User After()
                {
                    var EndThisTask = new User();

                    int EndThisTaskyear = user.year;
                    int EndThisTaskmonth = user.month;
                    int EndThisTaskdate = user.date;
                    int EndThisTaskhour = user.hour;
                    int EndThisTaskminute = user.minute;

                    if (user.duration > 60)
                    {
                        int x = user.duration;
                        while (x > 60)
                        {
                            EndThisTaskhour++;
                            x -= 60;
                        }
                        if (EndThisTaskminute + x >= 60)
                        {
                            EndThisTaskhour++;
                            EndThisTaskminute += (x - 60);
                        }
                        else
                        {
                            EndThisTaskminute += x;
                        }
                        if (EndThisTaskhour > 24)
                        {
                            while (EndThisTaskhour >= 24)
                            {
                                EndThisTaskdate++;
                                EndThisTaskhour -= 24;
                            }
                            if (EndThisTaskdate > 31)
                            {
                                while (EndThisTaskdate > 31)
                                {
                                    EndThisTaskmonth++;
                                    EndThisTaskdate -= 31;
                                }
                                if (EndThisTaskmonth > 12)
                                {
                                    while (EndThisTaskmonth > 12)
                                    {
                                        EndThisTaskyear++;
                                        EndThisTaskmonth -= 12;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                        if (EndThisTaskminute + user.duration >= 60)
                        {
                            int x = user.duration;
                            EndThisTaskhour++;
                            EndThisTaskminute += (x - 60);
                        }
                        else
                        {
                            EndThisTaskminute += user.duration;
                        }
                        if (EndThisTaskhour > 24)
                        {
                            while (EndThisTaskhour >= 24)
                            {
                                EndThisTaskdate++;
                                EndThisTaskhour -= 24;
                            }
                            if (EndThisTaskdate > 31)
                            {
                                while (EndThisTaskdate > 31)
                                {
                                    EndThisTaskmonth++;
                                    EndThisTaskdate -= 31;
                                }
                                if (EndThisTaskmonth > 12)
                                {
                                    while (EndThisTaskmonth > 12)
                                    {
                                        EndThisTaskyear++;
                                        EndThisTaskmonth -= 12;
                                    }
                                }
                            }
                        }
                    }
                    return new User(EndThisTaskyear, EndThisTaskmonth, EndThisTaskdate, EndThisTaskhour, EndThisTaskminute, 0, user.place, user.task);
                }

                if (index > 0 && index < list.Count - 1)
                {
                    var before = list[index - 1];

                    var after = list[index + 1];
                    var EndBeforeTask = Before();

                    if (EndBeforeTask.year > user.year)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{before.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndBeforeTask.year == user.year && EndBeforeTask.month > user.month)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{before.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndBeforeTask.year == user.year && EndBeforeTask.month == user.month && EndBeforeTask.date > user.date)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{before.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndBeforeTask.year == user.year && EndBeforeTask.month == user.month && EndBeforeTask.date == user.date && EndBeforeTask.hour > user.hour)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{before.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndBeforeTask.year == user.year && EndBeforeTask.month == user.month && EndBeforeTask.date == user.date && EndBeforeTask.hour == user.hour && EndBeforeTask.minute > user.minute)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{before.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else
                    {
                        var EndthisTask = After();
                        if (EndthisTask.year > after.year)
                        {
                            textBoxDuration.ToolTip = "Your added task is intersected with something!";
                            MessageBox.Show($"Your added task is intersected with\n{after.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                            textBoxDuration.Foreground = Brushes.DarkRed;
                        }
                        else if (EndthisTask.year == after.year && EndthisTask.month > after.month)
                        {
                            textBoxDuration.ToolTip = "Your added task is intersected with something!";
                            MessageBox.Show($"Your added task is intersected with\n{after.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                            textBoxDuration.Foreground = Brushes.DarkRed;
                        }
                        else if (EndthisTask.year == after.year && EndthisTask.month == after.month && EndthisTask.date > after.date)
                        {
                            textBoxDuration.ToolTip = "Your added task is intersected with something!";
                            MessageBox.Show($"Your added task is intersected with\n{after.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                            textBoxDuration.Foreground = Brushes.DarkRed;
                        }
                        else if (EndthisTask.year == after.year && EndthisTask.month == after.month && EndthisTask.date == after.date && EndthisTask.hour > after.hour)
                        {
                            textBoxDuration.ToolTip = "Your added task is intersected with something!";
                            MessageBox.Show($"Your added task is intersected with\n{after.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                            textBoxDuration.Foreground = Brushes.DarkRed;
                        }
                        else if (EndthisTask.year == after.year && EndthisTask.month == after.month && EndthisTask.date == after.date && EndthisTask.hour == after.hour && EndthisTask.minute > after.minute)
                        {
                            textBoxDuration.ToolTip = "Your added task is intersected with something!";
                            MessageBox.Show($"Your added task is intersected with\n{after.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                            textBoxDuration.Foreground = Brushes.DarkRed;
                        }
                        else
                        {
                            if (id == -1)
                                SaveInBase();
                            else
                                ChangeBase();
                        }
                    }
                }
                else if (index == 0 && list.Count == 1)
                {
                    SaveInBase();
                }
                else if (index == 0 && list.Count != 0)
                {
                    var after = list[index + 1];
                    var EndthisTask = After();
                    if (EndthisTask.year > after.year)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{after.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndthisTask.year == after.year && EndthisTask.month > after.month)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{after.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndthisTask.year == after.year && EndthisTask.month == after.month && EndthisTask.date > after.date)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{after.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndthisTask.year == after.year && EndthisTask.month == after.month && EndthisTask.date == after.date && EndthisTask.hour > after.hour)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{after.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndthisTask.year == after.year && EndthisTask.month == after.month && EndthisTask.date == after.date && EndthisTask.hour == after.hour && EndthisTask.minute > after.minute)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{after.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else
                    {
                        if (id == -1)
                            SaveInBase();
                        else
                            ChangeBase();
                    }
                }
                else
                {
                    var before = list[index - 1];

                    var EndBeforeTask = Before();
                    if (EndBeforeTask.year > user.year)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{before.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndBeforeTask.year == user.year && EndBeforeTask.month > user.month)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{before.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndBeforeTask.year == user.year && EndBeforeTask.month == user.month && EndBeforeTask.date > user.date)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{before.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndBeforeTask.year == user.year && EndBeforeTask.month == user.month && EndBeforeTask.date == user.date && EndBeforeTask.hour > user.hour)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{before.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else if (EndBeforeTask.year == user.year && EndBeforeTask.month == user.month && EndBeforeTask.date == user.date && EndBeforeTask.hour == user.hour && EndBeforeTask.minute > user.minute)
                    {
                        textBoxDuration.ToolTip = "Your added task is intersected with something!";
                        MessageBox.Show($"Your added task is intersected with\n{before.ToString()}\nIf it is not mistake press checkbox!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxDuration.Foreground = Brushes.DarkRed;
                    }
                    else
                    {
                        if (id == -1)
                            SaveInBase();
                        else
                            ChangeBase();
                    }
                }
            }
            else
            {
                if (id == -1)
                    SaveInBase();
                else
                    ChangeBase();
            }
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
