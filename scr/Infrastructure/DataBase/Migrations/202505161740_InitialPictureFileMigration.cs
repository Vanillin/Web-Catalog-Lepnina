using FluentMigrator;

namespace Infrastructure.DataBase.Migrations
{
    [Migration(202505161740)]
    public class InitialPictureFileMigration : Migration
    {
        public override void Up()
        {
            Create.Table("picture_file")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("file_name").AsString().NotNullable()
                .WithColumn("stored_path").AsString().NotNullable()
                .WithColumn("content_type").AsString().NotNullable()
                .WithColumn("size").AsInt64().NotNullable();

            //Alter.Table("users")
            //    .AddColumn("logo_attachment_id").AsInt32().Nullable();

            //Create.ForeignKey("fk_users_logo_attachment_id")
            //    .FromTable("users").ForeignColumn("logo_attachment_id")
            //    .ToTable("attachments").PrimaryColumn("id")
            //    .OnDeleteOrUpdate(Rule.SetNull);
        }

        public override void Down()
        {
            //Delete.ForeignKey("fk_users_logo_attachment_id").OnTable("users");
            //Delete.Column("logo_attachment_id").FromTable("users");

            Delete.Table("picture_file");
        }
    }
}