using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Training
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public IEnumerable<TrainingExercise> TrainingExercises { get; set; }
    }
}
