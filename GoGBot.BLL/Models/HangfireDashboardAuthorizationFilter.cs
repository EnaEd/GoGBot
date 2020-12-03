using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace GoGBot.BLL.Models
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context) => true;

    }
}
