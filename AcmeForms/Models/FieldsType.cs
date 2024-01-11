using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AcmeForms.Models
{
    public partial class FieldsType
    {
        public FieldsType()
        {
            Fields = new HashSet<Field>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Field> Fields { get; set; }
    }
}
