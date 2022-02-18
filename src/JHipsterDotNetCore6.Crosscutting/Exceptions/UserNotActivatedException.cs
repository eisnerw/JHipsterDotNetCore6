using System.Security.Authentication;

namespace JHipsterDotNetCore6.Crosscutting.Exceptions
{
    public class UserNotActivatedException : AuthenticationException
    {
        public UserNotActivatedException(string message) : base(message)
        {
        }
    }
}
