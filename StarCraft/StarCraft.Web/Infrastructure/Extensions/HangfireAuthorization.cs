namespace StarCraft.Web.Infrastructure.Extensions
{
    using Hangfire.Dashboard;

    public class HangfireAuthorization : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext.User.IsInRole(WebConstats.AdministratorRole);
        }
    }
}