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
            public const string UsernameMaxLength = "نام کاربری نمی‌تواند بیشتر از 64 کاراکتر باشد.";

            public const string FullNameRequired = "نام و نام خانوادگی الزامی است.";
            public const string FullNameMaxLength = "نام و نام خانوادگی نمی‌تواند بیشتر از 128 کاراکتر باشد.";

            public const string PhoneNumberRequired = "شماره موبایل الزامی است.";
            public const string PhoneNumberMaxLength = "شماره موبایل نمی‌تواند بیشتر از 15 کاراکتر باشد.";
            public const string InvalidPhoneNumber = "شماره موبایل معتبر نیست.";

            public const string EmailMaxLength = "ایمیل نمی‌تواند بیشتر از 256 کاراکتر باشد.";
            public const string InvalidEmail = "ایمیل معتبر نیست.";

            public const string PasswordRequired = "رمز عبور الزامی است.";
            public const string PasswordMinLength = "رمز عبور باید حداقل 8 کاراکتر باشد.";
            public const string PasswordMaxLength = "رمز عبور نمی‌تواند بیشتر از 128 کاراکتر باشد.";

            public const string PasswordsAreNotEqual = "رمز عبور و تکرار آن یکسان نیستند.";
        }

        public const string NotFound = "شخصی در سیستم یافت نشد.";
        public const string AlreadyRegistered = "در حال حاضر شخصی با این مشخصات در سیستم وجود دارد.";
        public const string InvalidUsernameOrPassword = "نام کاربری یا رمز عبور نامعتبر می باشد.";
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
            public const string NameRequired = "نام دسته‌بندی الزامی است.";
            public const string NameMaxLength = "نام دسته‌بندی نمی‌تواند بیشتر از 128 کاراکتر باشد.";
        }

        public const string NotFound = "دسته بندی در سیستم یافت نشد.";
        public const string Created = "دسته بندی مورد نظر با موفقیت ایجاد شد.";
        public const string Deleted = "دسته بندی مورد نظر با موفقیت حذف شد.";
        public const string CanNotDelete = "امکان حذف دسته بندی مورد نظر وجود ندارد.";
    }
    public static class Tag
    {
        public static class Validation
        {
            public const string NameRequired = "نام برچسب الزامی است.";
            public const string NameMaxLength = "نام برچسب نمی‌تواند بیشتر از 64 کاراکتر باشد.";
        }

        public const string NotFound = "برچسب در سیستم یافت نشد.";
        public const string Created = "برچسب مورد نظر با موفقیت ایجاد شد.";
        public const string Deleted = "برچسب مورد نظر با موفقیت حذف شد.";
        public const string CanNotDelete = "امکان حذف برچسب مورد نظر وجود ندارد.";
    }
    public static class Article
    {
        public static class Validation
        {
            public const string TitleRequired = "عنوان مقاله الزامی است.";
            public const string TitleMaxLength = "طول عنوان مقاله بیش از حد مجاز است.";

            public const string SummaryRequired = "خلاصه مقاله الزامی است.";
            public const string SummaryMaxLength = "طول خلاصه مقاله بیش از حد مجاز است.";

            public const string CategoryRequired = "انتخاب دسته‌بندی الزامی است.";

            public const string BlocksRequired = "مقاله باید حداقل یک بخش داشته باشد.";

            public const string InvalidBlockType = "نوع بلوک نامعتبر است.";
            public const string BlockOrderInvalid = "ترتیب بلوک نامعتبر است.";
            public const string BlockTextRequired = "متن این بخش الزامی است.";
            public const string Base64FileRequired = "آپلود فایل الزامی است.";
            public const string FileSizeExceeded = "حجم فایل بارگذاری شده نباید بیش از 3 مگابایت باشد.";
            public const string FileTypeNotAllowed = "نوع فایل باید یکی از تایپ های JPG ،PNG یا PDF باشد.";
        }

        public const string NotFound = "مقاله در سیستم یافت نشد.";
        public const string Created = "مقاله مورد نظر با موفقیت ایجاد شد.";
        public const string Deleted = "مقاله مورد نظر با موفقیت حذف شد.";
        public const string Published = "مقاله مورد نظر با موفقیت پابلیش شد.";
    }
    public static class Comment
    {
        public static class Validation
        {
            public const string AuthorNameRequired = "نام نویسنده الزامی است.";
            public const string AuthorNameMaxLength = "نام نویسنده نمی‌تواند بیشتر از 128 کاراکتر باشد.";

            public const string AuthorEmailRequired = "ایمیل نویسنده الزامی است.";
            public const string AuthorEmailMaxLength = "ایمیل نمی‌تواند بیشتر از 256 کاراکتر باشد.";
            public const string InvalidAuthorEmail = "ایمیل معتبر نیست.";

            public const string ContentRequired = "متن دیدگاه الزامی است.";
            public const string ContentMinLength = "متن دیدگاه باید حداقل 2 کاراکتر باشد.";
            public const string ContentMaxLength = "متن دیدگاه نمی‌تواند بیشتر از 2048 کاراکتر باشد.";

            public const string ArticleRequired = "مقاله انتخاب نشده است.";
        }

        public const string NotFound = "دیدگاهی در سیستم یافت نشد.";
        public const string Created = "دیدگاه مورد نظر با موفقیت ثبت شد.";
        public const string Deleted = "دیدگاه مورد نظر با موفقیت حذف شد.";
    }
    public static class Attachment
    {
        public const string NotFound = "فایل مورد نظر یافت نشد.";
    }
}