using Business.Interop.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IUserService
    {
        public UserDto CreateUser(UserDto user);
        public UserDto UpdateUser(UserDto user);
        public IEnumerable<UserDto> GetAll();
        public IEnumerable<UserDto> FindByFullName(string fullName);
        public void DeleteById(int id);
        public UserDto GetById(int id);
        public UserDto GetByEmail(string email);
    }
}
