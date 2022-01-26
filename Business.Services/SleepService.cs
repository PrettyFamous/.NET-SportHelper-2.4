using AutoMapper;
using Business.Entities;
using Business.Interop.Data;
using Business.Repositories.DataRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SleepService : ISleepService
    {
        private readonly ISleepRepository _sleepRepository;
        private readonly IMapper _mapper;

        public SleepService(ISleepRepository repository, IMapper mapper)
        {
            _sleepRepository = repository;
            _mapper = mapper;
        }

        public SleepDto CreateSleep(SleepDto sleep)
        {
            var entity = _mapper.Map<Sleep>(sleep);

            _sleepRepository.CreateOrUpdate(entity);

            return _mapper.Map<SleepDto>(entity);
        }

        public SleepDto UpdateSleep(SleepDto sleep)
        {
            var entity = _mapper.Map<Sleep>(sleep);

            _sleepRepository.CreateOrUpdate(entity);

            return _mapper.Map<SleepDto>(entity);
        }

        public IEnumerable<SleepDto> GetAll()
        {
            return _mapper.Map<List<Sleep>, IEnumerable<SleepDto>>(_sleepRepository.Query());
        }

        public void DeleteById(int id)
        {
            _sleepRepository.Delete(_sleepRepository.Read(id));
        }

        public SleepDto GetById(int id)
        {
            return _mapper.Map<Sleep, SleepDto>(_sleepRepository.Read(id));
        }

        public IEnumerable<SleepDto> FindByUserIdAndTimeRange(int userId, DateTime start, DateTime end)
        {
            return _mapper.Map<IEnumerable<Sleep>, IEnumerable<SleepDto>>(_sleepRepository.Query().Where(s => 
                    s.UserId == userId && // Совпадает пользователь
                                          // А также ищем пересечения:
                                          // Начало найденной сессии сна лежит в заданном временном интервале
                                          // ИЛИ конец найденной сессии сна лежит в заданном временном интервале
                                          // ИЛИ заданный временной интервал находится внутри найденной сессии сна
                    (s.Start >= start && s.Start <= end || s.End <= end && s.End >= start || s.Start <= start && s.End >= end)));
        }

        public MemoryStream GetXlsxWithSleepSessions(int userId, DateTime start, DateTime end)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add();

            worksheet.Cell(1, 1).Value = "Начало";
            worksheet.Cell(1, 2).Value = "Конец";

            foreach (var (sleep, i) in _sleepRepository.Query().Where(t => t.UserId == userId).Select((t, i) => (t, i)))
            {
                if (sleep.Start >= start && sleep.End <= end)
                {
                    worksheet.Cell(i + 2, 1).Value = sleep.Start;
                    worksheet.Cell(i + 2, 2).Value = sleep.End;
                }
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return stream;
        }
    }
}
