using AutoMapper;
using Business.Entities;
using Business.Interop.Data;
using Business.Repositories.DataRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class TrainingExerciseService : ITrainingExerciseService
    {
        private readonly ITrainingExerciseRepository _trainingExerciseRepository;
        private readonly IMapper _mapper;

        public TrainingExerciseService(ITrainingExerciseRepository repository, IMapper mapper)
        {
            _trainingExerciseRepository = repository;
            _mapper = mapper;
        }

        public TrainingExerciseDto CreateTrainingExercise(TrainingExerciseDto trainingExercise)
        {
            var entity = _mapper.Map<TrainingExercise>(trainingExercise);

            _trainingExerciseRepository.CreateOrUpdate(entity);

            return _mapper.Map<TrainingExerciseDto>(entity);
        }

        public TrainingExerciseDto UpdateTrainingExercise(TrainingExerciseDto trainingExercise)
        {
            var entity = _mapper.Map<TrainingExercise>(trainingExercise);

            _trainingExerciseRepository.CreateOrUpdate(entity);

            return _mapper.Map<TrainingExerciseDto>(entity);
        }

        public IEnumerable<TrainingExerciseDto> GetAll()
        {
            return _mapper.Map<List<TrainingExercise>, IEnumerable<TrainingExerciseDto>>(_trainingExerciseRepository.Query());
        }

        public void DeleteById(int id)
        {
            _trainingExerciseRepository.Delete(_trainingExerciseRepository.Read(id));
        }

        public TrainingExerciseDto GetById(int id)
        {
            return _mapper.Map<TrainingExercise, TrainingExerciseDto>(_trainingExerciseRepository.Read(id));
        }
    }
}
