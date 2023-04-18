using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CqrsFramework.Domain.Entities;

namespace CqrsFramework.Persistance.Configurations
{
    public class LogEntityConfiguration : IEntityTypeConfiguration<LogEntity>
    {
        public void Configure(EntityTypeBuilder<LogEntity> builder)
        {
            builder.ToTable("Logs");
            builder.HasKey(r => r.id);
            builder.Property(r => r.username);
            builder.Property(r => r.email);
            builder.Property(r => r.ip);
            builder.Property(r => r.logdate);
            builder.Property(r => r.controller);
            builder.Property(r => r.parameters);
            builder.Property(r => r.description);
            builder.Property(r => r.status);
            builder.Property(r => r.type);
        }
    }
}
