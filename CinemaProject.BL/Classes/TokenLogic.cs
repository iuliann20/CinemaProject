using CinemaProject.BL.Interfaces;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;

namespace CinemaProject.BL.Classes
{
   public class TokenLogic : ITokenLogic
   {
      private readonly ITokenRepository _tokenRepository;

      public TokenLogic(ITokenRepository tokenRepository)
      {
         _tokenRepository = tokenRepository;
      }

      public TokenDTO GetTokenByValue(string tokenValue)
      {
         return _tokenRepository.GetTokenByValue(tokenValue);
      }

      public void RemoveTokenById(int id)
      {
         _tokenRepository.RemoveTokenById(id);
      }
   }
}
