using InsurantSales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurantSales.DataAccess.EntityConfiguration
{
    public class EntityConfigurations : IEntityTypeConfiguration<Document>, IEntityTypeConfiguration<ExtractedField>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasOne<User>(d => d.User)
              .WithMany(u => u.Documents)
              .HasForeignKey(d => d.UserId);
        }

        public void Configure(EntityTypeBuilder<ExtractedField> builder)
        {
            builder.HasOne<Document>(f => f.Document)
              .WithMany(d => d.ExtractedFields)
              .HasForeignKey(f => f.DocumentId);
        }
    }
}
