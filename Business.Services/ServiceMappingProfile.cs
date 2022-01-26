using AutoMapper;
using Business.Entities;
using Business.Interop.Data;

namespace Business.Services
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Nutrition, NutritionDto>();
            CreateMap<Sleep, SleepDto>();
            CreateMap<Training, TrainingDto>();
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<TrainingExercise, TrainingExerciseDto>();

            CreateMap<UserDto, User>();
            CreateMap<NutritionDto, Nutrition>();
            CreateMap<SleepDto, Sleep>();
            CreateMap<TrainingDto, Training>();
            CreateMap<ExerciseDto, Exercise>();
            CreateMap<TrainingExerciseDto, TrainingExercise>();
        }
    }
}
