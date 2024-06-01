using System.Security.Claims;

namespace BackendService.Application.Common.Identity;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor context;

    public IdentityService(IServiceScopeFactory serviceScopeFactory)
    {
        var scope = serviceScopeFactory.CreateScope();
        context = scope.ServiceProvider.GetService<IHttpContextAccessor>()!;
    }

    public string GetUserId()
    {
        var identity = context.HttpContext!.User.Identity as ClaimsIdentity;
        if (identity is not null)
        {
            IEnumerable<Claim> claims = identity.Claims;

            var usernameClaim = claims.FirstOrDefault(claim => claim.Type == "id");

            if (usernameClaim != null)
            {
                return usernameClaim.Value;
            }
        }

        return null!;
    }
}
