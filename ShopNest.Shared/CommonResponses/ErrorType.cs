namespace ShopNest.Shared.CommonResponses
{
    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        unAuthorized = 3,
        Forbidden = 4,
        InvalidCredentials = 5
    }
}