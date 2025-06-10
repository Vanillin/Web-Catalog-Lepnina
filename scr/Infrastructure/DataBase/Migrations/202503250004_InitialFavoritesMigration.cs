using FluentMigrator;

namespace Infrastructure.Database.Migrations
{
    [Migration(202503250004)]
    public class InitialFavoritesMigration : Migration
    {
        public override void Up()
        {
            Create.Table("favorites")
                .WithColumn("id_user").AsInt32().PrimaryKey().NotNullable().ForeignKey("users", "id")
                .WithColumn("id_product").AsInt32().PrimaryKey().NotNullable().ForeignKey("products", "id")
            ;
        }
        public override void Down()
        {
            Delete.Table("favorites");
        }
    }
}
