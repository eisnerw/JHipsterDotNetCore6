using JHipsterDotNetCore6.Infrastructure.Data;
using JHipsterDotNetCore6.Domain;
using JHipsterDotNetCore6.Test.Setup;

namespace JHipsterDotNetCore6.Test
{
    public static class Fixme
    {
        public static User ReloadUser<TEntryPoint>(AppWebApplicationFactory<TEntryPoint> factory, User user)
            where TEntryPoint : class
        {
            var applicationDatabaseContext = factory.GetRequiredService<ApplicationDatabaseContext>();
            applicationDatabaseContext.Entry(user).Reload();
            return user;
        }
    }
}
