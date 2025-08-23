using Cephalopod.Contracts.Utilities;

namespace Cephalopod.Client.Accounts;

public class FakeTranslator : ITranslator
{

    private readonly Dictionary<string, string> _fa = [];

    public FakeTranslator()
    {
        _fa.Add("Home","داشبورد");
        _fa.Add("Counter","شمارنده");
        _fa.Add("Weather", "آب و هوا");
        _fa.Add("Brand", "برند");
        _fa.Add("Name", "عنوان");
        _fa.Add("Status", "وضعیت");
        _fa.Add("ImageUrl", "تصویر");
        _fa.Add("Description", "توضیحات");
        _fa.Add("Inactive", "غیرفعال");
        _fa.Add("Active", "فعال");
        _fa.Add("Badoomeh", "بادومه");
        _fa.Add("Login", "ورود");
        _fa.Add("UserNameOrPhoneNumber", "نام کاربری یا شماره همراه");
        _fa.Add("PhoneNumber", "شماره همراه");
        _fa.Add("Password", "رمز عبور");
        _fa.Add("Logout", "خروج");
        _fa.Add("SignInWithOtp", "ورود با رمز یکبار مصرف");
        _fa.Add("ForgotPassword", "فراموشی رمز عبور");
        _fa.Add("SendOtp", "ارسال کد یکبار مصرف");
        _fa.Add("SignInWithPassword", "ورود با رمز عبور");
        _fa.Add("OtpCodeTo", "کد ارسالی به");
        _fa.Add("ChangePhoneNumber", "ویرایش شماره همراه");
        _fa.Add("SendOtpAgain", "ارسال مجدد");
        _fa.Add("Confirm", "تایید");
        _fa.Add("CompareNewPassword", "تکرار رمز عبور جدید");
        _fa.Add("NewPassword", "رمز عبور جدید");
        _fa.Add("SystemLogout", "خروج از سیستم");
        _fa.Add("Exit", "خروج");
        _fa.Add("Cancel", "منصرف شدم");
        _fa.Add("DoYouWantToExit", "آیا میخواهید خارج شوید؟");
        _fa.Add(BlazorConstants.RequiredField, "لطفا این قسمت را پر کنید");
        _fa.Add(BlazorConstants.PleaseEnterCorrectOtpCode, "لطفا کد را به درستی وارد کنید");
        _fa.Add(BlazorConstants.PleaseEnterCorrectPhoneNumber, "لطفا شماره همراه را به درستی وارد کنید");
        _fa.Add(BlazorConstants.PasswordsMustMatch, "مقدار وارد شده با رمز عبور مطابقت ندارد");
        _fa.Add(BlazorConstants.PasswordsHasChanged, "رمز عبور با موفقیت تغییر یافت");
        _fa.Add(BlazorConstants.Actions, "عملیات");
        _fa.Add(BlazorConstants.Edit, "ویرایش");
        _fa.Add(BlazorConstants.UploadImage, "بارگزاری تصویر");
    }

    public string this[string name] => _fa.GetValueOrDefault(name) ?? name;
}