using Business.Interop.Data;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Controllers
{
    public class SleepController : Controller
    {
        private readonly ISleepService _sleepService;
        private readonly IUserService _userService;

        public SleepController(ISleepService sleepService, IUserService userService)
        {
            _sleepService = sleepService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_sleepService.GetAll());
        }

        public ActionResult Create()
        {
            ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

            return View("Create");
        }

        public ActionResult Update(int id)
        {
            if (_sleepService.GetById(id) == null)
            {
                return NotFound();
            }
            ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

            return View("Update", _sleepService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SleepDto dto)
        {

            var sleepInTimeRange = _sleepService.FindByUserIdAndTimeRange(dto.UserId, dto.Start, dto.End);
            // Если есть такие сессии сна, и их количество больше единицы
            // (Если количество единица, то проверяем, не нашли ли мы ту же сессию сна)
            if (sleepInTimeRange.Any() && (sleepInTimeRange.Count() > 1 || sleepInTimeRange.First().Id != dto.Id))
            {
                ModelState.AddModelError("Start", "Недопустимое время начала или конца");
                ModelState.AddModelError("End", "Недопустимое время начала или конца");
            }

            if (!ModelState.IsValid)
            {
                ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

                return View(dto);
            }

            _sleepService.CreateSleep(dto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SleepDto dto)
        {
            
            var sleepInTimeRange = _sleepService.FindByUserIdAndTimeRange(dto.UserId, dto.Start, dto.End);
            // Если есть такие сессии сна, и их количество больше единицы
            // (Если количество единица, то проверяем, не нашли ли мы ту же сессию сна)
            if (sleepInTimeRange.Any() && (sleepInTimeRange.Count() > 1 || sleepInTimeRange.First().Id != dto.Id))
            {
                ModelState.AddModelError("Start", "Недопустимое время начала или конца");
                ModelState.AddModelError("End", "Недопустимое время начала или конца");
            }
            
            if (!ModelState.IsValid)
            {
                ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

                return View(dto);
            }

            _sleepService.UpdateSleep(dto);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (_sleepService.GetById(id) == null)
            {
                return NotFound();
            }
            _sleepService.DeleteById(id);

            return RedirectToAction("Index");
        }
    }
}
