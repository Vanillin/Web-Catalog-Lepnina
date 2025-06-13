using FluentMigrator;

namespace Infrastructure.Database.Migrations
{
    [Migration(202503250001)]
    public class InitialUserMigration : Migration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().Unique()
                .WithColumn("name").AsString(255).NotNullable()
                .WithColumn("id_picture_icon").AsInt32().Nullable().ForeignKey("picture_file", "id")
            ;
        }
        public override void Down()
        {
            Delete.Table("users");
        }
    }
}
