using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Constants;

internal static class ErrorMessages
{
    internal const string UserNotFound = "User not found";
    internal const string InvalidPassword = "Invalid password";
    internal const string EmailInUse = "Email address is already in use";
    internal const string TokenNotFound = "User not found";
    internal const string InvalidToken = "Invalid refresh token";
    internal const string IpDoesNotMatch = "Ip address does not match";

}
