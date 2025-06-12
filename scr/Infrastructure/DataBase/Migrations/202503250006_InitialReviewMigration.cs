using FluentMigrator;

namespace Infrastructure.Database.Migrations
{
    [Migration(202503250006)]
    public class InitialReviewMigration : Migration
    {
        public override void Up()
        {
            Create.Table("reviews")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("message").AsString(255).NotNullable()
                .WithColumn("id_picture").AsInt32().Nullable().ForeignKey("picture_file", "id")
                .WithColumn("id_user").AsInt32().NotNullable().ForeignKey("users", "id")
                .WithColumn("id_product").AsInt32().Nullable().ForeignKey("products", "id")
            ;
        }

        public override void Down()
        {
            Delete.Table("reviews");
        }
    }
}