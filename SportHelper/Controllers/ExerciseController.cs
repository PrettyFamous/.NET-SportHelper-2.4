using Business.Interop.Data;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public IActionResult Index()
        {
            return View(_exerciseService.GetAll());
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        public ActionResult Update(int id)
        {
            if (_exerciseService.GetById(id) == null)
            {
                return NotFound();
            }
            return View("Update", _exerciseService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExerciseDto dto)
        {
            var exerciseWithName = _exerciseService.GetByName(dto.Name);
            if (!(exerciseWithName is null) && exerciseWithName.Id != dto.Id)
            {
                ModelState.AddModelError("Name", "Упражнение с таким названием уже существует");
            }
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            if (dto.Name != null)
            {
                string[] strs = dto.Name.Split(' ').Where(e => e.Length != 0).ToArray();
                dto.Name = string.Join(' ', strs);

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    ModelState.AddModelError("Name", "Название упражнения не может состоять только из пробелов");
                }
            }


            _exerciseService.UpdateExercise(dto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ExerciseDto dto)
        {
            var exerciseWithName = _exerciseService.GetByName(dto.Name);
            if (!(exerciseWithName is null) && exerciseWithName.Id != dto.Id)
            {
                ModelState.AddModelError("Name", "Упражнение с таким названием уже существует");
            }
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            if (dto.Name != null)
            {
                string[] strs = dto.Name.Split(' ').Where(e => e.Length != 0).ToArray();
                dto.Name = string.Join(' ', strs);

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    ModelState.AddModelError("Name", "Название упражнения не может состоять только из пробелов");
                }
            }

            _exerciseService.UpdateExercise(dto);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (_exerciseService.GetById(id) == null)
            {
                return NotFound();
            }
            _exerciseService.DeleteById(id);

            return RedirectToAction("Index");
        }
    }
}
