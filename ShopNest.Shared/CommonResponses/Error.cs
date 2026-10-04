namespace ShopNest.Shared.CommonResponses
{
    public class Error
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public ErrorType Status { get; set; }
        private Error(string description, string code, ErrorType status)
        {
            Description = description;
            Code = code;
            Status = status;
        }
        public static Error Failure(string Code = "General Failure", string Description = "General Failure Occured ")
        {
            return new Error(Description, Code, ErrorType.Failure);
        }
        public static Error Validation(string Code = "General Validation", string Description = "General Validation Occured ")
        {
            return new Error(Description, Code, ErrorType.Validation);
        }
        public static Error Forbidden(string Code = "General Forbidden", string Description = "General Forbidden Occured ")
        {
            return new Error(Description, Code, ErrorType.Forbidden);
        }
        public static Error UnAuthorizad(string Code = "General UnAuthorizad", string Description = "General UnAuthorizad Occured ")
        {
            return new Error(Description, Code, ErrorType.unAuthorized);
        }
        public static Error InvalidCredentials(string Code = "General InvalidCredentials", string Description = "General InvalidCredentials  Occured ")
        {
            return new Error(Description, Code, ErrorType.InvalidCredentials);
        }
        public static Error NotFound(string Code = "General NotFound", string Description = "General NotFound  Occured ")
        {
            return new Error(Description, Code, ErrorType.NotFound);
        }


    }
}
