using FluentMigrator;

namespace Infrastructure.DataBase.Migrations
{
    [Migration(202503250004)]
    public class InitialFavoritesMigration : Migration
    {
        public override void Up()
        {
            Create.Table("favorites")
                .WithColumn("iduser").AsInt32().PrimaryKey().NotNullable().ForeignKey("users", "id")
                .WithColumn("idproduct").AsInt32().PrimaryKey().NotNullable().ForeignKey("products", "id")
            ;

            Insert.IntoTable("favorites")
                    .Row(new { iduser = 1, idproduct = 2 })
                    .Row(new { iduser = 2, idproduct = 3 });
        }
        public override void Down()
        {
            Delete.Table("favorites");
        }
    }
}
