using Domain.Enums;

namespace ITransitionProject
{
    public class SupportedUserRolesForSalesforce
    {
        public IEnumerable<UserRoles> Roles() => [UserRoles.Candidate, UserRoles.Recruter];
    }
}
