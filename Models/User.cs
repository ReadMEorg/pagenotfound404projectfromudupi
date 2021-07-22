namespace ReadME.Models
{
    public class User
    {

        public int Id {get; set;}

        public string Name {get;set;}

        public string Email {get;set;}

        public string Password {get;set;}

        public float Earnings {get; set;}

        public string Suggestions {get;set;}

        public bool isVerified {get; set;}
    }
}