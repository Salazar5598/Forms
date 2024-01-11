using System;
using System.Collections.Generic;

namespace AcmeForms.Models
{
    public partial class Field
    {
        public int FieldId { get; set; }
        public string Name { get; set; } = null!;
        public string? Title { get; set; }
        public bool? Required { get; set; }
        public int? FormId { get; set; }
        public int? TypeId { get; set; }

        public virtual Form? oForm { get; set; }
        public virtual FieldsType? oType { get; set; }
    }
}
