using NotificationService.Domain.Models;

namespace NotificationService.Domain.Dto
{
    public class DtoExpiredToken
    {
        public string Token { get; set; }
        public string NewToken { get; set; }

        public static DtoExpiredToken FromDataModel(ExpiredToken expiredToken)
        {
            return new DtoExpiredToken() {Token = expiredToken.Token,NewToken = expiredToken.NewToken};
        }
    }
}