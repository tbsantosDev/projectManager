using ProjectManager.Domain.Models;
using ProjectManager.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectManager.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public UserEnums Role { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string? EmailConfirmationToken { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpires { get; set; }
        [JsonIgnore]
        public ICollection<TeamMemberModel> TeamMembers { get; set; }
        [JsonIgnore]
        public ICollection<TaskModel> Tasks { get; set; }
        [JsonIgnore]
        public ICollection<CommentModel> Comments { get; set; }

    }
}
