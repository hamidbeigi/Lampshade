namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string SeniorAdmin = "2";
        public const string ContentUploader = "3";
        public const string ColleagueUser = "4";
        public const string SystemUser = "5";
        
        public static string GetRoleBy(long id)
        {
            switch (id)
            {
                case 1:
                    return "مدیرسیستم";
                case 2:
                    return "مدیر ارشد";
                case 3:
                    return "محتوا گذار";
                default:
                    return "";
            }
        }
    }
}
