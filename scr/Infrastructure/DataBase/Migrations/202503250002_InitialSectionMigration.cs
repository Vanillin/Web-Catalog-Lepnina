using FluentMigrator;

namespace Infrastructure.Database.Migrations
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
        }

        public override void Down()
        {
            Delete.Table("sections");
        }
    }
}
