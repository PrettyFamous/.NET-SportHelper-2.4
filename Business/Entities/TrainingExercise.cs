using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class TrainingExercise
    {
        public int Id { get; set; }
        public int? TotalReps { get; set; }
        public int? Sets { get; set; }
        public float? TotalMovedWeight { get; set; }
        public float? TotalDistance { get; set; }

        public Training Session { get; set; }
        public int SessionId { get; set; }

        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
    }
}
