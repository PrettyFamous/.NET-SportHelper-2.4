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
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseRepository repository, IMapper mapper)
        {
            _exerciseRepository = repository;
            _mapper = mapper;
        }

        public ExerciseDto CreateExercise(ExerciseDto trainingExercise)
        {
            var entity = _mapper.Map<Exercise>(trainingExercise);

            _exerciseRepository.CreateOrUpdate(entity);

            return _mapper.Map<ExerciseDto>(entity);
        }

        public ExerciseDto UpdateExercise(ExerciseDto trainingExercise)
        {
            var entity = _mapper.Map<Exercise>(trainingExercise);

            _exerciseRepository.CreateOrUpdate(entity);

            return _mapper.Map<ExerciseDto>(entity);
        }

        public IEnumerable<ExerciseDto> GetAll()
        {
            return _mapper.Map<List<Exercise>, IEnumerable<ExerciseDto>>(_exerciseRepository.Query());
        }

        public void DeleteById(int id)
        {
            _exerciseRepository.Delete(_exerciseRepository.Read(id));
        }

        public ExerciseDto GetById(int id)
        {
            return _mapper.Map<Exercise, ExerciseDto>(_exerciseRepository.Read(id));
        }

        public ExerciseDto GetByName(string name)
        {
            return _mapper.Map<Exercise, ExerciseDto>(_exerciseRepository.Query().Where(te => te.Name.ToUpper() == name.ToUpper()).FirstOrDefault());
        }
    }
}
