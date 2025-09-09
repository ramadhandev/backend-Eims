namespace EIMS.Constants
{
    public class RoleConstants
    {
        public const string Worker = "Worker";
        public const string Supervisor = "Supervisor";
        public const string HSE = "HSE";
        public const string Manager = "Manager";

        public static readonly string[] AllRoles = { Worker, Supervisor, HSE, Manager };
        public static readonly string[] ManagementRoles = { Supervisor, HSE, Manager };
        public static readonly string[] ApprovalRoles = { Supervisor, HSE, Manager };

        public static bool IsValidRole(string role)
        {
            return AllRoles.Contains(role);
        }

        public static bool CanApprove(string role)
        {
            return ApprovalRoles.Contains(role);
        }

        public static bool IsManagement(string role)
        {
            return ManagementRoles.Contains(role);
        }
    }
}
