using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace PoliticPolls.DataModel.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "POLLSDB");

            migrationBuilder.CreateTable(
                name: "respondents",
                schema: "POLLSDB",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "VARCHAR2(100)", nullable: true),
                    surname = table.Column<string>(type: "VARCHAR2(100)", nullable: true),
                    patro = table.Column<string>(type: "VARCHAR2(100)", nullable: true),
                    birth_date = table.Column<DateTime>(type: "DATE", nullable: true, defaultValueSql: "CURRENT_DATE ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respondents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "terrtitory",
                schema: "POLLSDB",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    territory_name = table.Column<string>(type: "VARCHAR2(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terrtitory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "poll",
                schema: "POLLSDB",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    id_respondent = table.Column<decimal>(type: "NUMBER", nullable: false),
                    poll_date = table.Column<DateTime>(type: "DATE", nullable: true, defaultValueSql: "CURRENT_DATE ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_poll", x => x.id);
                    table.ForeignKey(
                        name: "fk_poll_respondents",
                        column: x => x.id_respondent,
                        principalSchema: "POLLSDB",
                        principalTable: "respondents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "politicians",
                schema: "POLLSDB",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "VARCHAR2(100)", nullable: true),
                    surname = table.Column<string>(type: "VARCHAR2(100)", nullable: true),
                    patro = table.Column<string>(type: "VARCHAR2(100)", nullable: true),
                    id_territory = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_politicians", x => x.id);
                    table.ForeignKey(
                        name: "fk_politicians_terrtitory",
                        column: x => x.id_territory,
                        principalSchema: "POLLSDB",
                        principalTable: "terrtitory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "POLLSDB",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "NUMBER", nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    text = table.Column<string>(type: "VARCHAR2(1000)", nullable: true),
                    id_politician = table.Column<decimal>(type: "NUMBER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_politicians",
                        column: x => x.id_politician,
                        principalSchema: "POLLSDB",
                        principalTable: "politicians",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "politician_sets",
                schema: "POLLSDB",
                columns: table => new
                {
                    id_poll = table.Column<decimal>(type: "NUMBER", nullable: false),
                    id_politician = table.Column<decimal>(type: "NUMBER", nullable: false),
                    rating = table.Column<decimal>(type: "NUMBER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_politician_sets", x => new { x.id_poll, x.id_politician });
                    table.ForeignKey(
                        name: "fk_politician_sets_politicians",
                        column: x => x.id_politician,
                        principalSchema: "POLLSDB",
                        principalTable: "politicians",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_politician_sets_poll",
                        column: x => x.id_poll,
                        principalSchema: "POLLSDB",
                        principalTable: "poll",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_sets",
                schema: "POLLSDB",
                columns: table => new
                {
                    id_poll = table.Column<decimal>(type: "NUMBER", nullable: false),
                    id_order = table.Column<decimal>(type: "NUMBER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_sets", x => new { x.id_poll, x.id_order });
                    table.ForeignKey(
                        name: "fk_order_sets_orders",
                        column: x => x.id_order,
                        principalSchema: "POLLSDB",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_order_sets_poll",
                        column: x => x.id_poll,
                        principalSchema: "POLLSDB",
                        principalTable: "poll",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_sets_id_order",
                schema: "POLLSDB",
                table: "order_sets",
                column: "id_order");

            migrationBuilder.CreateIndex(
                name: "_1",
                schema: "POLLSDB",
                table: "order_sets",
                columns: new[] { "id_poll", "id_order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "pk_orders_id",
                schema: "POLLSDB",
                table: "orders",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_id_politician",
                schema: "POLLSDB",
                table: "orders",
                column: "id_politician");

            migrationBuilder.CreateIndex(
                name: "IX_politician_sets_id_politician",
                schema: "POLLSDB",
                table: "politician_sets",
                column: "id_politician");

            migrationBuilder.CreateIndex(
                name: "_0",
                schema: "POLLSDB",
                table: "politician_sets",
                columns: new[] { "id_poll", "id_politician" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "pk_politicians_id",
                schema: "POLLSDB",
                table: "politicians",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_politicians_id_territory",
                schema: "POLLSDB",
                table: "politicians",
                column: "id_territory");

            migrationBuilder.CreateIndex(
                name: "pk_poll_id",
                schema: "POLLSDB",
                table: "poll",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "unq_poll_id_respondent",
                schema: "POLLSDB",
                table: "poll",
                column: "id_respondent",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "pk_respondents_id",
                schema: "POLLSDB",
                table: "respondents",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "pk_terrtitory_id",
                schema: "POLLSDB",
                table: "terrtitory",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_sets",
                schema: "POLLSDB");

            migrationBuilder.DropTable(
                name: "politician_sets",
                schema: "POLLSDB");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "POLLSDB");

            migrationBuilder.DropTable(
                name: "poll",
                schema: "POLLSDB");

            migrationBuilder.DropTable(
                name: "politicians",
                schema: "POLLSDB");

            migrationBuilder.DropTable(
                name: "respondents",
                schema: "POLLSDB");

            migrationBuilder.DropTable(
                name: "terrtitory",
                schema: "POLLSDB");
        }
    }
}
