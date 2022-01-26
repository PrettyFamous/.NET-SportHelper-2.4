using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Это обязательное поле")]
        public string FullName { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Это обязательное поле")]
        public DateTime Birthdate { get; set; }


        [Required(ErrorMessage = "Это обязательное поле")]
        [Range(40, 400, ErrorMessage = "Вес должен быть не меньше 40 и не больше 400 кг")]
        public float Weight { get; set; }


        [Required(ErrorMessage = "Это обязательное поле")]
        [Range(100, 280, ErrorMessage = "Рост должен быть не меньше 140 и не больше 280 см")]
        public float Height { get; set; }


        [Required(ErrorMessage = "Это обязательное поле")]
        [EmailAddress(ErrorMessage = "Недопустимый адрес электронной почты")]
        public string Email { get; set; }


        public IEnumerable<NutritionDto> Meals { get; set; }

        public IEnumerable<SleepDto> SleepSessions { get; set; }

        public IEnumerable<TrainingDto> TrainingSessions { get; set; }

    }
}
