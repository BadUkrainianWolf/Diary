using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    [DataContract]
    public class User
    {
        public int id { get; set; }

        [DataMember]
        public int year { get; set; }

        [DataMember]
        public int month { get; set; }

        [DataMember]
        public int date { get; set; }

        [DataMember]
        public int hour { get; set; }

        [DataMember]
        public int minute { get; set; }

        [DataMember]
        public int duration { get; set; }

        [DataMember]
        public string place { get; set; }

        [DataMember]
        public string task { get; set; }
        public User(int year, int month, int date, int hour, int minute, int duration, string place, string task)
        {
            this.year = year;
            this.month = month;
            this.date = date;
            this.hour = hour;
            this.minute = minute;
            this.duration = duration;
            this.place = place;
            this.task = task;
        }
        public User()
        {

        }

        public override string ToString()
        {
            return "Date: " + date + "/" + month + " / " + year + "   |   "
                + "Time: " + hour + ":" + minute + "   |   " + "Duration: "
                + duration + "   |   " + "Place: " + place + "   |   " 
                + "Task: " + task;
        }
    }
}
