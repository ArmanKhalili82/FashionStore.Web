using FashionStore.Business.UserService;
using FashionStore.Models.Models;
using FashionStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.Web.Controllers;

public class AuthController : Controller
{
    private IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(CreateUserViewModel user)
    {
        var userFromDb = await _userService.Login(user.UserName, user.Password);
        HttpContext.Session.SetInt32("userId", userFromDb.Id);
        HttpContext.Session.SetString("userName", userFromDb.UserName);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreateUserViewModel vm)
    {
        var user = new User
        {
            UserName = vm.UserName,
            PasswordHash = vm.Password,
            Email = vm.Email
        };
        await _userService.Register(user);
        return RedirectToAction("Login");
    }
}
