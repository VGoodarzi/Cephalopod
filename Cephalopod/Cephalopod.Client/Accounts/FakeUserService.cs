using Cephalopod.Client.Contracts;

namespace Cephalopod.Client.Accounts;

public class FakeUserService : IUserService
{
    public async Task<SignInResponse> SignInWithPassword(SignInWithPasswordRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        return new SignInResponse
        {
            AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIwMTk4OTM3MS01OTA4LTc0ZjUtODQ1Yy1jMjNmMzBlZTE5ODUiLCJnaXZlbl9uYW1lIjoiYWRtaW4iLCJmYW1pbHlfbmFtZSI6ImFkbWluIiwibmFtZWlkIjoiYWRtaW4iLCJ1aWQiOiI1ZjRjYjc4Ny01OGRhLTRmODMtOGFhYS1iYjQ4ZTRlMjc4YzMiLCJpcCI6IjEyNy4wLjAuMSIsInJvbGVzIjpbIkdsb2JhbFJlYWQiLCJHbG9iYWxXcml0ZSIsIkJhc2ljIl0sInN1YiI6Iis5ODkxMjEwMDAwMDAiLCJleHAiOjE3NTQ4MjM5MzUsImlzcyI6IkNvcmVJZGVudGl0eSIsImF1ZCI6IkNvcmVJZGVudGl0eVVzZXIifQ.uxfl19BD4yoRmwBUOK_9H2yOTmWyg59Rr00pMbTIGp0",
            PhoneNumber = "+989212681463",
            TokenType = "Bearer",
            RefreshToken = "uxfl19BD4yoRmwBUOK_9H2yOTmWyg59Rr00pMbTIGp0",
            UserId = "1",
            ExpireIn = 1,
            HasPassword = true
        };
    }
}

