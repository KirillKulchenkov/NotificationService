using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotificationService.Domain;
using NotificationService.Domain.Models;

namespace NotificationService.Infrastructure
{
    public class TokenExperation
    {
        public static void AddExpiredToken(string oldtoken, string newToken)
        {
            var dbContext = new DatabaseContext();
            var expiredToken = dbContext.ExpiredTokens.FirstOrDefault(x => x.Token == oldtoken);
            if (expiredToken != null)
            {
                if (newToken != null)
                {
                    expiredToken.NewToken = newToken;
                }
            }
            else
            {
                dbContext.ExpiredTokens.Add(new ExpiredToken(oldtoken, newToken));
            }
            dbContext.SaveChanges();
        }

        public static ExpiredToken CheckExpired(string token)
        {
            var dbContext = new DatabaseContext();
            return dbContext.ExpiredTokens.FirstOrDefault(x => x.Token == token);
        }

        public static IEnumerable<ExpiredToken> CheckExpired(string[] tokens)
        {
            var dbContext = new DatabaseContext();
            return dbContext.ExpiredTokens.Where(x => tokens.Contains(x.Token));
        } 
    }
}
