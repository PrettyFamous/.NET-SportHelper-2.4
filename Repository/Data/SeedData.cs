using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public static class SeedData
    {
        public static void Initialize(Context context)
        {
            if (!context.TrainingExercises.Any())
            {
                context.TrainingExercises.AddRange(trainingExercises);
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!context.SleepSessions.Any())
            {
                List<Sleep> sleepSessions = new List<Sleep>()
                {
                    new Sleep { Start = new DateTime(2021,03,12), End = new DateTime(2021,03,13), User = users[0], UserId = users[0].Id},
                    new Sleep { Start = new DateTime(2021,03,12), End = new DateTime(2021,03,13), User = users[1], UserId = users[1].Id},
                };

                context.SleepSessions.AddRange(sleepSessions);
                context.SaveChanges();
            }

            if (!context.TrainingSessions.Any())
            {
                List<Training> trainingSessions = new List<Training>()
                {
                    new Training { Start = new DateTime(2021,03,13), End = new DateTime(2021,03,14), User = users[0], UserId = users[0].Id},
                    new Training { Start = new DateTime(2021,03,14), End = new DateTime(2021,03,15), User = users[1], UserId = users[1].Id},
                };

                context.TrainingSessions.AddRange(trainingSessions);
                context.SaveChanges();
            }

            if (!context.Meals.Any())
            {
                List<Nutrition> meals = new List<Nutrition>()
                {
                    new Nutrition { Time = new DateTime(2021,03,15), Proteins = 25, Calories = 20, Carbohydrates = 66, Fats = 77, User = users[0], UserId = users[0].Id },
                    new Nutrition { Time = new DateTime(2021,03,18), Proteins = 22, Calories = 29, Carbohydrates = 76, Fats = 70, User = users[1], UserId = users[1].Id },
                };
                context.Meals.AddRange(meals);
                context.SaveChanges();
            }
        }



        private static readonly List<Exercise> trainingExercises = new List<Exercise>()
        {
            new Exercise { Name = "Отжимания" },
            new Exercise { Name = "Бег"},
            new Exercise { Name = "Подтягивания"},
        };

        private static readonly List<User> users = new List<User>()
        {
            new User { FullName = "Иванов Иван Иванович", Birthdate = new DateTime(1988, 3, 4), Weight = 55, Height = 180, Email = "ak.abaz@mail.ru"},
            new User { FullName = "Петров Иван Иванович", Birthdate = new DateTime(1992, 3, 7), Weight = 75, Height = 160, Email = "akkk.abaz@mail.ru"},
        };
    }
}