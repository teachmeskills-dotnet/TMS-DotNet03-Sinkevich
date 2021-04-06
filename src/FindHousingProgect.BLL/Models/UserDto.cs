namespace FindHousingProject.BLL.Models
{
    /// <summary>
    /// User data transfer object.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Full Name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Role owner or guest.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        /// Is owner.
        /// </summary>
        public bool IsOwner { get; set; }
    }
}
