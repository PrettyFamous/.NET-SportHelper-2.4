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
    public class NutritionController : Controller
    {
        private readonly INutritionService _nutritionService;
        private readonly IUserService _userService;

        public NutritionController(INutritionService nutritionService, IUserService userService)
        {
            _nutritionService = nutritionService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_nutritionService.GetAll());
        }

        public ActionResult Create()
        {
            ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email}), "Id", "Session");

            return View("Create");
        }

        public ActionResult Update(int id)
        {
            if (_nutritionService.GetById(id) == null)
            {
                return NotFound();
            }
            ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

            return View("Update", _nutritionService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NutritionDto dto)
        {
            var nutritionWithTime = _nutritionService.GetByUserIdAndTime(dto.UserId, dto.Time);
            if (!(nutritionWithTime is null) && nutritionWithTime.Id != dto.Id)
            {
                ModelState.AddModelError("Time", "Приём пищи в такое время у такого пользователя уже существует");
            }
            if (!ModelState.IsValid)
            {
                ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

                return View(dto);
            }

            _nutritionService.CreateNutrition(dto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(NutritionDto dto)
        {
            var nutritionWithTime = _nutritionService.GetByUserIdAndTime(dto.UserId, dto.Time);
            if (!(nutritionWithTime is null) && nutritionWithTime.Id != dto.Id)
            {
                ModelState.AddModelError("Time", "Приём пищи в такое время у такого пользователя уже существует");
            }
            if (!ModelState.IsValid)
            {
                ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

                return View(dto);
            }

            _nutritionService.UpdateNutrition(dto);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (_nutritionService.GetById(id) == null)
            {
                return NotFound();
            }
            _nutritionService.DeleteById(id);

            return RedirectToAction("Index");
        }

    }
}
