namespace Common.Request
{
    public class RegisterUser
    {
        public required string Email { get; set; } = string.Empty;

        public required string Firstname { get; set; }

        public required string Lastname { get; set; }

        public required string Nickname { get; set; }

        public required string Password { get; set; } = string.Empty;

        public required string Telephone { get; set; } = string.Empty;

        public required string SocialProvider { get; set; } = string.Empty;

        public required string SocialId { get; set; } = string.Empty;

    }
}

