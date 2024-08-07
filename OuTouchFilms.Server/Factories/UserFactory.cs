using OuTouchAnime.Server.Entity.Models;
using OuTouchAnime.Server.Services;
using OuTouchAnime.Server.Services.Models;
using System.Security.Cryptography;
using System.Text;

namespace OuTouchAnime.Server.Factories
{
    public class UserFactory
    {
        private readonly IUserAnimeService userAnimeService;
        private readonly IConfiguration configuration;

        public UserFactory(IUserAnimeService _userAnimeService, IConfiguration _configuration)
        {
            this.userAnimeService = _userAnimeService;
            this.configuration = _configuration;
        }


        public User GetDbFromView(UserView objectView)
        {
            return new User() { 
                Id = objectView.Id,
                Email = objectView.Email,
                Password = objectView.Password,
                Login = objectView.Login,
                ImgUrl = objectView.ImgUrl,
                RoleId = objectView.RoleId,
                NeedEmailSend = objectView.NeedEmailSend,
                Token = objectView.Token
            };
        }

        public async Task<UserView?> GetViewFromDb(User? objectDb, bool needUserAnime = false)
        {
            if(objectDb == null)
            {
                return null;
            }

            return new UserView()
            {
                Id = objectDb.Id,
                Email = objectDb.Email,
                Password = objectDb.Password,
                Login = objectDb.Login,
                ImgUrl = objectDb.ImgUrl,
                RoleId = objectDb.RoleId,
                Role = objectDb.Role,
                NeedEmailSend = objectDb.NeedEmailSend,
                UsersAnimes = needUserAnime ? (await userAnimeService.GetUserAnimeList(objectDb.Id)).Item1 : new List<UserAnimeView>(),
                Token = objectDb.Token
            };
        }

        public string EncryptPassword(string text)
        {
            string EncryptionKey = configuration["EncryptionKey"] ?? "";
            byte[] clearBytes = Encoding.Unicode.GetBytes(text);
            string clearText = "";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}
