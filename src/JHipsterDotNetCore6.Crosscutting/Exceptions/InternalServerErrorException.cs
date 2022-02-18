using JHipsterDotNetCore6.Crosscutting.Constants;

namespace JHipsterDotNetCore6.Crosscutting.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException(string message) : base(ErrorConstants.DefaultType, message)
        {
        }
    }
}
