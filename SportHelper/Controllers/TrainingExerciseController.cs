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
    public class TrainingExerciseController : Controller
    {
        private readonly ITrainingExerciseService _trainingExerciseService;
        private readonly ITrainingService _trainingService;
        private readonly IExerciseService _exerciseService;

        public TrainingExerciseController(ITrainingExerciseService trainingExerciseService, ITrainingService trainingService, IExerciseService exerciseService)
        {
            _trainingExerciseService = trainingExerciseService;
            _trainingService = trainingService;
            _exerciseService = exerciseService;
        }

        public IActionResult Index()
        {
            return View(_trainingExerciseService.GetAll());
        }

        public ActionResult Create()
        {
            ViewData["SessionIds"] = new SelectList(_trainingService.GetAll().Select(s => new { s.Id, Session = s.User.FullName + " – " + s.Start }), "Id", "Session");
            ViewData["ExerciseIds"] = new SelectList(_exerciseService.GetAll(), "Id", "Name");

            return View("Create");
        }

        public ActionResult Update(int id)
        {
            if (_trainingExerciseService.GetById(id) == null)
            {
                return NotFound();
            }
            ViewData["SessionIds"] = new SelectList(_trainingService.GetAll().Select(s => new { s.Id, Session = s.User.FullName  + " – " + s.Start }), "Id", "Session");
            ViewData["ExerciseIds"] = new SelectList(_exerciseService.GetAll(), "Id", "Name");

            return View("Update", _trainingExerciseService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainingExerciseDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["SessionIds"] = new SelectList(_trainingService.GetAll().Select(s => new { s.Id, Session = s.User.FullName + " – " + s.Start }), "Id", "Session");
                ViewData["ExerciseIds"] = new SelectList(_exerciseService.GetAll(), "Id", "Name");

                return View(dto);
            }

            _trainingExerciseService.CreateTrainingExercise(dto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TrainingExerciseDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewData["SessionIds"] = new SelectList(_trainingService.GetAll().Select(s => new { s.Id, Session = s.User.FullName + " – " + s.Start }), "Id", "Session");
                ViewData["ExerciseIds"] = new SelectList(_exerciseService.GetAll(), "Id", "Name");

                return View(dto);
            }

            _trainingExerciseService.UpdateTrainingExercise(dto);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (_trainingExerciseService.GetById(id) == null)
            {
                return NotFound();
            }
            _trainingExerciseService.DeleteById(id);

            return RedirectToAction("Index");
        }
    }
}
