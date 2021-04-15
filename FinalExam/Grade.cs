using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExam
{
    class Grade
    {
        private double mid_Term;
        private double final;
        private double project;


        public double MidTerm { get => mid_Term; set => mid_Term = value; }
        public double Final { get => final; set => final = value; }
        public double Project { get => project; set => project = value; }


        public double total_Marks()
        {
            return MidTerm + Final + Project;
        }

        public double marksPercentage(double marks, double total)
        {
            return (marks / 100 * total);
        }

        public char gradePoint(double totalPerc)
        {
            if (totalPerc >= 90 && totalPerc <= 100)
            {
                return 'A';
            }
            else if (totalPerc >= 80 && totalPerc < 90)
            {
                return 'B';
            }
            else if (totalPerc >= 70 && totalPerc < 80)
            {
                return 'C';
            }
            else if (totalPerc >= 60 && totalPerc < 70)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }
        }

    }
}

