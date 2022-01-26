using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SportHelper.Models
{
    public class UserTimeRangeModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        public DateTime End { get; set; }
    }
}
