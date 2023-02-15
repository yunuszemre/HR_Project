using HR.Project.BL.Abstract;
using HR.Project.Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HR.Project.WebUI.Areas.Login.Controllers
{
    [Area("Login")]
    public class LoginController : Controller
    {
        private readonly IGenericService<UserEntity> _userService;
        public LoginController(IGenericService<UserEntity> userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserEntity user)
        {
            // Kullanıcının DB'de olup olmadığını kontrol ediyoruz.
            if (_userService.Any(x => x.Email == user.Email && x.Password == user.Password))
            {
                // Eğer kullanıcı DB'de var ise kullanıcımızı yakalıyoruz.
                UserEntity loggedUser = _userService.GetByDefault(x => x.Email == user.Email && x.Password == user.Password);

                // Kullanıcımızın saklayacağımız bilgilerini Claim'ler olarak tutabiliriz.
                var claims = new List<Claim>()
                {
                    new Claim("Id", loggedUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, loggedUser.FirstName),
                    new Claim(ClaimTypes.Surname, loggedUser.LastName),
                    new Claim(ClaimTypes.Email, loggedUser.Email)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                // Yönetici Home/Index sayfasına yönlendireceğiz.
                return RedirectToAction("Index", "Home", new { area = " " });
            }

            // Eğer giriş yapamazsa kullanıcı bilgileri ile forma dönsün.
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = " " });   // Çıkış yapıldıktan sonra Blog Anasayfasına yönlendirmek için.
        }
    }
}
