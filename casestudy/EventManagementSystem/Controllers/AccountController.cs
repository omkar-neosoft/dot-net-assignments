using System.Threading.Tasks;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    #region user

    [HttpGet]
    public IActionResult Register() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model) {
        if (!ModelState.IsValid) {
            return View(model);
        }

        var user = new ApplicationUser {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email,
            EmailConfirmed = true // Automatically confirm email on registration
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded) {
            // Assign the "User" role to all normal users
            await _userManager.AddToRoleAsync(user, "User");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors) {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult LogIn() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LoginViewModel loginModel) {
        if (ModelState.IsValid) {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
            if (result.Succeeded) {
                return RedirectToAction("Index", "Event");
            }
            ModelState.AddModelError("", "LoginFailed");
        }
        return View(loginModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOut() {
        await _signInManager.SignOutAsync();
        return RedirectToAction("LogIn");
    }

    #endregion

    #region organizer

    [Authorize(Roles = "Admin")] // Only Admin can add Organizers
    [HttpGet]
    public IActionResult RegisterOrganizer() {
        return View();
    }

    [Authorize(Roles = "Admin")] // Only Admin can add Organizers
    [HttpPost]
    public async Task<IActionResult> RegisterOrganizer(RegisterViewModel model) {
        if (!ModelState.IsValid) {
            return View(model);
        }

        var user = new ApplicationUser {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded) {
            await _userManager.AddToRoleAsync(user, "Organizer"); // Assign Organizer Role
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors) {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    #endregion
}


#region old code 1

//using EventManagementSystem.Models;
//using EventManagementSystem.ViewModels;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using NuGet.Protocol;

//[Authorize(Roles = "Admin")]
//public class AccountController : Controller {
//    private readonly UserManager<ApplicationUser> _userManager;
//    private readonly SignInManager<ApplicationUser> _signInManager;
//    private readonly RoleManager<IdentityRole> _roleManager;

//    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) {
//        _userManager = userManager;
//        _signInManager = signInManager;
//        _roleManager = roleManager;
//    }

//    #region user

//    [HttpGet]
//    public IActionResult Register() {
//        return View();
//    }

//    [HttpPost]
//    public async Task<IActionResult> Register(RegisterViewModel model) {
//        if (!ModelState.IsValid) {
//            return View(model);
//        }

//        var user = new ApplicationUser {
//            FirstName = model.FirstName,
//            LastName = model.LastName,
//            Email = model.Email,
//            UserName = model.Email,
//            EmailConfirmed = true // Automatically confirm email on registration
//        };

//        var result = await _userManager.CreateAsync(user, model.Password);

//        if (result.Succeeded) {
//            await _signInManager.SignInAsync(user, isPersistent: false);
//            return RedirectToAction("Index", "Home");
//        }

//        foreach (var error in result.Errors) {
//            ModelState.AddModelError("", error.Description);
//        }

//        return View(model);
//    }
//    [HttpGet]
//    public IActionResult LogIn() {
//        return View();
//    }
//    [HttpPost]
//    public async Task<IActionResult> LogIn(LoginViewModel loginModel) {
//        if (ModelState.IsValid) {
//            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
//            if (result.Succeeded) {
//                return RedirectToAction("Privacy", "Home");
//            }
//            ModelState.AddModelError("", "LoginFailed");
//            ModelState.AddModelError("", result.ToJson());
//        }
//        return View(loginModel);
//    }
//    public async Task<IActionResult> LogOut() {
//        await _signInManager.SignOutAsync();
//        return RedirectToAction("LogIn");
//    }

//    #endregion

//    #region organizer

//    [HttpGet]
//    public IActionResult RegisterOrganizer() {
//        return View();
//    }

//    [HttpPost]
//    public async Task<IActionResult> RegisterOrganizer(RegisterViewModel model) {
//        if (!ModelState.IsValid) {
//            return View(model);
//        }

//        var user = new ApplicationUser {
//            FirstName = model.FirstName,
//            LastName = model.LastName,
//            Email = model.Email,
//            UserName = model.Email,
//            EmailConfirmed = true
//        };

//        var result = await _userManager.CreateAsync(user, model.Password);

//        if (result.Succeeded) {
//            await _userManager.AddToRoleAsync(user, "Organizer"); // Assign Organizer Role
//            return RedirectToAction("Index", "Home");
//        }

//        foreach (var error in result.Errors) {
//            ModelState.AddModelError("", error.Description);
//        }

//        return View(model);
//    }

//    #endregion
//}


#endregion

#region old code
//using System.Data;
//using EventManagementSystem.Constants;
//using EventManagementSystem.Models;
//using EventManagementSystem.ViewModels;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using NuGet.Protocol;

//namespace EventManagementSystem.Controllers {
//    public class AccountController : Controller {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;

//        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) {
//            _userManager = userManager;
//            _signInManager = signInManager;
//        }

//        [HttpGet]
//        public IActionResult Register() {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel model) {
//            if (!ModelState.IsValid) {
//                return View(model);
//            }

//            var user = new ApplicationUser {
//                FirstName = model.FirstName,
//                LastName = model.LastName,
//                Email = model.Email,
//                UserName = model.Email,
//                EmailConfirmed = true // Automatically confirm email on registration
//            };

//            var result = await _userManager.CreateAsync(user, model.Password);

//            if (result.Succeeded) {
//                await _signInManager.SignInAsync(user, isPersistent: false);
//                return RedirectToAction("Index", "Home");
//            }

//            foreach (var error in result.Errors) {
//                ModelState.AddModelError("", error.Description);
//            }

//            return View(model);
//        }
//        [HttpGet]
//        public IActionResult LogIn() {
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> LogIn(LoginViewModel loginModel) {
//            if (ModelState.IsValid) {
//                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
//                if (result.Succeeded) {
//                    return RedirectToAction("Privacy", "Home");
//                }
//                ModelState.AddModelError("", "LoginFailed");
//                ModelState.AddModelError("", result.ToJson());
//            }
//            return View(loginModel);
//        }
//        public async Task<IActionResult> LogOut() {
//            await _signInManager.SignOutAsync();
//            return RedirectToAction("LogIn");
//        }

//    }
//    //41776008-6086-4dbf-b923-2879a6680b9a
//}

#endregion