using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Constant
{
    public static class RoleDataConstant
    {
        public const string ADMIN = "Admin";
        public const string MANAGER = "Manager";
        public const string STAFF = "Staff";

        public const int ADMIN_ID = 1;
        public const int MANAGER_ID = 2;
        public const int STAFF_ID = 3;
    }

    public static class PremisesTypeDataConstant
    {
        public const string FARM = "Farm";
        public const string PROVIDER = "Provider";
        public const string DISTRIBUTOR = "Distributor";

        public const int FARM_ID = 1;
        public const int PROVIDER_ID = 2;
        public const int DISTRIBUTOR_ID = 3;
    }

    public static class CategoryDataConstant
    {
        public const string PORK = "Thịt Heo";
        public const string CHICKEN = "Thịt Gà";
        public const string BEEF = "Thịt Bò";

        public const int PORK_ID = 1;
        public const int CHICKEN_ID = 2;
        public const int BEEF_ID = 3;
    }

    public static class TransactionStatusDataConstant
    {
        public const string CREATED = "Đã Tạo";
        public const string CERTIFICATION = "Đã Kiểm Định";
        public const string CONFIRMED = "Đã Xác Nhận";
        public const string REJECTED = "Đã Từ Chối";

        public const int CREATED_ID = 1;
        public const int CERTIFICATION_ID = 2;
        public const int CONFIRMED_ID = 3;
        public const int REJECTED_ID = 4;
    }

    public static class FoodDetailTypeDataConstant
    {
        public const string CREATE_NEW = "Tạo mới";
        public const string ADD_FEEDING = "Thêm thông tin thức ăn";
        public const string ADD_VACCINATION = "Thêm thông tin vac-xin";
        public const string ADD_CERTIFICATION = "Thêm thông tin kiểm định";
        public const string ADD_PROVIDER = "Thêm nhà cung cấp";
        public const string ADD_TREATMENT = "Thêm quy trình xử lý";
        public const string ADD_PACKAGING = "Thêm thông tin đóng gói";

        public const int CREATE_NEW_ID = 1;
        public const int ADD_FEEDING_ID = 2;
        public const int ADD_VACCINATION_ID = 3;
        public const int ADD_CERTIFICATION_ID = 4;
        public const int ADD_PROVIDER_ID = 5;
        public const int ADD_TREATMENT_ID = 6;
        public const int ADD_PACKAGING_ID = 7;
    }
}
