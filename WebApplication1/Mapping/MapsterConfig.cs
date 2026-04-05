using Microsoft.AspNetCore.Razor.Language;

namespace WebApplication1.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            CourseMapping.Register();
        }
    }
}
