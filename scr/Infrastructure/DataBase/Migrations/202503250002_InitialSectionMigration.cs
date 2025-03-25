using FluentMigrator;

namespace Infrastructure.DataBase.Migrations
{
    [Migration(202503250002)]
    public class InitialSectionMigration : Migration
    {
        public override void Up()
        {
            Create.Table("sections")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(255).NotNullable()
            ;

            Insert.IntoTable("sections")
                .Row(new { name = "first" })
                .Row(new { name = "second" });
        }

        public override void Down()
        {
            Delete.Table("sections");
        }
    }
}
