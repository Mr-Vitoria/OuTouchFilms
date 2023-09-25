namespace OuTouchFilms.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string ImgUrl { get; set; } = "";
        public string TypeAccount { get; set; } = "Обычный";


        public int GetAccountImportant()
        {
            switch (TypeAccount)
            {
                case "Admin_BL":
                    return 10;
                case "Bluepuper":
                    return 9;
                case "GDMC":
                    return 8;
                case "SubAdmin":
                    return 6;
                case "Обычный":
                    return 1;
                default:
                    return 0;

            }
        }
    }
}
