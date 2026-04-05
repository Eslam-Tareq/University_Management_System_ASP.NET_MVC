using Mapster;
using WebApplication1.Models;
using WebApplication1.ViewModels.Course;

namespace WebApplication1.Mapping
{
    public static class CourseMapping
    {
        public static void Register()
        {
            TypeAdapterConfig<CourseFormViewModel, Course>.NewConfig()
                .Map(dest => dest.Topics, src =>
                    !string.IsNullOrWhiteSpace(src.Topics)
                        ? src.Topics.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(s => s.Trim())
                                    .ToList()
                        : new List<string>());

                    TypeAdapterConfig<Course, CourseFormViewModel>.NewConfig()
             .Map(dest => dest.Topics, src =>
                 src.Topics != null && src.Topics.Any()
                     ? string.Join(", ", src.Topics)
                     : string.Empty);
        }
    }
}
