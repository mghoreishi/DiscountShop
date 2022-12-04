using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Shop.Domain.AggregateModel.CategoryAggregate
{
    public class CategoryName : ValueObject
    {
        public string Value { get; }

        public CategoryName(string value)
        {
            Value = value;
        }

        public static Result<CategoryName, Error> Create(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Errors.General.ValueIsRequired(nameof(CategoryName));
            }

            input = input.Trim();

            if (input.Length > 50)
            {
                return Errors.General.InvalidLength(nameof(CategoryName));
            }

            if (Regex.IsMatch(input, @"^[^<>]*$") == false)
            {
                return Errors.General.InvalidCharacters(nameof(CategoryName));
            }

            return new CategoryName(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}



