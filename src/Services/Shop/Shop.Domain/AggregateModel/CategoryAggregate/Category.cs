using CSharpFunctionalExtensions;

namespace Shop.Domain.AggregateModel.CategoryAggregate
{
    public class Category : EntityBase
    {

        #region - Constructor - 
        public Category(CategoryName categoryName)
        {
            CategoryName = categoryName;
        }

        #endregion

        #region - Properties -
        public CategoryName CategoryName { get; private set; }

        #endregion

        #region - Functions -
        #endregion

    }
}
