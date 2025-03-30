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

            Insert.IntoTable("favorites")
                    .Row(new { id_user = 1, id_product = 2 })
                    .Row(new { id_user = 2, id_product = 3 });
        }
        public override void Down()
        {
            Delete.Table("favorites");
        }
    }
}
