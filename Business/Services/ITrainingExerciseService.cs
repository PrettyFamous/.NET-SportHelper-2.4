using Business.Interop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ITrainingExerciseService
    {
        public TrainingExerciseDto CreateTrainingExercise(TrainingExerciseDto trainingExercise);
        public TrainingExerciseDto UpdateTrainingExercise(TrainingExerciseDto trainingExercise);
        public IEnumerable<TrainingExerciseDto> GetAll();
        public void DeleteById(int id);
        public TrainingExerciseDto GetById(int id);
    }
}
