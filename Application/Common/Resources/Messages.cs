namespace Application.Common.Resources;

public static class Messages
{
    public static class Common
    {
        public const string ValidationFailureMessage = "خطا در مقادیر ورودی.";
        public const string InternalServerError = "خطایی در سرور رخ داده است. لطفا با پشتیبانی ارتباط برقرار کنید.";
    }

    public static class User
    {
        public const string NotFound = "شخصی در سیستم یافت نشد.";
        public const string AlreadyRegistered = "در حال حاضر شخصی با این شماره موبایل یا ایمیل در سیستم وجود دارد.";
    }
}