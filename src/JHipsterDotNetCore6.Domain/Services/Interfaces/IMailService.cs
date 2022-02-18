using System.Threading.Tasks;
using JHipsterDotNetCore6.Domain;

namespace JHipsterDotNetCore6.Domain.Services.Interfaces
{
    public interface IMailService
    {
        Task SendPasswordResetMail(User user);
        Task SendActivationEmail(User user);
        Task SendCreationEmail(User user);
    }
}
