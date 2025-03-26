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
                .WithColumn("pathpicture").AsString(255).Nullable()
                .WithColumn("iduser").AsInt32().NotNullable().ForeignKey("users", "id")
                .WithColumn("idproduct").AsInt32().Nullable().ForeignKey("products", "id")
            ;

            Insert.IntoTable("reviews")
                .Row(new { message = "firstmessage", pathpicture = "firstpath", iduser = 1, idproduct = 2 })
                .Row(new { message = "secondmessage", pathpicture = "secondpath", iduser = 3, idproduct = 2 })
            ;
        }

        public override void Down()
        {
            Delete.Table("reviews");
        }
    }
}
