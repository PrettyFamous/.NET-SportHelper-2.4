using Business.Entities;
using Business.Repositories.DataRepositories;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class NutritionRepository : AbstractRepository<Nutrition, int>, INutritionRepository
    {
		public NutritionRepository(Context context)
		{
			_context = context;
		}

		protected override Nutrition ReadImplementation(int key)
		{
			return QueryImplementation().FirstOrDefault(m => m.Id == key);
		}
		protected override async Task<Nutrition> ReadImplementationAsync(int key)
		{
			return await QueryImplementation().FirstOrDefaultAsync(m => m.Id == key);
		}

		protected override void CreateImplementation(Nutrition value)
		{
			_context.Meals.Add(value);
		}
		protected override async Task CreateImplementationAsync(Nutrition value)
		{
			await _context.Meals.AddAsync(value);
		}

		protected override void UpdateImplementation(Nutrition value)
		{
			_context.Update(value);
		}

		protected override void DeleteImplementation(Nutrition value)
		{
			var entity = ReadImplementation(value.Id);

			if (entity != null)
			{
				_context.Meals.Remove(entity);
			}
		}

		protected override IQueryable<Nutrition> QueryImplementation()
		{
			return _context.Meals.Include(m => m.User);
		}

		protected override int KeySelector(Nutrition entity) => entity.Id;
	}
}
