using Business.Interop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface INutritionService
    {
        public NutritionDto CreateNutrition(NutritionDto nutrition);
        public NutritionDto UpdateNutrition(NutritionDto nutrition);
        public IEnumerable<NutritionDto> GetAll();
        public void DeleteById(int id);
        public NutritionDto GetById(int id);
        public NutritionDto GetByUserIdAndTime(int userId, DateTime time);
    }
}
