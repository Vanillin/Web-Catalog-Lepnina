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
                .WithColumn("path_picture").AsString(255).Nullable()
                .WithColumn("id_user").AsInt32().NotNullable().ForeignKey("users", "id")
                .WithColumn("id_product").AsInt32().Nullable().ForeignKey("products", "id")
            ;

            Insert.IntoTable("reviews")
                .Row(new { message = "firstmessage", path_picture = "firstpath", id_user = 1, id_product = 2 })
                .Row(new { message = "secondmessage", path_picture = "secondpath", id_user = 3, id_product = 2 })
            ;
        }

        public override void Down()
        {
            Delete.Table("reviews");
        }
    }
}
