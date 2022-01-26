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
    public class UserRepository : AbstractRepository<User, int>, IUserRepository
    {
        public UserRepository(Context context)
        {
            _context = context;
        }

		protected override User ReadImplementation(int key)
		{
			return QueryImplementation().FirstOrDefault(u => u.Id == key);
		}
		protected override async Task<User> ReadImplementationAsync(int key)
		{
			return await QueryImplementation().FirstOrDefaultAsync(u => u.Id == key);
		}

		protected override void CreateImplementation(User value)
		{
			_context.Users.Add(value);
		}
		protected override async Task CreateImplementationAsync(User value)
		{
			await _context.Users.AddAsync(value);
		}

		protected override void UpdateImplementation(User value)
		{
			_context.Update(value);
		}

		protected override void DeleteImplementation(User value)
		{
			var entity = ReadImplementation(value.Id);

			if (entity != null)
			{
				_context.Users.Remove(entity);
			}
		}

		protected override IQueryable<User> QueryImplementation()
		{
			return _context.Users.Include(u => u.Meals).Include(u => u.SleepSessions).Include(u => u.Trainings);
		}

		protected override int KeySelector(User entity) => entity.Id;
	}
}
