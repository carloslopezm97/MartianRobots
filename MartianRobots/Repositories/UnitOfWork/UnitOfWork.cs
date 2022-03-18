using MartianRobots.Entities;
using MartianRobots.Repositories.Interfaces;

namespace MartianRobots.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public IExecutionRepository ExecutionRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.logger = loggerFactory.CreateLogger("logs");

            this.ExecutionRepository = new ExecutionRepository(context, logger);
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
