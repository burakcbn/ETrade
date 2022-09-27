using System.Runtime.Serialization;

namespace ETradeStudy.Application.Exceptions
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException() : base("kullanıcı kaydı oluşturulurken beklenmedik bir hata ile karşılaşıldı")
        {
        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
