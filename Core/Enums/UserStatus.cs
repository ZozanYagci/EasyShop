using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum UserStatus
    {
        Active = 1,    //Kullanıcı Aktif
        Inactive = 2,  //Pasif 
        Banned = 3,    //Yasaklı
        Suspended = 4  //Geçici olarak askıya alınmış
    }
}
