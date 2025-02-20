namespace Common.Request
{
    public class RegisterUser
    {
        public required string Firstname { get; set; }

        public required string Lastname { get; set; }

        public required string Nickname { get; set; }
    }
}

