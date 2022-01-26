using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public string Email { get; set; }
        public IEnumerable<Nutrition> Meals { get; set; }
        public IEnumerable<Sleep> SleepSessions { get; set; }
        public IEnumerable<Training> Trainings { get; set; }
    }
}
