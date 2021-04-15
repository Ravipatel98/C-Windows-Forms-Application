using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Windows.Forms;
namespace FinalExam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Grade mygradeobj = new Grade();

        private void btn_RpEXIT_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_RpData_Click(object sender, EventArgs e)
        {
            var student_Name = txt_Rpnumber1.Text;
            var course_Number = txt_Rpnumber2.Text;
            var year = txt_Rpnumber3.Text;

            Regex rgxstudent_Name = new Regex(@"^[a-zA-Z0-9\s]{5,30}$");
            Regex rgxcourse_Number = new Regex(@"[0-9]{3}\-[A-Z][A-Z][0-9]\-[A-Z]");
            if (rgxstudent_Name.IsMatch(student_Name))
            {
                if (rgxcourse_Number.IsMatch(course_Number))
                {
                    
                        mygradeobj.MidTerm = Convert.ToDouble(txt_Rpnumber4.Text);
                        mygradeobj.Final = Convert.ToDouble(txt_Rpnumber6.Text);
                        mygradeobj.Project = Convert.ToDouble(txt_Rpnumber5.Text);

                        double totalPerc = (mygradeobj.total_Marks() / 300 * 100);

                        txt_Rpnumber7.Text = totalPerc.ToString();
                        txt_Rpnumber8.Text = mygradeobj.marksPercentage(mygradeobj.MidTerm, 30).ToString();
                        txt_Rpnumber9.Text = mygradeobj.marksPercentage(mygradeobj.Project, 30).ToString();
                        txt_Rpnumber10.Text = mygradeobj.marksPercentage(mygradeobj.Final, 40).ToString();

                        txt_Rpnumber11.Text = mygradeobj.gradePoint(totalPerc).ToString();
                 
                }
                else
                {
                    MessageBox.Show("PLEASE ENTER VALID COURSE ");
                }
            }
            else
            {
                MessageBox.Show("NAME IS NOT VALID!\n (5  to 30 characters).");
            }
        }
        string dir = @"./Sum20/";
        string filePath = @"./Sum20/Final.txt";

        private void btn_Rpriteadd_Click(object sender, EventArgs e)
        {
            var student_Name = txt_Rpnumber1.Text;
            var course_Number = txt_Rpnumber2.Text;
            var teacher = txt_Rpnumber3.Text;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(file);
            textOut.Write(student_Name + "|");
            textOut.Write(course_Number + "|");
            textOut.Write(comboBox2.SelectedItem + "|");
            textOut.Write(txt_Rpnumber3.Text + "|");

            textOut.Write(comboBox1.Text + "|");
            textOut.Write(txt_Rpnumber4.Text + "|");
            textOut.Write(txt_Rpnumber5.Text + "|");
            textOut.Write(txt_Rpnumber6.Text + "|");
            textOut.Write(txt_Rpnumber7.Text + "|");
            textOut.Write(txt_Rpnumber8.Text + "|");
            textOut.Write(txt_Rpnumber9.Text + "|");
            textOut.Write(txt_Rpnumber10.Text + "|");
            textOut.Write(txt_Rpnumber11.Text + "|");
            textOut.Close();
            if (file != null) { file.Close(); }
            MessageBox.Show("Write to Text File = OK");

        }

        private void btn_RPcreatwrite_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists("Test.xml"))
            {

                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader textIn = new StreamReader(fs);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true; settings.IndentChars = (" ");
                settings.NewLineOnAttributes = true;


                XmlWriter xmlOut = XmlWriter.Create(dir + "Final.xml", settings);

                xmlOut.WriteStartDocument();
                xmlOut.WriteStartElement("Root");
                string[] columns;
                int i = 0;




                while (textIn.Peek() != -1)
                {
                    columns = textIn.ReadLine().Split('|');

                    {
                        xmlOut.WriteStartElement("Grade");
                        xmlOut.WriteElementString("StudentName", columns[i]);
                        xmlOut.WriteElementString("CourseNumber", columns[++i]);
                        xmlOut.WriteElementString("Teacher: Mihai mufti");

                        xmlOut.WriteElementString("Session", columns[++i]);
                        xmlOut.WriteElementString("Year", columns[++i]);
                        xmlOut.WriteElementString("MidTermMarks", columns[++i]);
                        xmlOut.WriteElementString("ProjectMarks", columns[++i]);
                        xmlOut.WriteElementString("FinalMarks", columns[++i]);
                        xmlOut.WriteElementString("TotalPercentage", columns[++i]);
                        xmlOut.WriteElementString("MidTermPercentage", columns[++i]);
                        xmlOut.WriteElementString("ProjectPercentage", columns[++i]);
                        xmlOut.WriteElementString("FinalPercentage", columns[++i]);

                        xmlOut.WriteElementString("FinalGrade", columns[++i]);

                        xmlOut.WriteEndElement();
                        i++;
                    }

                }

                textIn.Close();
                if (fs != null) { fs.Close(); }
                xmlOut.WriteEndElement();
                xmlOut.Close();
            }

            MessageBox.Show("WriTTEN TO XML file");

        }

        private void btn_RpRead_Click(object sender, EventArgs e)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader xmlIn = XmlReader.Create(dir + "Final.xml", settings);
            string Val1 = " ", Val2 = " ", Val3 = " ", Val4 = " ", Val5 = " ", Val6 = " ", Val7 = " ", Val8 = " ", Val9 = " ", Val10 = " ", Val11 = " ", Val12 = " ", Val13 = " ", tempStr = " ";
            
            if (xmlIn.ReadToDescendant("Grade"))
            {
                do
                {
                    Val1 = xmlIn.ReadElementContentAsString();
                    Val2 = xmlIn.ReadElementContentAsString();
                    Val3 = xmlIn.ReadElementContentAsString();
                    Val4 = xmlIn.ReadElementContentAsString();
                    Val5 = xmlIn.ReadElementContentAsString();
                    Val6 = xmlIn.ReadElementContentAsString();
                    Val7 = xmlIn.ReadElementContentAsString();
                    Val8 = xmlIn.ReadElementContentAsString();
                    Val9 = xmlIn.ReadElementContentAsString();
                    Val10 = xmlIn.ReadElementContentAsString();
                    Val11 = xmlIn.ReadElementContentAsString();
                    Val12 = xmlIn.ReadElementContentAsString();
                    Val13 = xmlIn.ReadElementContentAsString();
                    tempStr += "Student Name: " + Val1 + "\nCourse Number: " + Val2 + "\nSession: " + Val3 + "\nTeacher: " + Val4 + "\nYear: " + Val5 + "\nMid term marks: " + Val6 + "\nProject marks: " + Val7 + "\nFinal marks: " + Val8 + "\nTotal: " + Val9 + "\nMid term percentage: " + Val10 + "\nProject percentage: " + Val11 + "\nFinal percentage: " + Val12 + "\nFinal Grade: " + Val13 + "\r\n\n";
                } while (xmlIn.ReadToNextSibling("Grade"));
            }
            MessageBox.Show(tempStr);
            xmlIn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
