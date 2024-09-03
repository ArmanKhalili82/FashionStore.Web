using FashionStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Business.UserService;

public interface IUserService
{
    Task Register(User user);
    Task<User> Login(string UserName, string Password);
}
