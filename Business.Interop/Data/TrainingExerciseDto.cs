using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interop.Data
{
    public class TrainingExerciseDto
    {
        public int Id { get; set; }



        [Range(0, int.MaxValue, ErrorMessage = "Некорректный ввод")]
        public int? TotalReps { get; set; }



        [Range(0, int.MaxValue, ErrorMessage = "Некорректный ввод")]
        public int? Sets { get; set; }



        [Range(0, float.MaxValue, ErrorMessage = "Некорректный ввод")]
        public float? TotalMovedWeight { get; set; }


        [Range(0, float.MaxValue, ErrorMessage = "Некорректный ввод")]
        public float? TotalDistance { get; set; }


        public TrainingDto Session { get; set; }
        [Required(ErrorMessage = "Необходимо выбрать тренировку")]
        public int SessionId { get; set; }


        public ExerciseDto Exercise { get; set; }
        [Required(ErrorMessage = "Необходимо выбрать упражнение")]


        public int ExerciseId { get; set; }
    }
}
