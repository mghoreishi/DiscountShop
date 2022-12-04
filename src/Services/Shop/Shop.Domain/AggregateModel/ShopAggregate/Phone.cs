using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;


namespace Shopping.Domain.AggregateModel.ShopAggregate
{
    public class Phone : ValueObject
    {
        public string Value { get; }

        public Phone(string value)
        {
            Value = value;
        }

        public static Result<Phone, Error> Create(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Errors.General.ValueIsRequired(nameof(Phone));
            }

            input = input.Trim();

            if (input.Length > 12)
            {
                return Errors.General.InvalidLength(nameof(Phone));
            }

            if (Regex.IsMatch(input, @"^[^<>]*$") == false || input.All(char.IsDigit) == false)
            {
                return Errors.General.InvalidCharacters(nameof(Phone));
            }

            return new Phone(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}




