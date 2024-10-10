using System;
using BookCatalog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCatalog.Data.Configurations;

public class BookConfig : IEntityTypeConfiguration<Book>
{
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.Property(b => b.Title).HasMaxLength(50).IsRequired();
        }
}