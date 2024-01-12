using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AcmeForms.Models
{
    public partial class Form
    {
        public Form()
        {
            Fields = new HashSet<Field>();
        }

        public int FormId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Link { get; set; }
        public int? UserId { get; set; }

        public virtual User? oUser { get; set; }

        public virtual ICollection<Field> Fields { get; set; }
    }
}
