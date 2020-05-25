using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Media;
using System.IO;

namespace Ecilipse_alarm_clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        System.Threading.Timer TheTimer = null;
        private DateTime C1EventDateTime = new DateTime();
        private DateTime C2EventDateTime = new DateTime();
        private DateTime C3EventDateTime = new DateTime();
        private DateTime C4EventDateTime = new DateTime();
        private DateTime MaxEclipseDateTime = new DateTime();


        private void Form1_Load(object sender, EventArgs e)
        {
            TheTimer = new System.Threading.Timer(this.Tick, null, 0, 1000);
        }

        private void Form1_Closed(object sender, EventArgs e)
        {
            TheTimer.Dispose();
        }

        public void Tick(object info)
        {
            this.Invoke((Action)this.UpdateCountdown);
        }

        private void UpdateCountdown()
        {
            //update Current time
            label_now_time.Text = paddingTimeFormat(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            //Countdown for specific events
            C1EventDateTime = this.dateTimePicker1.Value;
            C2EventDateTime = this.dateTimePicker2.Value;
            MaxEclipseDateTime = this.dateTimePicker3.Value;
            C3EventDateTime = this.dateTimePicker4.Value;
            C4EventDateTime = this.dateTimePicker5.Value;
            SoundPlayer player = new SoundPlayer();

            TimeSpan remaining1 = C1EventDateTime - DateTime.Now;
            TimeSpan remaining2 = C2EventDateTime - DateTime.Now;
            TimeSpan remaining3 = MaxEclipseDateTime - DateTime.Now;
            TimeSpan remaining4 = C3EventDateTime - DateTime.Now;
            TimeSpan remaining5 = C4EventDateTime - DateTime.Now;

            if (remaining1.TotalSeconds > 0)
            {
                groupBox2.Text = "初虧倒數";
                label_remain_time.Text = paddingTimeFormat(remaining1.Hours, remaining1.Minutes, remaining1.Seconds);

                if (remaining1.Hours == 0 && remaining1.Minutes == 5 && remaining1.Seconds == 0)
                {
                    player.SoundLocation = @"5分鐘後初虧.wav";
                  //  player.LoadAsync();
                    player.Play();
                }

                if (remaining1.Hours == 0 && remaining1.Minutes == 1 && remaining1.Seconds == 0)
                {
                    player.SoundLocation = @"1分鐘後初虧.wav";
                    player.Play();
                }

                if (remaining1.Hours == 0 && remaining1.Minutes == 0 && remaining1.Seconds == 6)
                {
                    player.SoundLocation = @"初虧54321.wav";
                    player.Play();
                }
            }

            if (remaining2.TotalSeconds > 0 && remaining1.TotalSeconds < 0)
            {
                groupBox2.Text = "日環食倒數";
                label_remain_time.Text = paddingTimeFormat(remaining2.Hours, remaining2.Minutes, remaining2.Seconds);

                if (remaining2.Hours == 0 && remaining2.Minutes == 5 && remaining2.Seconds == 0)
                {
                    player.SoundLocation = @"5分鐘後日環食.wav";
                    player.Play();
                }

                if (remaining2.Hours == 0 && remaining2.Minutes == 1 && remaining2.Seconds == 0)
                {
                    player.SoundLocation = @"1分鐘後日環食.wav";
                    player.Play();
                }

                if (remaining2.Hours == 0 && remaining2.Minutes == 0 && remaining2.Seconds == 6)
                {
                    player.SoundLocation = @"日環食543210.wav";
                    player.Play();
                }
            }
            if (remaining3.TotalSeconds > 0 && remaining2.TotalSeconds < 0)
            {
                groupBox2.Text = "食甚倒數";
                label_remain_time.Text = paddingTimeFormat(remaining3.Hours, remaining3.Minutes, remaining3.Seconds);
                if (remaining3.Hours == 0 && remaining3.Minutes == 0 && remaining3.Seconds == 6)
                {
                    player.SoundLocation = @"食甚543210.wav";
                    player.Play();
                }
            }

            if (remaining4.TotalSeconds > 0 && remaining3.TotalSeconds < 0)
            {
                groupBox2.Text = "日環食結束倒數";
                label_remain_time.Text = paddingTimeFormat(remaining4.Hours, remaining4.Minutes, remaining4.Seconds);
                if (remaining4.Hours == 0 && remaining4.Minutes == 0 && remaining4.Seconds == 6)
                {
                    player.SoundLocation = @"日環食結束543210.wav";
                    player.Play();
                }
            }

            if (remaining5.TotalSeconds > 0 && remaining4.TotalSeconds < 0)
            {
                groupBox2.Text = "復圓倒數";
                label_remain_time.Text = paddingTimeFormat(remaining5.Hours, remaining5.Minutes, remaining5.Seconds);

                if (remaining5.Hours == 0 && remaining5.Minutes == 5 && remaining5.Seconds == 0)
                {
                    player.SoundLocation = @"5分鐘後復圓.wav";
                    player.Play();
                }

                if (remaining5.Hours == 0 && remaining5.Minutes == 1 && remaining5.Seconds == 0)
                {
                    player.SoundLocation = @"1分鐘後復圓.wav";
                    player.Play();
                }

                if (remaining5.Hours == 0 && remaining5.Minutes == 0 && remaining5.Seconds == 6)
                {
                    player.SoundLocation = @"復圓54321.wav";
                    player.Play();
                }
            }
        }
        protected string paddingTimeFormat(int HH, int MM, int SS)
        {
            // time format
            string displaytime = "";

            //padding leading zero
            if (HH < 10)
            {
                displaytime += "0" + HH;
            }
            else
            {
                displaytime += HH;
            }
            displaytime += ":";

            if (MM < 10)
            {
                displaytime += "0" + MM;
            }
            else
            {
                displaytime += MM;
            }
            displaytime += ":";
            if (SS < 10)
            {
                displaytime += "0" + SS;
            }
            else
            {
                displaytime += SS;
            }
            //Return time string in 00:00:00 this format
            return displaytime;
        }

        private void btnSaveTimeSetting_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XmlTextWriter xwriter = new XmlTextWriter(saveFileDialog1.FileName, Encoding.Unicode);
                xwriter.WriteStartDocument();
                xwriter.WriteStartElement("XMLFILE");
                xwriter.WriteString("\r\n");

                xwriter.WriteStartElement("觀測地點");
                xwriter.WriteString(tBLocation.Text);
                xwriter.WriteEndElement();
                xwriter.WriteString("\r\n");

                xwriter.WriteStartElement("C1_EVENT_DATETIME");
                xwriter.WriteValue(dateTimePicker1.Value);
                //xwriter.WriteString(dateTimePicker1.Value.ToString());
                xwriter.WriteEndElement();
                xwriter.WriteString("\r\n");

                xwriter.WriteStartElement("C2_EVENT_DATETIME");
                xwriter.WriteValue(dateTimePicker2.Value);
                //xwriter.WriteString(this.dateTimePicker2.Value.ToString());
                xwriter.WriteEndElement();
                xwriter.WriteString("\r\n");

                xwriter.WriteStartElement("MAX_ECLIPSE_DATETIME");
                xwriter.WriteValue(dateTimePicker3.Value);
                //xwriter.WriteString(dateTimePicker3.Value.ToString());
                xwriter.WriteEndElement();
                xwriter.WriteString("\r\n");

                xwriter.WriteStartElement("C3_EVENT_DATETIME");
                xwriter.WriteValue(dateTimePicker4.Value);
                //xwriter.WriteString(dateTimePicker4.Value.ToString());
                xwriter.WriteEndElement();
                xwriter.WriteString("\r\n");

                xwriter.WriteStartElement("C4_EVENT_DATETIME");
                xwriter.WriteValue(dateTimePicker5.Value);
                //xwriter.WriteString(dateTimePicker5.Value.ToString());
                xwriter.WriteEndElement();
                xwriter.WriteString("\r\n");

                xwriter.WriteEndDocument();
                xwriter.Close();
            }
        }
        private void btnLoadTimeSetting_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                using (XmlReader reader = XmlReader.Create(openFileDialog1.FileName, settings))
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "觀測地點"))
                        {
                            string tempresult = reader.ReadElementContentAsString();
                            tBLocation.Text = tempresult;
                        }

                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "C1_EVENT_DATETIME"))
                        {
                            DateTime tempresult = reader.ReadElementContentAsDateTime();
                            dateTimePicker1.Value = tempresult;
                        }

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "C2_EVENT_DATETIME")
                        {
                            DateTime tempresult = reader.ReadElementContentAsDateTime();
                            dateTimePicker2.Value = tempresult;
                        }

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "MAX_ECLIPSE_DATETIME")
                        {
                            DateTime tempresult = reader.ReadElementContentAsDateTime();
                            dateTimePicker3.Value = tempresult;
                        }

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "C3_EVENT_DATETIME")
                        {
                            DateTime tempresult = reader.ReadElementContentAsDateTime();
                            dateTimePicker4.Value = tempresult;
                        }

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "C4_EVENT_DATETIME")
                        {
                            DateTime tempresult = reader.ReadElementContentAsDateTime();
                            dateTimePicker5.Value = tempresult;
                        }
                    }

                }
            }
        }
    }
}
