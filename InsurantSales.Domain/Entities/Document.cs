using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.Domain.Entities
{
    public class Document
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string DocumentName { get; set; }

        [Required]
        public string DocumentFilePaths { get; set; }

        public User? User { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public ICollection<ExtractedField>? ExtractedFields { get; } = new List<ExtractedField>();
    }
}
