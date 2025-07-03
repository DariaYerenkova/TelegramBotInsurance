using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Domain.Entities
{
    public class ExtractedField
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ExtractedFieldName { get; set; }

        [Required]
        public string ExtractedFieldValue { get; set; }

        public Document? Document { get; set; }

        [Required]
        public Guid DocumentId { get; set; }
    }
}
