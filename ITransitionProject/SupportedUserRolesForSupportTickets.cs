using Domain.Enums;

namespace ITransitionProject
{
    public class SupportedUserRolesForSupportTickets
    {
        public IEnumerable<UserRoles> Roles() => [UserRoles.Candidate, UserRoles.Recruter];
    }
}
