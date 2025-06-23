using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStreamer.Infrastructure.EntityFrameworkProvider.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bandas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bandas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musicas_Bandas_BandaId",
                        column: x => x.BandaId,
                        principalTable: "Bandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assinaturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Plano = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAssinatura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comerciante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Autorizada = table.Column<bool>(type: "bit", nullable: false),
                    MotivoNegacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosBandasFavoritas",
                columns: table => new
                {
                    BandasFavoritasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FavoritadaPorUsuariosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosBandasFavoritas", x => new { x.BandasFavoritasId, x.FavoritadaPorUsuariosId });
                    table.ForeignKey(
                        name: "FK_UsuariosBandasFavoritas_Bandas_BandasFavoritasId",
                        column: x => x.BandasFavoritasId,
                        principalTable: "Bandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosBandasFavoritas_Usuarios_FavoritadaPorUsuariosId",
                        column: x => x.FavoritadaPorUsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosMusicasFavoritas",
                columns: table => new
                {
                    FavoritadaPorUsuariosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicasFavoritasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosMusicasFavoritas", x => new { x.FavoritadaPorUsuariosId, x.MusicasFavoritasId });
                    table.ForeignKey(
                        name: "FK_UsuariosMusicasFavoritas_Musicas_MusicasFavoritasId",
                        column: x => x.MusicasFavoritasId,
                        principalTable: "Musicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosMusicasFavoritas_Usuarios_FavoritadaPorUsuariosId",
                        column: x => x.FavoritadaPorUsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_UsuarioId",
                table: "Assinaturas",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musicas_BandaId",
                table: "Musicas",
                column: "BandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_UsuarioId",
                table: "Transacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosBandasFavoritas_FavoritadaPorUsuariosId",
                table: "UsuariosBandasFavoritas",
                column: "FavoritadaPorUsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosMusicasFavoritas_MusicasFavoritasId",
                table: "UsuariosMusicasFavoritas",
                column: "MusicasFavoritasId");

            var inserts = File.ReadAllText(Scripts.Scripts.InsertMusicasBandas);
            migrationBuilder.Sql(inserts);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assinaturas");

            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "UsuariosBandasFavoritas");

            migrationBuilder.DropTable(
                name: "UsuariosMusicasFavoritas");

            migrationBuilder.DropTable(
                name: "Musicas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Bandas");
        }
    }
}
