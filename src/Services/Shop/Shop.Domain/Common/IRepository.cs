using CSharpFunctionalExtensions;


namespace Shopping.Domain.Common
{
    public interface IRepository<T> where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
