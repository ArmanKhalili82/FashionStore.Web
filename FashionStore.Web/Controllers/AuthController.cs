using FashionStore.Models.Models;
using FashionStore.ViewModels;
using FashionStore.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.Web.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtService _jwtService;

    public AuthController(SignInManager<ApplicationUser> signInManager,
                             UserManager<ApplicationUser> userManager,
                             JwtService jwtService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login() => View();

    // POST: /Account/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Return the view with validation errors
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            // Handle user not found case
            ModelState.AddModelError(string.Empty, "User does not exist.");
            return View(model);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded)
        {
            // Handle login failures (e.g., invalid password)
            ModelState.AddModelError(string.Empty, "Invalid credentials.");
            return View(model);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateJwtToken(user, roles);

        // Return JWT token as JSON or redirect with success
        return RedirectToAction("Index", "Home");









        //if (!ModelState.IsValid) return View(model);

        //var user = await _userManager.FindByEmailAsync(model.Email);
        //if (user != null)
        //{
        //    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        //    if (result.Succeeded)
        //    {
        //        var roles = await _userManager.GetRolesAsync(user);
        //        var token = _jwtService.GenerateJwtToken(user, roles);

        //        // Store the JWT token in the client-side
        //        ViewBag.Token = token;

        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //return View(model);
    }

    // GET: /Account/Register
    [HttpGet]
    public IActionResult Register() => View();

    // POST: /Account/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Return validation errors to the view
            return View(model);
        }

        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            // Handle email already in use
            ModelState.AddModelError(string.Empty, "Email is already in use.");
            return View(model);
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            // Handle creation errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return RedirectToAction("Index", "Home");










        //if (!ModelState.IsValid) return View(model);

        //var user = new ApplicationUser
        //{
        //    UserName = model.Email,
        //    Email = model.Email
        //};

        //var result = await _userManager.CreateAsync(user, model.Password);
        //if (result.Succeeded)
        //{
        //    await _signInManager.SignInAsync(user, isPersistent: false);
        //    return RedirectToAction("Index", "Home");
        //}

        //foreach (var error in result.Errors)
        //{
        //    ModelState.AddModelError(string.Empty, error.Description);
        //}

        //return View(model);
    }

    // POST: /Account/Logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }












    //private IUserService _userService;

    //public AuthController(IUserService userService)
    //{
    //    _userService = userService;
    //}

    //[HttpGet]
    //public IActionResult Login()
    //{
    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> Login(CreateUserViewModel user)
    //{
    //    var userFromDb = await _userService.Login(user.UserName, user.Password);
    //    HttpContext.Session.SetInt32("userId", userFromDb.Id);
    //    HttpContext.Session.SetString("userName", userFromDb.UserName);
    //    return RedirectToAction("Index", "Home");
    //}

    //[HttpGet]
    //public IActionResult Logout()
    //{
    //    HttpContext.Session.Clear();
    //    return RedirectToAction("Login");
    //}

    //[HttpGet]
    //public IActionResult Register()
    //{
    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> Register(CreateUserViewModel vm)
    //{
    //    var user = new User
    //    {
    //        UserName = vm.UserName,
    //        PasswordHash = vm.Password,
    //        Email = vm.Email
    //    };
    //    await _userService.Register(user);
    //    return RedirectToAction("Login");
    //}
}
