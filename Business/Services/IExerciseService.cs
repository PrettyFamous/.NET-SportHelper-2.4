using Business.Interop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IExerciseService
    {
        public ExerciseDto CreateExercise(ExerciseDto exercise);
        public ExerciseDto UpdateExercise(ExerciseDto exercise);
        public IEnumerable<ExerciseDto> GetAll();
        public void DeleteById(int id);
        public ExerciseDto GetById(int id);
        public ExerciseDto GetByName(string name);
    }
}
