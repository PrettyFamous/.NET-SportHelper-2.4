using Business.Interop.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ITrainingService
    {
        public TrainingDto CreateTraining(TrainingDto training);
        public TrainingDto UpdateTraining(TrainingDto training);
        public IEnumerable<TrainingDto> GetAll();
        public void DeleteById(int id);
        public TrainingDto GetById(int id);
        public IEnumerable<TrainingDto> FindByUserIdAndTimeRange(int userId, DateTime start, DateTime end);
        public MemoryStream GetXlsxWithTrainings(int userId, DateTime start, DateTime end);
    }
}
