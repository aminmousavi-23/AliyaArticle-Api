namespace Application.Common.Resources;

public static class Messages
{
    public static class Common
    {
        public const string InternalServerError = "خطایی در سرور رخ داده است. لطفا با پشتیبانی ارتباط برقرار کنید.";
    }
    public static class User
    {
        public static class Validation
        {
            public const string UsernameRequired = "نام کاربری الزامی است.";
            public const string FullNameRequired = "نام و نام خانوادگی الزامی است.";
            public const string PhoneNumberRequired = "شماره تلفن الزامی است.";
            public const string InvalidPhoneNumber = "شماره تلفن معتبر نیست.";
            public const string InvalidEmail = "فرمت ایمیل نامعتبر است.";
            public const string PasswordRequired = "رمز عبور الزامی است.";
            public const string PasswordsAreNotEqual = "رمز عبور و تکرار آن مطابقت ندارند.";
        }

        public const string NotFound = "شخصی در سیستم یافت نشد.";
        public const string AlreadyRegistered = "در حال حاضر شخصی با این مشخصات در سیستم وجود دارد.";
        public const string InvalidPhoneNumberOrPassword =
            "شماره موبایل یا رمز عبور نامعتبر می باشد.";
        public const string IsNotActive = "کاربری شما در حال حاضر غیر فعال می باشد.\n" +
                                          "لطفا با پشتیبانی ارتباط برقرار کنید.";
    }
    public static class Auth
    {
        public const string InvalidToken = "توکن نامعتبر.";
    }
    public static class Category
    {
        public static class Validation
        {
            public const string NameRequired = "نام دسته بندی الزامی است.";
        }

        public const string NotFound = "دسته بندی ای در سیستم یافت نشد.";
        public const string Created = "دسته بندی مورد نظر با موفقیت ایجاد شد.";
        public const string Deleted = "دسته بندی مورد نظر با موفقیت حذف شد.";
        public const string CanNotDelete = "امکان حذف دسته بندی مورد نظر وجود ندارد.";
    }
    public static class Tag
    {
        public static class Validation
        {
            public const string NameRequired = "نام برچسب الزامی است.";
        }

        public const string NotFound = "برچسبی در سیستم یافت نشد.";
        public const string Created = "برچسب مورد نظر با موفقیت ایجاد شد.";
        public const string Deleted = "برچسب مورد نظر با موفقیت حذف شد.";
        public const string CanNotDelete = "امکان حذف برچسب مورد نظر وجود ندارد.";
    }
}