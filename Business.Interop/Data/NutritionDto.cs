using System;
using System.ComponentModel.DataAnnotations;


namespace Business.Interop.Data
{
    public class NutritionDto
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Это обязательное поле")]
        public DateTime Time { get; set; }



        [Required(ErrorMessage = "Это обязательное поле")]
        [Range(0, float.MaxValue, ErrorMessage = "Некорректный ввод")]
        public float Proteins { get; set; }



        [Required(ErrorMessage = "Это обязательное поле")]
        [Range(0, float.MaxValue, ErrorMessage = "Некорректный ввод")]
        public float Fats { get; set; }



        [Required(ErrorMessage = "Это обязательное поле")]
        [Range(0, float.MaxValue, ErrorMessage = "Некорректный ввод")]
        public float Carbohydrates { get; set; }



        [Required(ErrorMessage = "Это обязательное поле")]
        [Range(0, float.MaxValue, ErrorMessage = "Некорректный ввод")]
        public float Calories { get; set; }



        
        public UserDto User { get; set; }


        [Required(ErrorMessage = "Это обязательное поле")]
        public int UserId { get; set; }
    }
}