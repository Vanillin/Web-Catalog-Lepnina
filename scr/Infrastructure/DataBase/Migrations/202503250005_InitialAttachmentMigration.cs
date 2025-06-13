using FluentMigrator;

namespace Infrastructure.Database.Migrations
{
    [Migration(202503250005)]
    public class InitialAttachmentMigration : Migration
    {
        public override void Up()
        {
            Create.Table("attachments")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().Unique()
                .WithColumn("message").AsString(255).Nullable()
                .WithColumn("id_picture").AsInt32().NotNullable().ForeignKey("picture_file", "id")
                .WithColumn("id_product").AsInt32().NotNullable().ForeignKey("products", "id")
            ;
        }
        public override void Down()
        {
            Delete.Table("attachments");
        }
    }
}