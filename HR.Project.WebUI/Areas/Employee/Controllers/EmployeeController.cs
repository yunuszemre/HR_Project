using HR.Project.BL.Abstract;
using HR.Project.BL.HelperMethods;
using HR.Project.Entities.Concrete;
using HR.Project.Entities.Enum;
using HR.Project.WebUI.Areas.Employee.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace HR.Project.WebUI.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class EmployeeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IGenericService<AdvancePayment> _paymentService;
        private readonly IGenericService<UserEntity> _userService;
        private readonly IGenericService<Permission> _permissionService;
        private readonly IGenericService<Spending> _spendingService;

        public EmployeeController(IGenericService<AdvancePayment> paymentService, IGenericService<UserEntity> userService, IGenericService<Permission> permissionService, IGenericService<Spending> spendingService, IWebHostEnvironment webHostEnvironment)
        {
            _paymentService = paymentService;
            _userService = userService;
            _permissionService = permissionService;
            _spendingService = spendingService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdvancePaymentRequest()
        {
            return View();
        }

        [HttpPost]

        public IActionResult AdvancePaymentRequest(AdvancePayment avans, int userId)
        {
            bool requestResult = false;

            avans.RequestDate = DateTime.Now;

            avans.ApprovalStatus = ApprovalStatus.Beklemede;
            avans.UserId = 4;

            var user = _userService.GetById(4);

            List<AdvancePayment> avanslarim = _paymentService.GetAll(x => x.UserId == 4).ToList();


            string message = Helper.AvansTalebi(avans, user, avanslarim, out requestResult);


            if (requestResult)
            {
                _paymentService.Add(avans);
            }
            else
            {
                TempData["fail_message"] = message;
                return RedirectToAction("AdvancePaymentRequest", "Employee", new { Area = "Employee" });
            }

            _paymentService.Save();
            TempData["success_message"] = message;
            return RedirectToAction("AdvancePaymentRequest", "Employee", new { Area = "Employee" });
        }

        [HttpGet]
        public IActionResult AdvancePaymentList()
        {
            var avanslar = _paymentService.GetAll();
            ViewBag.User = _userService.GetById(4);
            return View(avanslar);
        }


        [HttpGet]
        public IActionResult SpendingRequest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SpendingRequest(Spending spending, int userId)
        {
            spending.SpendingDate = DateTime.Now;
            spending.ApprovalStatus = ApprovalStatus.Beklemede;
            spending.UserId = userId;

            _spendingService.Add(spending);
            _spendingService.Save();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SpendingList()
        {
            var spendings = _spendingService.GetAll();
            ViewBag.User = _userService.GetById(4);
            return View(spendings);
        }


        [HttpGet]
        public IActionResult EmployeeDetail()
        {
            var user = _userService.GetById(4);     // Web sayfası üstünden kullanıcının Id'si alındıktan sonra buraya verilmesi gerek.
            return View(user);
        }

        [HttpGet]
        public IActionResult EmployeeUpdate()
        {
            var user = _userService.GetById(4);     // Web sayfası üstünden kullanıcının Id'si alındıktan sonra buraya verilmesi gerek.
            return View(user);
        }


        [HttpPost]
        public IActionResult EmployeeUpdate(UserEntity user, IFormFile file)
        {
            var updatedUser = _userService.GetById(user.Id);
            updatedUser.Address = user.Address;
            updatedUser.Phone = user.Phone;
            if (user.ImageURL == null)
            {
                updatedUser.ImageURL = ImageUpload(file);
            }

            _userService.Update(updatedUser);
            return View(updatedUser);
        }


        /// <summary>
        /// FOTOĞRAF YÜKLEME
        /// </summary>
        /// 
        public string ImageUpload(IFormFile file)
        {
            try
            {
                string[] extensions = { ".jpg", ".png", ".img", ".jpeg" };

                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string ext = Path.GetExtension(file.FileName);
                if (extensions.Contains(ext) == false)
                {
                    throw new Exception();
                }

                string fullPath = Path.Combine(filePath, $"{file.FileName}");
                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                file.CopyTo(fileStream);
                fileStream.Flush();
                string newFilePath = fullPath.Substring(fullPath.IndexOf("Uploads\\"));
                return newFilePath;

            }
            catch (Exception)
            {
                return "Lütfen resim dosyası yükleyiniz.";
            }
        }


        /// <summary>
        /// PDF YÜKLEME
        /// </summary>
        [HttpPost]
        public IActionResult SpendingDocumentUpload(IFormFile file)  // KONTROL EDİLMESİ GEREKİYOR
        {
            try
            {
                SpendingDocumentPreview.ShowPreview(file, Response, _webHostEnvironment);
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }



        [HttpGet]
        public IActionResult PermissionRequest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PermissonRequest(Permission permission)
        {
            var employee = _userService.GetById(permission.UserId);
            if (employee.Gender == Gender.Man)
            {
                if (permission.PermissionType == PermissionType.Doğumİzni)
                {
                    TempData["message"] = "Erkek çalışanlarımız doğum izninden faydalanamazlar";
                    return RedirectToAction("PermissonRequest", "Employee");
                }
                if (permission.PermissionType == PermissionType.Yıllıkİzin)
                {
                    if (employee.JobEnterDate.AddYears(1) < DateTime.Now)
                    {
                        TempData["message"] = "1 yıldan az süreli çalışanlar yıllık izin talebinde bulunamazlar";
                        return RedirectToAction("PermissonRequest", "Employee");
                    }
                }
                if (permission.PermissionStartDate < DateTime.Now.AddDays(1))
                {
                    TempData["message"] = "İzin talebi en erken yarın için oluşturulabilir";
                    return RedirectToAction("PermissonRequest", "Employee");
                }
                _permissionService.Add(permission);
            }
            else
            {
                if (permission.PermissionType == PermissionType.Yıllıkİzin)
                {
                    if (employee.JobEnterDate.AddYears(1) < DateTime.Now)
                    {
                        TempData["message"] = "1 yıldan az süreli çalışanlar yıllık izin talebinde bulunamazlar";
                        return RedirectToAction("PermissonRequest", "Employee");
                    }
                }
                if (permission.PermissionStartDate == DateTime.Now)
                {
                    TempData["message"] = "İzin talebi en erken yarın için oluşturulabilir";
                    return RedirectToAction("PermissonRequest", "Employee");
                }
                permission.PermissionDays = Convert.ToInt32((permission.PermissionEndDate - permission.PermissionStartDate).Value.TotalDays);
                permission.UserId = 4;
                _permissionService.Add(permission);
            }
            employee.YillikIzinSayisi -= permission.PermissionDays;
            _userService.Update(employee);
            return RedirectToAction();
        }
    }
}

