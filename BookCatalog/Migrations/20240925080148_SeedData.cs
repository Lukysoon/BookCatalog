using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCatalog.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        bool seedData = false;
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (seedData)
            {
                var janeAustenId = Guid.NewGuid();
                var georgeOrwellId = Guid.NewGuid();
                var gabrielGarciaMarquezId = Guid.NewGuid();
                var virginiaWoolfId = Guid.NewGuid();
                var harukiMurakamiId = Guid.NewGuid();

                migrationBuilder.InsertData(
                    table: "Authors",
                    columns: new[] { "Id", "FirstName", "LastName" },
                    values: new object[,]
                    {
                        { janeAustenId, "Jane", "Austen" },
                        { georgeOrwellId, "George", "Orwell" },
                        { gabrielGarciaMarquezId, "Gabriel", "García Márquez" },
                        { virginiaWoolfId, "Virginia", "Woolf" },
                        { harukiMurakamiId, "Haruki", "Murakami" }
                    });

                migrationBuilder.InsertData(
                    table: "Books",
                    columns: new[] { "Id", "Title", "AuthorId" },
                    values: new object[,]
                    {
                        { Guid.NewGuid(), "Pride and Prejudice", janeAustenId },
                        { Guid.NewGuid(), "Sense and Sensibility", janeAustenId },
                        { Guid.NewGuid(), "1984", georgeOrwellId },
                        { Guid.NewGuid(), "Animal Farm", georgeOrwellId },
                        { Guid.NewGuid(), "One Hundred Years of Solitude", gabrielGarciaMarquezId },
                        { Guid.NewGuid(), "Love in the Time of Cholera", gabrielGarciaMarquezId },
                        { Guid.NewGuid(), "Mrs. Dalloway", virginiaWoolfId },
                        { Guid.NewGuid(), "To the Lighthouse", virginiaWoolfId },
                        { Guid.NewGuid(), "Norwegian Wood", harukiMurakamiId },
                        { Guid.NewGuid(), "Kafka on the Shore", harukiMurakamiId }
                    });
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (seedData)
            {
                migrationBuilder.DeleteData(
                    table: "Books",
                    keyColumn: "Title",
                    keyValues: new object[] { 
                        "Pride and Prejudice", "Sense and Sensibility", 
                        "1984", "Animal Farm", 
                        "One Hundred Years of Solitude", "Love in the Time of Cholera",
                        "Mrs. Dalloway", "To the Lighthouse",
                        "Norwegian Wood", "Kafka on the Shore"
                    });

                migrationBuilder.DeleteData(
                    table: "Authors",
                    keyColumn: "LastName",
                    keyValues: new object[] { "Austen", "Orwell", "García Márquez", "Woolf", "Murakami" });
            }
        }   
    }
}
