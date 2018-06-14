using Microsoft.EntityFrameworkCore.Migrations;

namespace TavernAPI.Migrations
{
    public partial class dbcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerName = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    race = table.Column<string>(nullable: true),
                    subRace = table.Column<string>(nullable: true),
                    charClass = table.Column<string>(nullable: true),
                    alignment = table.Column<string>(nullable: true),
                    background = table.Column<string>(nullable: true),
                    size = table.Column<string>(nullable: true),
                    speed = table.Column<int>(nullable: false),
                    HitPoints = table.Column<int>(nullable: false),
                    SavingThrows = table.Column<string>(nullable: true),
                    Strength = table.Column<int>(nullable: false),
                    StrMod = table.Column<int>(nullable: false),
                    Dexterity = table.Column<int>(nullable: false),
                    DexMod = table.Column<int>(nullable: false),
                    Constitution = table.Column<int>(nullable: false),
                    ConMod = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false),
                    IntMod = table.Column<int>(nullable: false),
                    Wisdom = table.Column<int>(nullable: false),
                    WisMod = table.Column<int>(nullable: false),
                    Charisma = table.Column<int>(nullable: false),
                    ChaMod = table.Column<int>(nullable: false),
                    Skills = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_Character_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_PlayerId",
                table: "Character",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
