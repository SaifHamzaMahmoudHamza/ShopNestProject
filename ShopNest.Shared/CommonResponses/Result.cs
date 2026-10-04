namespace ShopNest.Shared.CommonResponses
{
    public class Result
    {
        //COMPOSITE Design Pattern 
        //IsSuccess
        private readonly List<Error> Errors = [];
        public bool IsSuccess => Errors.Count == 0;
        //IsFailure
        //public bool IsFailure => Errors.Count > 0;
        public bool IsFailure => !IsSuccess;
        //Errors[Code-Desc-Status]
        public IReadOnlyList<Error> errors => Errors;
        //3 main ctors
        //1- Success 
        protected Result()
        {

        }
        //2-Single Error 
        protected Result(Error error)
        {
            Errors.Add(error);
        }
        //3-Multiple Errors  mainly validation Errors
        protected Result(List<Error> _Errors)
        {
            Errors.AddRange(_Errors);
        }
        //FACTORY METHODS
        public static Result Ok()
        {
            return new();
        }
        public static Result Fail(Error Error)
        {
            return new(Error);
        }
        public static Result Fail(List<Error> errors)
        {
            return new(errors);
        }
    }
    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("You cannot access the value in case of Failure Scenario");
        private Result(TValue value)
        {
            _value = value;
        }
        private Result(Error error) : base(error)
        {
            _value = default!;

        }
        private Result(List<Error> errors) : base(errors)
        {
            _value = default!;
        }
        public static Result<TValue> Ok(TValue value)
        {
            return new Result<TValue>(value);
        }
        public static new Result<TValue> Fail(Error Error)
        {
            return new Result<TValue>(Error);
        }
        public static new Result<TValue> Fail(List<Error> Errors)
        {
            return new Result<TValue>(Errors);
        }
        //Converts Tvalue into Result<TValue> Automatically
        public static implicit operator Result<TValue>(TValue value) => Ok(value);
        public static implicit operator Result<TValue>(Error error) => Fail(error);
        public static implicit operator Result<TValue>(List<Error> errors) => Fail(errors);
    }
}
