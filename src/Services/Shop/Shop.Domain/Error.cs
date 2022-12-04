using CSharpFunctionalExtensions;


namespace Shopping.Domain
{
    public sealed class Error : ValueObject
    {
        private const string Separator = "||";

        public string Code { get; }
        public string Message { get; }

        internal Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }

        public string Serialize()
        {
            return $"{Code}{Separator}{Message}";
        }

        public static Error Deserialize(string serialized)
        {
            if (serialized == "A non-empty request body is required.")
            {
                return Errors.General.ValueIsRequired();
            }

            string[] data = serialized.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length < 2)
            {
                throw new Exception($"Invalid error serialization: '{serialized}'");
            }

            return new Error(data[0], data[1]);
        }
    }

    public static class Errors
    {
        public static class Comment
        {

        }

        public static class General
        {
            public static Error NotFound(long? id = null)
            {
                string forId = id == null ? "" : $" for Id '{id}'";
                return new Error("record.not.found", $"Record not found{forId}");
            }

            public static Error ValueIsInvalid(string name = null)
            {
                string msg = name == null ? "Value is invalid" : $"{name} is invalid";
                return new Error("value.is.invalid", msg);
            }

            public static Error ValueIsRequired(string name = null)
            {
                string msg = name == null ? "Value is required" : $"{name} is required";
                return new Error("value.is.required", msg);
            }

            public static Error InvalidCharacters(string name = null)
            {
                string msg = name == null ? "Invalid Characters" : $"{name} has invalid characters";
                return new Error("invalid.string.characters", msg);
            }

            public static Error InvalidLength(string name = null)
            {
                string label = name == null ? " " : " " + name + " ";
                return new Error("invalid.string.length", $"Invalid{label}length");
            }

            public static Error ValueIsLessThanOne(string name = null)
            {
                string label = name == null ? " " : " " + name + " ";
                return new Error("value.is.less.than.one", $"value{label}should be greater than or equal to one");
            }

            public static Error CollectionIsTooSmall(int min, int current)
            {
                return new Error(
                    "collection.is.too.small",
                    $"The collection must contain {min} items or more. It contains {current} items.");
            }

            public static Error CollectionIsTooLarge(int max, int current)
            {
                return new Error(
                    "collection.is.too.large",
                    $"The collection must contain {max} items or more. It contains {current} items.");
            }

            public static Error InternalServerError(string message)
            {
                return new Error("internal.server.error", message);
            }
        }
    }
}
