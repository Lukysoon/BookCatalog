using System;
using BookCatalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCatalog.Data.Configurations;

public class BookConfig : IEntityTypeConfiguration<Book>
{
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Id).HasDefaultValueSql("gen_random_uuid()").IsRequired();
            builder.Property(b => b.Title).HasMaxLength(50).IsRequired();
            
            builder
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
}