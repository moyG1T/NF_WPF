using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF_WPF.Data
{
    public partial class Discipline
    {
        public int SpecialityCount
        {
            get
            {
                return Request.Count;
            }

        }
    }
}
