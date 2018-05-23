using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms1
{
    [Serializable]
    class Tasks : IComparable
    {
        public DateTime DateInfo { get; set; }
        public string Task { get; set; }
        public Tasks(DateTime d, string t)
        {
            DateInfo = d;
            Task = t;
        }

        public int CompareTo(object obj)
        {
            if ((obj as Tasks).DateInfo.Hour != DateInfo.Hour)
                return DateInfo.Hour - (obj as Tasks).DateInfo.Hour;
            else
                return DateInfo.Minute - (obj as Tasks).DateInfo.Minute;
        }
    }
}
