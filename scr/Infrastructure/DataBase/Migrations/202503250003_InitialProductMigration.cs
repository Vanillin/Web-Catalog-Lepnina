using FluentMigrator;

namespace Infrastructure.Database.Migrations
{
    [Migration(202503250003)]
    public class InitialProducttMigration : Migration
    {
        public override void Up()
        {
            Create.Table("products")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().Unique()
                .WithColumn("length").AsDouble().NotNullable()
                .WithColumn("height").AsDouble().NotNullable()
                .WithColumn("width").AsDouble().NotNullable()
                .WithColumn("price").AsDouble().Nullable()
                .WithColumn("discount").AsDouble().Nullable()
                .WithColumn("id_picture").AsInt32().NotNullable().ForeignKey("picture_file", "id")
                .WithColumn("id_section").AsInt32().NotNullable().ForeignKey("sections", "id")
            ;
        }
        public override void Down()
        {
            Delete.Table("products");
        }
    }
}