using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string UserNotFound = "Kullanıcı adı veya şifre hatalı.";
    public const string InvalidPassword = "Kullanıcı adı veya şifre hatalı.";
    public const string EmailAlreadyExists = "Bu email adresiyle kayıtlı bir kullanıcı mevcut.";
    public const string UserNameAlreadyExists = "Bu kullanıcı adıyla kayıtlı bir kullanıcı mevcut.";
}
