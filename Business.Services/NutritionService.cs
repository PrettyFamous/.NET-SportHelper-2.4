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
    public class NutritionService : INutritionService
    {
        private readonly INutritionRepository _nutritionRepository;
        private readonly IMapper _mapper;

        public NutritionService(INutritionRepository repository, IMapper mapper)
        {
            _nutritionRepository = repository;
            _mapper = mapper;
        }

        public NutritionDto CreateNutrition(NutritionDto nutrition)
        {
            var entity = _mapper.Map<Nutrition>(nutrition);

            _nutritionRepository.CreateOrUpdate(entity);

            return _mapper.Map<NutritionDto>(entity);
        }

        public NutritionDto UpdateNutrition(NutritionDto nutrition)
        {
            var entity = _mapper.Map<Nutrition>(nutrition);

            _nutritionRepository.CreateOrUpdate(entity);

            return _mapper.Map<NutritionDto>(entity);
        }

        public IEnumerable<NutritionDto> GetAll()
        {
            return _mapper.Map<List<Nutrition>, IEnumerable<NutritionDto>>(_nutritionRepository.Query());
        }

        public void DeleteById(int id)
        {
            _nutritionRepository.Delete(_nutritionRepository.Read(id));
        }

        public NutritionDto GetById(int id)
        {
            return _mapper.Map<Nutrition, NutritionDto>(_nutritionRepository.Read(id));
        }

        public NutritionDto GetByUserIdAndTime(int userId, DateTime time)
        {
            return _mapper.Map<Nutrition, NutritionDto>(_nutritionRepository.Query().FirstOrDefault(m => m.UserId == userId && m.Time == time));
        }
    }
}
