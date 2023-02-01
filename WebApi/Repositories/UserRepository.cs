using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataContext _context;

        public UserRepository(IDataContext context)
        {
            _context = context;
        }

        async Task IUserRepository.Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

        }

        async Task IUserRepository.Delete(int id)
        {

            var thresUser = await _context.Users.FindAsync(id);
            if(thresUser == null)
            {
                throw new NullReferenceException();
            }
            _context.Users.Remove(thresUser);
            await _context.SaveChangesAsync();

        }

        async Task<User> IUserRepository.Get(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        async Task<IEnumerable<User>> IUserRepository.GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        async Task IUserRepository.Update(User user)
        {
            var userUpdate = await _context.Users.FindAsync(user.Id);
            if(userUpdate == null)
            {
                throw new NullReferenceException();
            }

            userUpdate.FirstName = user.FirstName;
            userUpdate.SurName = user.SurName;
            userUpdate.Id = user.Id;
            userUpdate.Month = user.Month;
            userUpdate.Descriptor = user.Descriptor;

        }
    }
}
