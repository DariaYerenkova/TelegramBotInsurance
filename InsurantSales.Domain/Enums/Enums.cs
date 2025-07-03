using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Domain.Enums
{
    public class Enums
    {
        public enum DocumentStep
        {
            None,
            WaitingForPassport,
            WaitingForCarDocument,
            Completed
        }
    }
}
