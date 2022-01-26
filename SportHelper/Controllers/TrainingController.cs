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
    public class TrainingController : Controller
    {
        private readonly ITrainingService _trainingService;
        private readonly IUserService _userService;

        public TrainingController(ITrainingService trainingService, IUserService userService)
        {
            _trainingService = trainingService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_trainingService.GetAll());
        }

        public ActionResult Create()
        {
            ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

            return View("Create");
        }

        public ActionResult Update(int id)
        {
            if (_trainingService.GetById(id) == null)
            {
                return NotFound();
            }
            ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

            return View("Update", _trainingService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainingDto dto)
        {
            var trainingsInTimeRange = _trainingService.FindByUserIdAndTimeRange(dto.UserId, dto.Start, dto.End);
            // Если есть такие тренировки, и их количество больше единицы
            // (Если количество единица, то проверяем, не нашли ли мы ту же тренировку)
            if (trainingsInTimeRange.Any() && (trainingsInTimeRange.Count() > 1 || trainingsInTimeRange.First().Id != dto.Id))
            {
                ModelState.AddModelError("Start", "Недопустимое время начала или конца");
                ModelState.AddModelError("End", "Недопустимое время начала или конца");
            }
            if (!ModelState.IsValid)
            {
                ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

                return View(dto);
            }

            _trainingService.CreateTraining(dto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TrainingDto dto)
        {
            var trainingsInTimeRange = _trainingService.FindByUserIdAndTimeRange(dto.UserId, dto.Start, dto.End);
            // Если есть такие тренировки, и их количество больше единицы
            // (Если количество единица, то проверяем, не нашли ли мы ту же тренировку)
            if (trainingsInTimeRange.Any() && (trainingsInTimeRange.Count() > 1 || trainingsInTimeRange.First().Id != dto.Id))
            {
                ModelState.AddModelError("Start", "Недопустимое время начала или конца");
                ModelState.AddModelError("End", "Недопустимое время начала или конца");
            }
            if (!ModelState.IsValid)
            {
                ViewData["UserIds"] = new SelectList(_userService.GetAll().Select(u => new { u.Id, Session = u.FullName + " – " + u.Email }), "Id", "Session");

                return View(dto);
            }

            _trainingService.UpdateTraining(dto);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (_trainingService.GetById(id) == null)
            {
                return NotFound();
            }
            _trainingService.DeleteById(id);

            return RedirectToAction("Index");
        }
    }
}
