using FluentMigrator;

namespace Infrastructure.DataBase.Migrations
{
    [Migration(202503250001)]
    public class InitialUserMigration : Migration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(255).NotNullable()
                .WithColumn("pathicon").AsString(255).Nullable()
            ;

            Insert.IntoTable("users")
                .Row(new { name = "firstuser", pathicon = "firstusericon" })
                .Row(new { name = "seconduser", pathicon = "secondusericon" })
                .Row(new { name = "thirduser", pathicon = "thirdusericon" });
        }
        public override void Down()
        {
            Delete.Table("users");
        }
    }
}