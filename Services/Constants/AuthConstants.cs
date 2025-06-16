namespace Services.Constants
{
    public static class AuthConstants
    {
        public const int ROLE_ADMINISTRATOR = 1;
        public const int ROLE_MANAGER = 2;
        public const int ROLE_STAFF = 3;
        public const int ROLE_MEMBER = 4;

        public static readonly Dictionary<int, string> RoleNames = new()
        {
            { ROLE_ADMINISTRATOR, "Administrator" },
            { ROLE_MANAGER, "Manager" },
            { ROLE_STAFF, "Staff" },
            { ROLE_MEMBER, "Member" }
        };

        public static string GetRoleName(int roleId)
        {
            return RoleNames.TryGetValue(roleId, out var roleName) ? roleName : "Unknown";
        }
    }
}