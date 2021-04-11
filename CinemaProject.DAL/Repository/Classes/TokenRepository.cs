using CinemaProject.DAL.Entities;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using System.Collections.Generic;
using System.Linq;

namespace CinemaProject.DAL.Repository.Classes
{
   public class TokenRepository : ITokenRepository
   {
      private readonly CinemaDbContext _cinemaDbContext;

      public TokenRepository(CinemaDbContext cinemaDbContext)
      {
         _cinemaDbContext = cinemaDbContext;
      }

      public void AddToken(TokenDTO tokenDTO)
      {
         _cinemaDbContext.AccessTokens.Add(new Token {
            AccessToken = tokenDTO.AccessToken,
            ExpirationDate = tokenDTO.ExpirationDate,
            TokenId = tokenDTO.TokenId,
            UserId = tokenDTO.UserId
         });
         _cinemaDbContext.SaveChanges();
      }

      public TokenDTO GetTokenByValue(string tokenValue)
      {
         Token tokenFromDb = _cinemaDbContext.AccessTokens.FirstOrDefault(x => x.AccessToken == tokenValue);
         if (tokenFromDb == null) {
            return null;
         }
         return new TokenDTO {
            TokenId = tokenFromDb.TokenId,
            AccessToken = tokenFromDb.AccessToken,
            ExpirationDate = tokenFromDb.ExpirationDate,
            UserId = tokenFromDb.UserId
         };
      }

      public void RemoveTokenById(int id)
      {
         Token tokenFromDb = _cinemaDbContext.AccessTokens.FirstOrDefault(x => x.TokenId == id);
         if (tokenFromDb != null) {
            _cinemaDbContext.AccessTokens.Remove(tokenFromDb);
            _cinemaDbContext.SaveChanges();
         }
      }
      public void RemoveAllTokensByUserId(int userId)
      {
         List<Token> tokensUser = _cinemaDbContext.AccessTokens.Where(x => x.UserId == userId).ToList();
         foreach (Token tokenUser in tokensUser) {
            if (tokenUser != null) {
               RemoveTokenById(tokenUser.TokenId);
            }
         }
      }
   }
}
