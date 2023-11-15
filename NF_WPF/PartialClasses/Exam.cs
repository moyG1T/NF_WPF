using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF_WPF.Data
{
    public partial class Exam
    {
        public string ShowDiscipline
        {
            get
            {
                return Discipline.DName;
            }
        }
    }
}
