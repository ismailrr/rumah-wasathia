using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MvcRWV2.Data.Migrations
{
    public partial class RWMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KataSandi = table.Column<string>(maxLength: 50, nullable: false),
                    NamaPengguna = table.Column<string>(maxLength: 50, nullable: false),
                    RememberMe = table.Column<bool>(nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategori_Artikel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nama = table.Column<string>(maxLength: 200, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori_Artikel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategori_Buku",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nama = table.Column<string>(maxLength: 200, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori_Buku", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategori_Kajian",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nama = table.Column<string>(maxLength: 200, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori_Kajian", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kategori_Konsultasi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nama = table.Column<string>(maxLength: 200, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori_Konsultasi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KategoriGaleri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nama = table.Column<string>(maxLength: 200, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategoriGaleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Path_Artikel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Path_Artikel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Path_Buku",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Path_Buku", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Path_Galeri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Path_Galeri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Path_Kajian_Audio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Path_Kajian_Audio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Path_Kajian_Video",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Path_Kajian_Video", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Path_Konsultasi_E_Paper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Path_Konsultasi_E_Paper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Path_Konsultasi_Infografis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Path_Konsultasi_Infografis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PathKonsultasiRumahWasathia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 500, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathKonsultasiRumahWasathia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Konsultasi_Republika",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Judul = table.Column<string>(maxLength: 200, nullable: false),
                    KategoriId = table.Column<int>(nullable: true),
                    Link = table.Column<string>(maxLength: 500, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konsultasi_Republika", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Konsultasi_Republika_Kategori_Konsultasi_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori_Konsultasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Artikel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Judul = table.Column<string>(maxLength: 200, nullable: false),
                    KategoriId = table.Column<int>(nullable: true),
                    PathId = table.Column<int>(nullable: true),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikel_Kategori_Artikel_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori_Artikel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Artikel_Path_Artikel_PathId",
                        column: x => x.PathId,
                        principalTable: "Path_Artikel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Buku",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Deskripsi = table.Column<string>(maxLength: 5000, nullable: true),
                    ISBN = table.Column<string>(maxLength: 50, nullable: true),
                    Judul = table.Column<string>(maxLength: 200, nullable: false),
                    KategoriId = table.Column<int>(nullable: true),
                    PathId = table.Column<int>(nullable: true),
                    Penulis = table.Column<string>(maxLength: 200, nullable: true),
                    Tanggal = table.Column<DateTime>(nullable: false),
                    Tebal = table.Column<int>(nullable: false),
                    Terbitan = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buku_Kategori_Buku_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori_Buku",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buku_Path_Buku_PathId",
                        column: x => x.PathId,
                        principalTable: "Path_Buku",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Galeri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Judul = table.Column<string>(maxLength: 200, nullable: false),
                    KategoriId = table.Column<int>(nullable: true),
                    PathId = table.Column<int>(nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galeri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Galeri_KategoriGaleri_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "KategoriGaleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Galeri_Path_Galeri_PathId",
                        column: x => x.PathId,
                        principalTable: "Path_Galeri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kajian_Audio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KategoriId = table.Column<int>(nullable: true),
                    Link = table.Column<string>(maxLength: 500, nullable: false),
                    PathId = table.Column<int>(nullable: true),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kajian_Audio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kajian_Audio_Kategori_Kajian_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori_Kajian",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kajian_Audio_Path_Kajian_Audio_PathId",
                        column: x => x.PathId,
                        principalTable: "Path_Kajian_Audio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kajian_Video",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KategoriId = table.Column<int>(nullable: true),
                    Link = table.Column<string>(maxLength: 500, nullable: false),
                    PathId = table.Column<int>(nullable: true),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kajian_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kajian_Video_Kategori_Kajian_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori_Kajian",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kajian_Video_Path_Kajian_Video_PathId",
                        column: x => x.PathId,
                        principalTable: "Path_Kajian_Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Konsultasi_EPaper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Judul = table.Column<string>(maxLength: 200, nullable: false),
                    KategoriId = table.Column<int>(nullable: true),
                    PathId = table.Column<int>(nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konsultasi_EPaper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Konsultasi_EPaper_Kategori_Konsultasi_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori_Konsultasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konsultasi_EPaper_Path_Konsultasi_E_Paper_PathId",
                        column: x => x.PathId,
                        principalTable: "Path_Konsultasi_E_Paper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Konsultasi_Infografis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Judul = table.Column<string>(maxLength: 200, nullable: false),
                    KategoriId = table.Column<int>(nullable: true),
                    PathId = table.Column<int>(nullable: true),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konsultasi_Infografis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Konsultasi_Infografis_Kategori_Konsultasi_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori_Konsultasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konsultasi_Infografis_Path_Konsultasi_Infografis_PathId",
                        column: x => x.PathId,
                        principalTable: "Path_Konsultasi_Infografis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Konsultasi_Rumah_Wasathia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Jawaban = table.Column<string>(maxLength: 5000, nullable: false),
                    Judul = table.Column<string>(maxLength: 200, nullable: false),
                    KategoriId = table.Column<int>(nullable: true),
                    PathId = table.Column<int>(nullable: true),
                    Penulis = table.Column<string>(maxLength: 200, nullable: true),
                    Pertanyaan = table.Column<string>(maxLength: 5000, nullable: false),
                    Tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konsultasi_Rumah_Wasathia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Konsultasi_Rumah_Wasathia_Kategori_Konsultasi_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori_Konsultasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Konsultasi_Rumah_Wasathia_PathKonsultasiRumahWasathia_PathId",
                        column: x => x.PathId,
                        principalTable: "PathKonsultasiRumahWasathia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Artikel_KategoriId",
                table: "Artikel",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Artikel_PathId",
                table: "Artikel",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_Buku_KategoriId",
                table: "Buku",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Buku_PathId",
                table: "Buku",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_Galeri_KategoriId",
                table: "Galeri",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Galeri_PathId",
                table: "Galeri",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_Kajian_Audio_KategoriId",
                table: "Kajian_Audio",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Kajian_Audio_PathId",
                table: "Kajian_Audio",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_Kajian_Video_KategoriId",
                table: "Kajian_Video",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Kajian_Video_PathId",
                table: "Kajian_Video",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_Konsultasi_EPaper_KategoriId",
                table: "Konsultasi_EPaper",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Konsultasi_EPaper_PathId",
                table: "Konsultasi_EPaper",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_Konsultasi_Infografis_KategoriId",
                table: "Konsultasi_Infografis",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Konsultasi_Infografis_PathId",
                table: "Konsultasi_Infografis",
                column: "PathId");

            migrationBuilder.CreateIndex(
                name: "IX_Konsultasi_Republika_KategoriId",
                table: "Konsultasi_Republika",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Konsultasi_Rumah_Wasathia_KategoriId",
                table: "Konsultasi_Rumah_Wasathia",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_Konsultasi_Rumah_Wasathia_PathId",
                table: "Konsultasi_Rumah_Wasathia",
                column: "PathId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Artikel");

            migrationBuilder.DropTable(
                name: "Buku");

            migrationBuilder.DropTable(
                name: "Galeri");

            migrationBuilder.DropTable(
                name: "Kajian_Audio");

            migrationBuilder.DropTable(
                name: "Kajian_Video");

            migrationBuilder.DropTable(
                name: "Konsultasi_EPaper");

            migrationBuilder.DropTable(
                name: "Konsultasi_Infografis");

            migrationBuilder.DropTable(
                name: "Konsultasi_Republika");

            migrationBuilder.DropTable(
                name: "Konsultasi_Rumah_Wasathia");

            migrationBuilder.DropTable(
                name: "Kategori_Artikel");

            migrationBuilder.DropTable(
                name: "Path_Artikel");

            migrationBuilder.DropTable(
                name: "Kategori_Buku");

            migrationBuilder.DropTable(
                name: "Path_Buku");

            migrationBuilder.DropTable(
                name: "KategoriGaleri");

            migrationBuilder.DropTable(
                name: "Path_Galeri");

            migrationBuilder.DropTable(
                name: "Path_Kajian_Audio");

            migrationBuilder.DropTable(
                name: "Kategori_Kajian");

            migrationBuilder.DropTable(
                name: "Path_Kajian_Video");

            migrationBuilder.DropTable(
                name: "Path_Konsultasi_E_Paper");

            migrationBuilder.DropTable(
                name: "Path_Konsultasi_Infografis");

            migrationBuilder.DropTable(
                name: "Kategori_Konsultasi");

            migrationBuilder.DropTable(
                name: "PathKonsultasiRumahWasathia");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
