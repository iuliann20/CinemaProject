using CinemaProject.TL.DTO;

namespace CinemaProject.BL.Interfaces
{
   public interface ITokenLogic
   {
      TokenDTO GetTokenByValue(string tokenValue);
      void RemoveTokenById(int id);
   }
}
