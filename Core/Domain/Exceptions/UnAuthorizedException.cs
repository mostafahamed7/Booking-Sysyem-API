namespace Domain.Exceptions
{
    public class UnAuthorizedException(string Message = "Invalid Email or Password") : Exception
    {

    }
}
