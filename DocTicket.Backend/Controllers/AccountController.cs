using AutoMapper;
using DocTicket.Backend.Models;
using DocTicket.Backend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DocTicket.Backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }


        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user == null)
            {
                TempData["Error"] = "Неверный Email.";
                return View(loginVM);
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

            if (!passwordCheck)
            {
                TempData["Error"] = "Неверный пароль.";
                return View(loginVM);
            }

            var resut = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

            if (resut.Succeeded)
                return RedirectToAction("Index", "Home");

            TempData["Error"] = "Ошибка сервера. Попробуйте позже!";
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);

            if (user != null)
            {
                TempData["Error"] = "Этот Email уже используется.";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Patronymic = registerViewModel.Patronymic,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
            };

            var newUserResponce = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponce.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                var signinResult = await _signInManager.PasswordSignInAsync(newUser, registerViewModel.Password, false, false);

                if (signinResult.Succeeded)
                    return RedirectToAction("Index", "Home");
            }

            return View(registerViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> View()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(_mapper.Map<AppUser, RegisterViewModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegisterViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var appUser = await _userManager.GetUserAsync(User);

            var passwordCorrect = await _userManager.CheckPasswordAsync(appUser, user.Password);

            if (!passwordCorrect)
            {
                TempData["Error"] = "Старый пароль введен не верно.";
                return View(user);
            }

            appUser.FirstName = user.FirstName;
            appUser.LastName = user.LastName;
            appUser.Patronymic = user.Patronymic;

            if (appUser.Email != user.Email)
            {
                appUser.Email = user.Email;
                appUser.NormalizedEmail = _userManager.NormalizeEmail(appUser.Email);
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(appUser, user.Password, user.ConfirmPassword);

            if (!changePasswordResult.Succeeded)
            {
                TempData["Error"] = "Новый пароль слишком прост.";
                return View(user);
            }

            var result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                TempData["Error"] = "Не корректный Email.";
                return View(user);
            }

            return RedirectToAction("View");
        }
    }
}
