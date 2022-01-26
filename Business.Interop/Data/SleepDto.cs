using FoolProof.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class SleepDto
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Это обязательное поле")]
        public DateTime Start { get; set; }


        [Required(ErrorMessage = "Это обязательное поле")]
        [GreaterThan("Start", ErrorMessage = "Время конца должно быть больше, чем время начала")]
        public DateTime End { get; set; }


        public UserDto User { get; set; }


        [Required(ErrorMessage = "Это обязательное поле")]
        public int UserId { get; set; }
    }
}
