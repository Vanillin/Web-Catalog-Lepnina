using FluentMigrator;

namespace Infrastructure.Database.Migrations
{
    [Migration(202503250005)]
    public class InitialAttachmentMigration : Migration
    {
        public override void Up()
        {
            Create.Table("attachments")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("message").AsString(255).Nullable()
                .WithColumn("path_picture").AsString(255).NotNullable()
                .WithColumn("id_product").AsInt32().NotNullable().ForeignKey("products", "id")
            ;

            Insert.IntoTable("attachments")
                .Row(new { message = "one", path_picture = "onepath", id_product = 1 })
                .Row(new { message = "second", path_picture = "secondpath", id_product = 3 });
        }
        public override void Down()
        {
            Delete.Table("attachments");
        }
    }
}
