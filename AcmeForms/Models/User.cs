using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AcmeForms.Models
{
    public partial class User
    {
        public User()
        {
            Forms = new HashSet<Form>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string User1 { get; set; } = null!;
        public string? Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<Form> Forms { get; set; }
    }
}
