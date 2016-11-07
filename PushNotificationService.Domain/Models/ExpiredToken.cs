using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Models
{
    public class ExpiredToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string NewToken { get; set; }
        public DateTime Expired { get; set; }

        public ExpiredToken()
        {
            Id = Guid.NewGuid();
            Expired = DateTime.Now;
        }

        public ExpiredToken(string oldToken, string newToken)
        {
            Id = Guid.NewGuid();
            Expired = DateTime.Now;
            Token = oldToken;
            NewToken = newToken;
        }
    }
}
