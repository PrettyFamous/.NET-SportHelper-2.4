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
    public class TrainingRepository : AbstractRepository<Training, int>, ITrainingRepository
    {
		public TrainingRepository(Context context)
		{
			_context = context;
		}

		protected override Training ReadImplementation(int key)
		{
			return QueryImplementation().FirstOrDefault(ts => ts.Id == key);
		}
		protected override async Task<Training> ReadImplementationAsync(int key)
		{
			return await QueryImplementation().FirstOrDefaultAsync(ts => ts.Id == key);
		}

		protected override void CreateImplementation(Training value)
		{
			_context.TrainingSessions.Add(value);
		}
		protected override async Task CreateImplementationAsync(Training value)
		{
			await _context.TrainingSessions.AddAsync(value);
		}

		protected override void UpdateImplementation(Training value)
		{
			_context.Update(value);
		}

		protected override void DeleteImplementation(Training value)
		{
			var entity = ReadImplementation(value.Id);

			if (entity != null)
			{
				_context.TrainingSessions.Remove(entity);
			}
		}

		protected override IQueryable<Training> QueryImplementation()
		{
			return _context.TrainingSessions.Include(ts => ts.User).Include(ts => ts.TrainingExercises);
		}

		protected override int KeySelector(Training entity) => entity.Id;
	}
}
