using FluentMigrator;

namespace Infrastructure.Database.Migrations
{
    [Migration(202503250003)]
    public class InitialProducttMigration : Migration
    {
        public override void Up()
        {
            Create.Table("products")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("length").AsDouble().NotNullable()
                .WithColumn("height").AsDouble().NotNullable()
                .WithColumn("width").AsDouble().NotNullable()
                .WithColumn("price").AsDouble().Nullable()
                .WithColumn("discount").AsDouble().Nullable()
                .WithColumn("path_picture").AsString(255).NotNullable()
                .WithColumn("id_section").AsInt32().NotNullable().ForeignKey("sections", "id")
            ;

            Insert.IntoTable("products")
                .Row(new { length = 50, height = 50, width = 50, price = 100, discount = 0, path_picture = "firsticon", id_section = 1 })
                .Row(new { length = 50, height = 50, width = 50, price = 100, discount = 0, path_picture = "secondicon", id_section = 1 })
                .Row(new { length = 50, height = 50, width = 50, price = 100, discount = 0, path_picture = "thirdicon", id_section = 2 })
            ;
        }
        public override void Down()
        {
            Delete.Table("products");
        }
    }
}
