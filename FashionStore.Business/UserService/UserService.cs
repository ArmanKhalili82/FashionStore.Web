using FashionStore.DataAccess.Data;
using FashionStore.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Business.UserService;

public class UserService : IUserService
{
    private ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> Login(string UserName, string Password)
    {
        return await _context.users.FirstOrDefaultAsync(x => x.UserName.ToLower() == UserName && x.PasswordHash == Password);
    }

    public async Task Register(User user)
    {
        await _context.users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
