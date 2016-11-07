namespace NotificationService.Domain.Dto
{
    public class DtoExpiredToken
    {
        public string Token { get; set; }
        public string NewToken { get; set; }
    }
}