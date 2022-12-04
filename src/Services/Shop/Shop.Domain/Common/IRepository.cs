using CSharpFunctionalExtensions;


namespace Shop.Domain.Common
{
    public interface IRepository<T> where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
