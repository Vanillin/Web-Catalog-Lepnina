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
                .WithColumn("pathpicture").AsString(255).NotNullable()
                .WithColumn("idproduct").AsInt32().NotNullable().ForeignKey("products", "id")
            ;

            Insert.IntoTable("attachments")
                .Row(new { message = "one", pathpicture = "onepath", idproduct = 1 })
                .Row(new { message = "second", pathpicture = "secondpath", idproduct = 3 });
        }
        public override void Down()
        {
            Delete.Table("attachments");
        }
    }
}
