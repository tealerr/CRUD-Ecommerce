namespace Common.Request
{
    public class UpdateUser
    {
        public required string UserGuid { get; set; }
        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Nickname { get; set; }

    }
}

