using Business.Interop.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ISleepService
    {
        public SleepDto CreateSleep(SleepDto sleep);
        public SleepDto UpdateSleep(SleepDto sleep);
        public IEnumerable<SleepDto> GetAll();
        public void DeleteById(int id);
        public SleepDto GetById(int id);
        public IEnumerable<SleepDto> FindByUserIdAndTimeRange(int userId, DateTime start, DateTime end);
        public MemoryStream GetXlsxWithSleepSessions(int userId, DateTime start, DateTime end);
    }
}
