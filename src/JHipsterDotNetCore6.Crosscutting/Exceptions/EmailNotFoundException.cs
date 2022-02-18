using JHipsterDotNetCore6.Crosscutting.Constants;

namespace JHipsterDotNetCore6.Crosscutting.Exceptions
{
    public class EmailNotFoundException : BaseException
    {
        public EmailNotFoundException() : base(ErrorConstants.EmailNotFoundType, "Email address not registered")
        {
        }
    }
}
