using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace linkedInTutorial.Models
{
    public class FormattingService
    {
        public string AsReadableDate(DateTime date)
        {
            return date.ToString("d");
        }
    }
}
