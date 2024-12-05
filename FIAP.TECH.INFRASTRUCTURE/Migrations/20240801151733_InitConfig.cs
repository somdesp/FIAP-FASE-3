using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP.TECH.INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class InitConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    DDD = table.Column<string>(type: "varchar(2)", nullable: false),
                    UF = table.Column<string>(type: "varchar(2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(16)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    DDD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "IsActive", "Name", "Password" },
                values: new object[] { 1, new DateTime(2024, 8, 1, 12, 17, 33, 279, DateTimeKind.Local).AddTicks(3613), "tester@fiaptest.com.br", true, "Tech Challenge Fase1", "Senha@123" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_RegionId",
                table: "Contacts",
                column: "RegionId");

            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('11','SP','São Paulo',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('12','SP','S. José dos Campos',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('13','SP','Santos',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('14','SP','Baurú',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('15','SP','Sorocaba',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('16','SP','Ribeirão Preto',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('17','SP','S. José do Rio Preto',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('18','SP','Presidente Prudente',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('19','SP','Campinas',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('21','RJ','Rio de Janeiro',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('22','RJ','Campos',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('24','RJ','Volta Redonda',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('27','ES','Vitória',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('28','ES','Cach. de Itapemirim',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('31','MG','Belo Horizonte',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('32','MG','Juiz de Fora',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('33','MG','Gov. Valadares',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('34','MG','Uberlândia',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('35','MG','Varginha',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('37','MG','Divinópolis',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('38','MG','Montes Claros',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('41','PR','Curitiba',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('42','PR','Ponta Grossa',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('43','PR','Londrina',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('44','PR','Maringá',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('45','PR','Cascavel',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('46','PR','Pato Branco',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('47','SC','Joinville',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('48','SC','Florianópolis',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('49','SC','Chapecó',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('51','RS','Porto Alegre',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('53','RS','Pelotas',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('54','RS','Caxias do Sul',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('55','RS','Santa Maria',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('61','DF','Brasília',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('62','GO','Goiânia',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('63','TO','Palmas',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('64','GO','Rio Verde',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('65','MT','Cuiabá',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('66','MT','Rondonópolis',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('67','MS','Campo Grande',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('68','AC','Rio Branco',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('69','RO','Porto Velho',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('71','BA','Salvador',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('73','BA','Ilhéus',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('74','BA','Juazeiro',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('75','BA','Feira de Santana',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('77','BA','Barreiras',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('79','SE','Aracajú',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('81','PE','Recife',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('82','AL','Maceió',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('83','PB','João Pessoa',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('84','RN','Natal',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('85','CE','Fortaleza',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('86','PI','Teresina',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('87','PE','Petrolina',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('88','CE','Juazeiro do Norte',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('89','PI','Picos',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('91','PA','Belém',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('92','AM','Manaus',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('93','PA','Santarém',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('94','PA','Marabá',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('95','RR','Boa Vista',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('96','AP','Macapá',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('97','AM','Coari',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('98','MA','São Luis',GETDATE());");
            migrationBuilder.Sql("INSERT INTO Regions(DDD,UF,Name,CreatedDate) VALUES ('99','MA','Imperatriz',GETDATE());");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
