namespace Cephalopod.Client;

internal class BlazorConstants
{
    public const string AuthenticationScheme = "jwt";
    public const string AccessTokenCookieName = "access-token";
    public const string RefreshTokenCookieName = "refresh-token";

    public const string NoPictureUrl = "/images/NoPicture.jpg";
    public const string UploadImage = nameof(UploadImage);
    public const string Actions = nameof(Actions);
    public const string Edit = nameof(Edit);
    public const string RequiredField = nameof(RequiredField);
    public const string PleaseEnterCorrectOtpCode = nameof(PleaseEnterCorrectOtpCode);
    public const string PleaseEnterCorrectPhoneNumber = nameof(PleaseEnterCorrectPhoneNumber);
    public const string PasswordsMustMatch = nameof(PasswordsMustMatch);
    public const string PasswordsHasChanged = nameof(PasswordsHasChanged);
}