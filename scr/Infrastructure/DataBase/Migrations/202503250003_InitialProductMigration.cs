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
                .WithColumn("pathpicture").AsString(255).NotNullable()
                .WithColumn("idsection").AsInt32().NotNullable().ForeignKey("sections", "id")
            ;

            Insert.IntoTable("products")
                .Row(new { length = 50, height = 50, width = 50, price = 100, discount = 0, pathpicture = "firsticon", idsection = 1 })
                .Row(new { length = 50, height = 50, width = 50, price = 100, discount = 0, pathpicture = "secondicon", idsection = 1 })
                .Row(new { length = 50, height = 50, width = 50, price = 100, discount = 0, pathpicture = "thirdicon", idsection = 2 })
            ;
        }
        public override void Down()
        {
            Delete.Table("products");
        }
    }
}
