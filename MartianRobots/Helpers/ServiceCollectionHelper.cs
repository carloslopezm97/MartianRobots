using MartianRobots.Repositories.UnitOfWork;

namespace MartianRobots.Helpers
{
    public static class ServiceCollectionHelper
    {
        public static void AddServiceDependencies (this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
