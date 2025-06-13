using FluentMigrator;

namespace Infrastructure.DataBase.Migrations
{
    [Migration(202503250000)]
    public class InitialPictureFileMigration : Migration
    {
        public override void Up()
        {
            Create.Table("picture_file")
                .WithColumn("id").AsInt32().PrimaryKey().Identity().Unique()
                .WithColumn("file_name").AsString().NotNullable()
                .WithColumn("stored_path").AsString().NotNullable()
                .WithColumn("content_type").AsString().NotNullable()
                .WithColumn("size").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("picture_file");
        }
    }
}