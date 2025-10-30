namespace DiscountManagement.Configuration.Permissions
{
    public static class DiscountPermissions
    {
        //CustomerDiscount
        public const int ListCustomerDiscounts = 90;
        public const int CreateCustomerDiscount = 91;
        public const int EditCustomerDiscount = 92;


        //ColleagueDiscount
        public const int ListColleagueDiscounts = 100;
        public const int CreateColleagueDiscount = 101;
        public const int EditColleagueDiscount = 102;
        public const int RemoveColleagueDiscount = 103;
        public const int RestoreColleagueDiscount = 104;
    }
}