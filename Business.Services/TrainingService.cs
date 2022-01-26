using AutoMapper;
using Business.Entities;
using Business.Interop.Data;
using Business.Repositories.DataRepositories;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public TrainingService(ITrainingRepository repository, IMapper mapper)
        {
            _trainingRepository = repository;
            _mapper = mapper;
        }

        public TrainingDto CreateTraining(TrainingDto training)
        {
            var entity = _mapper.Map<Training>(training);

            _trainingRepository.CreateOrUpdate(entity);

            return _mapper.Map<TrainingDto>(entity);
        }

        public TrainingDto UpdateTraining(TrainingDto training)
        {
            var entity = _mapper.Map<Training>(training);

            _trainingRepository.CreateOrUpdate(entity);

            return _mapper.Map<TrainingDto>(entity);
        }

        public IEnumerable<TrainingDto> GetAll()
        {
            return _mapper.Map<List<Training>, IEnumerable<TrainingDto>>(_trainingRepository.Query());
        }

        public void DeleteById(int id)
        {
            _trainingRepository.Delete(_trainingRepository.Read(id));
        }

        public TrainingDto GetById(int id)
        {
            return _mapper.Map<Training, TrainingDto>(_trainingRepository.Read(id));
        }

        public IEnumerable<TrainingDto> FindByUserIdAndTimeRange(int userId, DateTime start, DateTime end)
        {
            return _mapper.Map<IEnumerable<Training>, IEnumerable<TrainingDto>>(_trainingRepository.Query().Where(t => 
                    t.UserId == userId && // Совпадает пользователь
                                          // А также ищем пересечения:
                                          // Начало найденной тренировки лежит в заданном временном интервале
                                          // ИЛИ конец найденной тренировки лежит в заданном временном интервале
                                          // ИЛИ заданный временной интервал находится внутри найденной тренировки
                    (t.Start >= start && t.Start <= end || t.End <= end && t.End >= start || t.Start <= start && t.End >= end)));
        }

        public MemoryStream GetXlsxWithTrainings(int userId, DateTime start, DateTime end)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add();

            worksheet.Cell(1, 1).Value = "Начало";
            worksheet.Cell(1, 2).Value = "Конец";

            foreach (var (training, i) in _trainingRepository.Query().Where(t => t.UserId == userId).Select((t, i) => (t, i)))
            {
                if (training.Start >= start && training.End <= end)
                {
                    worksheet.Cell(i + 2, 1).Value = training.Start;
                    worksheet.Cell(i + 2, 2).Value = training.End;
                }
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return stream;
        }
    }
}
