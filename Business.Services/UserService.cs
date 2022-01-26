using AutoMapper;
using Business.Entities;
using Business.Interop.Data;
using Business.Repositories.DataRepositories;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
        }

        public UserDto CreateUser(UserDto user)
        {
            var entity = _mapper.Map<User>(user);

            _userRepository.CreateOrUpdate(entity);

            return _mapper.Map<UserDto>(entity);
        }

        public UserDto UpdateUser(UserDto user)
        {
            var entity = _mapper.Map<User>(user);

            _userRepository.CreateOrUpdate(entity);

            return _mapper.Map<UserDto>(entity);
        }

        public IEnumerable<UserDto> FindByFullName(string fullName)
        {
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(_userRepository.Query().Where(u => u.FullName.Contains(fullName, System.StringComparison.InvariantCultureIgnoreCase)));
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _mapper.Map<List<User>, IEnumerable<UserDto>>(_userRepository.Query());
        }

        public void DeleteById(int id)
        {
            _userRepository.Delete(_userRepository.Read(id));
        }

        public UserDto GetById(int id)
        {
            return _mapper.Map<User, UserDto>(_userRepository.Read(id));
        }

        public UserDto GetByEmail(string email)
        {
            return _mapper.Map<User, UserDto>(_userRepository.Query().Where(u => u.Email.ToLower() == email.ToLower()).FirstOrDefault());
        }
    }
}