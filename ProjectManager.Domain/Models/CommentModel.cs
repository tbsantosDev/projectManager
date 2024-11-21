using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        //Chave estrangeira para a tabela tarefa
        public int TaskId { get; set; }
        [JsonIgnore]
        public ICollection<TaskModel> Tasks { get; set; }
        //Chave estrangeira para a tabela usuario
        public int UserId { get; set; }
        [JsonIgnore]
        public ICollection<UserModel> Users { get; set; }
    }
}
