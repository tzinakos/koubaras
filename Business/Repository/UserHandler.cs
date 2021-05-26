namespace Business.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Models;
    using IRepository;
    using AutoMapper;
    using DataAccess;
    using DataAccess.Models;

    public class UserHandler : IHandle<UserDTO>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserHandler(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserDTO> Create(UserDTO user)
        {
            var newUser = _mapper.Map<UserDTO,User>(user);

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return _mapper.Map<User, UserDTO>(newUser);
        }

        public async Task<UserDTO> Update(UserDTO user)
        {
            var userDetails = await _db.Users.FindAsync(user.Id);

            userDetails.FirstName = user.FirstName ?? userDetails.FirstName;
            userDetails.LastName = user.LastName ?? userDetails.LastName;
            userDetails.Age = user.Age ?? userDetails.Age;

            _db.Users.Update(userDetails);
            await _db.SaveChangesAsync();

            return _mapper.Map<User, UserDTO>(userDetails);
        }

        public async Task<int> Delete(Guid userId)
        {
            var user = await _db.Users.FindAsync(userId);

            try
            {
                _db.Users.Remove(user);
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<UserDTO> Get(Guid userId)
        {
            try
            {
                var user = await _db.Users.FindAsync(userId);
                return _mapper.Map<User, UserDTO>(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ICollection<UserDTO>> GetAll()
        {
            try
            {
                var users = await _db.Users.ToListAsync();
                return _mapper.Map<ICollection<User>,ICollection<UserDTO>>(users);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
