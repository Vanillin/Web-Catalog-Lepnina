using FluentMigrator;

namespace Infrastructure.Database.Migrations
{
    [Migration(202503250001)]
    public class InitialUserMigration : Migration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(255).NotNullable()
                .WithColumn("path_icon").AsString(255).Nullable()
            ;

            Insert.IntoTable("users")
                .Row(new { name = "firstuser", path_icon = "firstusericon" })
                .Row(new { name = "seconduser", path_icon = "secondusericon" })
                .Row(new { name = "thirduser", path_icon = "thirdusericon" });
        }
        public override void Down()
        {
            Delete.Table("users");
        }
    }
}