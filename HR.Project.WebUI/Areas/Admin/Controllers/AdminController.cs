using HR.Project.BL.Abstract;
using HR.Project.Entities.Concrete;
using HR.Project.Entities.Enum;
using Microsoft.AspNetCore.Mvc;

namespace HR.Project.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IGenericService<AdvancePayment> _paymentService;
        private readonly IGenericService<UserEntity> _userService;
        private readonly IGenericService<Spending> _spendingService;

        public AdminController(IGenericService<AdvancePayment> paymentService, IGenericService<UserEntity> userService, IGenericService<Spending> spendingService)
        {
            _paymentService = paymentService;
            _userService = userService;
            _spendingService = spendingService;
        }
        public IActionResult Index()
        {
            var user = _userService.GetById(1);

            return View(user);

        }

        /// <summary>
        /// AVANS İŞLEMLERİ
        /// </summary>
        /// <returns></returns>

        public IActionResult AdvancePaymentList()
        {
            var avanslar = _paymentService.GetAll().Where(x => x.ApprovalStatus == ApprovalStatus.Beklemede);
            ViewBag.User = _userService.GetById(1);
            return View(avanslar);
        }

        public IActionResult AvansOnayla(int id)
        {
            var approvedAvans = _paymentService.GetById(id);

            approvedAvans.ApprovalStatus = ApprovalStatus.Onaylandı;
            approvedAvans.ReplyDate = DateTime.Now;

            _paymentService.Save();

            return RedirectToAction("AdvancePaymentList");
        }

        public IActionResult AvansReddet(int id)
        {
            var rejectedAvans = _paymentService.GetById(id);

            rejectedAvans.ApprovalStatus = ApprovalStatus.Reddedildi;
            rejectedAvans.ReplyDate = DateTime.Now;

            _paymentService.Save();

            return RedirectToAction("AdvancePaymentList");

        }

        public IActionResult AvansBeklet(int id)
        {
            var pendingAvans = _paymentService.GetById(id);

            pendingAvans.ApprovalStatus = ApprovalStatus.Beklemede;
            pendingAvans.ReplyDate = DateTime.Now;

            _paymentService.Save();

            return RedirectToAction("AdvancePaymentList");
        }


        /// <summary>
        /// HARCAMA İŞLEMLERİ
        /// </summary>
        /// <returns></returns>

        public IActionResult SpendingList()
        {
            var spendings = _spendingService.GetAll().Where(x => x.ApprovalStatus == ApprovalStatus.Beklemede);
            ViewBag.User = _userService.GetById(1);
            return View(spendings);
        }

        public IActionResult SpendingApproved(int id)
        {
            var approvedSpending = _spendingService.GetById(id);

            approvedSpending.ApprovalStatus = ApprovalStatus.Onaylandı;
            approvedSpending.ReplyDate = DateTime.Now;

            _spendingService.Save();

            return RedirectToAction("SpendingList");
        }

        public IActionResult SpendingRejected(int id)
        {
            var rejectedSpending = _spendingService.GetById(id);

            rejectedSpending.ApprovalStatus = ApprovalStatus.Reddedildi;
            rejectedSpending.ReplyDate = DateTime.Now;

            _spendingService.Save();

            return RedirectToAction("SpendingList");

        }

        public IActionResult SpendingPending(int id)
        {
            var pendingSpending = _spendingService.GetById(id);

            pendingSpending.ApprovalStatus = ApprovalStatus.Beklemede;
            pendingSpending.ReplyDate = DateTime.Now;

            _spendingService.Save();

            return RedirectToAction("SpendingList");
        }
    }
}
