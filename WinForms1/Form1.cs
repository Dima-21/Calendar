using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace WinForms1
{
    public partial class Form1 : Form
    {
        List<Tasks> tasks = new List<Tasks>();
        public Form1()
        {
            InitializeComponent();
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists("Data.bin"))
            {

                using (var fs = new FileStream("Data.bin", FileMode.Open))
                {
                    tasks = formatter.Deserialize(fs) as List<Tasks>;
                }
            }
            foreach (var item in tasks)
            {
                monthCalendar1.AddBoldedDate(item.DateInfo);
            }
            monthCalendar1.UpdateBoldedDates();
            tasks.Sort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != String.Empty && textBox2.Text.First() != ' ')
            {
                DateTime tmp = new DateTime(monthCalendar1.SelectionRange.Start.Year,
                    monthCalendar1.SelectionRange.Start.Month,
                    monthCalendar1.SelectionRange.Start.Day,
                    dateTimePicker1.Value.Hour,
                    dateTimePicker1.Value.Minute,
                    0
                    );

                tasks.Add(new Tasks(tmp, textBox2.Text));
                textBox2.Text = "";

                monthCalendar1.AddBoldedDate(tmp);
                monthCalendar1.UpdateBoldedDates();
            }
            tasks.Sort();
            ShowTasks();
            Serealize();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            ShowTasks();
        }
        private void ShowTasks()
        {
            textBox1.Text = "";
            foreach (Tasks item in tasks)
            {
                if (item.DateInfo.Year == monthCalendar1.SelectionRange.Start.Year &&
                    item.DateInfo.Month == monthCalendar1.SelectionRange.Start.Month &&
                    item.DateInfo.Day == monthCalendar1.SelectionRange.Start.Day)
                    textBox1.Text += $"{item.DateInfo.Hour}:{item.DateInfo.Minute} {item.Task}{Environment.NewLine}";
            }
        }

        private void Serealize()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fs = new FileStream("Data.bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, tasks);
            }
        }

    }
}
