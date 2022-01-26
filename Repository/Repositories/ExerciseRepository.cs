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
    public class ExerciseRepository : AbstractRepository<Exercise, int>, IExerciseRepository
    {
		public ExerciseRepository(Context context)
		{
			_context = context;
		}

		protected override Exercise ReadImplementation(int key)
		{
			return QueryImplementation().FirstOrDefault(te => te.Id == key);
		}
		protected override async Task<Exercise> ReadImplementationAsync(int key)
		{
			return await QueryImplementation().FirstOrDefaultAsync(te => te.Id == key);
		}

		protected override void CreateImplementation(Exercise value)
		{
			_context.TrainingExercises.Add(value);
		}
		protected override async Task CreateImplementationAsync(Exercise value)
		{
			await _context.TrainingExercises.AddAsync(value);
		}

		protected override void UpdateImplementation(Exercise value)
		{
			_context.Update(value);
		}

		protected override void DeleteImplementation(Exercise value)
		{
			var entity = ReadImplementation(value.Id);

			if (entity != null)
			{
				_context.TrainingExercises.Remove(entity);
			}
		}

		protected override IQueryable<Exercise> QueryImplementation()
		{
			return _context.TrainingExercises.Include(te => te.TrainingExercises);
		}

		protected override int KeySelector(Exercise entity) => entity.Id;
	}
}
