using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static InsurantSales.Domain.Enums.Enums;

namespace InsurantSales.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public long? TelegramId { get; set; }

        public string? Username { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        public DocumentStep Step { get; set; } = DocumentStep.None;

        public ICollection<Document>? Documents { get; } = new List<Document>();
    }
}
