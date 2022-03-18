using MartianRobots.Repositories.Interfaces;

namespace MartianRobots.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IExecutionRepository ExecutionRepository { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
