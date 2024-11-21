﻿using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManager.Domain.Models
{
    public class TeamMemberModel
    {
        public int Id { get; set; }
        //chave estrangeira para a tabela time
        public int TeamId { get; set; }
        [JsonIgnore]
        public TeamModel Team { get; set; }
        //chave estrangeira para a tabela usuarios
        public int UserId { get; set; }
        [JsonIgnore]
        public UserModel User { get; set; }
        [Required]
        public DateTime EntryDate { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
