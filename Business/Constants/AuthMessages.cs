using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class AuthMessages
    {
        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Mevcut şifre hatalı";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";

        public static string PasswordChanged = "Şifre başarıyla güncellendi";

    }
}
