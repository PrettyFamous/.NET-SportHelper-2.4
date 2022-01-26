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
    public class TrainingExerciseRepository : AbstractRepository<TrainingExercise, int>, ITrainingExerciseRepository
    {
		public TrainingExerciseRepository(Context context)
		{
			_context = context;
		}

		protected override TrainingExercise ReadImplementation(int key)
		{
			return QueryImplementation().FirstOrDefault(tse => tse.Id == key);
		}
		protected override async Task<TrainingExercise> ReadImplementationAsync(int key)
		{
			return await QueryImplementation().FirstOrDefaultAsync(tse => tse.Id == key);
		}

		protected override void CreateImplementation(TrainingExercise value)
		{
			_context.TrainingSessionExercises.Add(value);
		}
		protected override async Task CreateImplementationAsync(TrainingExercise value)
		{
			await _context.TrainingSessionExercises.AddAsync(value);
		}

		protected override void UpdateImplementation(TrainingExercise value)
		{
			_context.Update(value);
		}

		protected override void DeleteImplementation(TrainingExercise value)
		{
			var entity = ReadImplementation(value.Id);

			if (entity != null)
			{
				_context.TrainingSessionExercises.Remove(entity);
			}
		}

		protected override IQueryable<TrainingExercise> QueryImplementation()
		{
			return _context.TrainingSessionExercises.Include(tse => tse.Session).ThenInclude(s => s.User).Include(tse => tse.Exercise);
		}

		protected override int KeySelector(TrainingExercise entity) => entity.Id;
	}
}
