using System;
using BookCatalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCatalog.Data.Configurations;

public class AuthorConfig : IEntityTypeConfiguration<Author>
{
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(b => b.Id).HasDefaultValueSql("gen_random_uuid()").IsRequired();
            builder.Property(b => b.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(b => b.LastName).HasMaxLength(50).IsRequired();
            
            builder
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
}
