using CinemaProject.TL.DTO;

namespace CinemaProject.BL.Interfaces
{
   public interface IAccountLogic
   {
      bool IsSignedIn();
      string GetCurentUserFullName();
      string EncryptPassword(string password);
      void AddToken(TokenDTO tokenDTO);
      void RemoveAllTokensByUserId(int userId);
      void Logout();
      int GetCurentUserId();
      bool IsAdmin();
      string GetCinemaLocation();
      void EditUser(CinemaUserDTO cinemaUserDTO);
   }
}
