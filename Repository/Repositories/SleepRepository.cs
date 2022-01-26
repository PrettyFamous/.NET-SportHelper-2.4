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
    public class SleepRepository : AbstractRepository<Sleep, int>, ISleepRepository
    {
		public SleepRepository(Context context)
		{
			_context = context;
		}

		protected override Sleep ReadImplementation(int key)
		{
			return QueryImplementation().FirstOrDefault(ss => ss.Id == key);
		}
		protected override async Task<Sleep> ReadImplementationAsync(int key)
		{
			return await QueryImplementation().FirstOrDefaultAsync(ss => ss.Id == key);
		}

		protected override void CreateImplementation(Sleep value)
		{
			_context.SleepSessions.Add(value);
		}
		protected override async Task CreateImplementationAsync(Sleep value)
		{
			await _context.SleepSessions.AddAsync(value);
		}

		protected override void UpdateImplementation(Sleep value)
		{
			_context.Update(value);
		}

		protected override void DeleteImplementation(Sleep value)
		{
			var entity = ReadImplementation(value.Id);

			if (entity != null)
			{
				_context.SleepSessions.Remove(entity);
			}
		}

		protected override IQueryable<Sleep> QueryImplementation()
		{
			return _context.SleepSessions.Include(ss => ss.User);
		}

		protected override int KeySelector(Sleep entity) => entity.Id;
	}
}
