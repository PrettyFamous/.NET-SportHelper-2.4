using Business.Interop.Data;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using SportHelper.Models;
using System;
using System.Linq;

namespace Lab1.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITrainingService _trainingService;
        private readonly ISleepService _sleepService;

        public UserController(IUserService userService, ITrainingService trainingService, ISleepService sleepService)
        {
            _userService = userService;
            _trainingService = trainingService;
            _sleepService = sleepService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetAll());
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        public ActionResult Update(int id)
        {
            if (_userService.GetById(id) == null)
            {
                return NotFound();
            }
            return View("Update", _userService.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDto dto)
        {
            var userWithEmail = _userService.GetByEmail(dto.Email);
            if (!(userWithEmail is null) && userWithEmail.Id != dto.Id)
            {
                ModelState.AddModelError("Email", "Пользователь с таким адресом электронной почты уже существует");
            }
            if (dto.Birthdate > DateTime.Now)
            {
                ModelState.AddModelError("Birthdate", "Дата рождения не может быть больше текущей календарной даты");
            }
            if (dto.FullName != null)
            {
                string[] strs = dto.FullName.Split(' ').Where(e => e.Length != 0).ToArray();
                dto.FullName = string.Join(' ', strs);

                if (string.IsNullOrWhiteSpace(dto.FullName))
                {
                    ModelState.AddModelError("FullName", "ФИО не может состоять только из пробелов");
                }
            }
            if (!ModelState.IsValid)
            {
                return View(dto);
            }


            _userService.CreateUser(dto);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserDto dto)
        {
            var userWithEmail = _userService.GetByEmail(dto.Email);
            if (!(userWithEmail is null) && userWithEmail.Id != dto.Id)
            {
                ModelState.AddModelError("Email", "Пользователь с таким адресом электронной почты уже существует");
            }
            if (dto.Birthdate > DateTime.Now)
            {
                ModelState.AddModelError("Birthdate", "Дата рождения не может быть больше текущей календарной даты");
            }
            if (dto.FullName != null)
            {
                string[] strs = dto.FullName.Split(' ').Where(e => e.Length != 0).ToArray();
                dto.FullName = string.Join(' ', strs);

                if (string.IsNullOrWhiteSpace(dto.FullName))
                {
                    ModelState.AddModelError("FullName", "ФИО не может состоять только из пробелов");
                }
            }
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            

            _userService.UpdateUser(dto);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (_userService.GetById(id) == null)
            {
                return NotFound();
            }
            _userService.DeleteById(id);

            return RedirectToAction("Index");
        }

        public ActionResult GetStatistics(int id, string fullName, string email)
        {
            var user = _userService.GetById(id);
            if (user == null || (user.FullName != fullName || user.Email != email))
            {
                return NotFound();
            }
            return View("Statistics",
                new UserTimeRangeModel {
                    UserId = id,
                    UserName = fullName,
                    Email = email,
                    Start = DateTime.MinValue,
                    End = DateTime.MaxValue,
                });
        }

        [HttpPost]
        public FileResult GetXlsxWithTrainings(UserTimeRangeModel model)
        {
            return File(_trainingService.GetXlsxWithTrainings(model.UserId, model.Start, model.End).ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"{model.UserName} {model.Email}. Календарь тренировок.xlsx"); ;
        }

        
        [HttpPost]
        public FileResult GetXlsxWithSleepSessions(UserTimeRangeModel model)
        {
            return File(_sleepService.GetXlsxWithSleepSessions(model.UserId, model.Start, model.End).ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"{model.UserName} {model.Email}. Распорядок сна.xlsx"); ;
        }
    }
}