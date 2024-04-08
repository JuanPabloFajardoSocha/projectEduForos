using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eduForos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    idCourse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    create_at = table.Column<DateTime>(type: "date", nullable: true),
                    update_at = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.idCourse);
                });

            migrationBuilder.CreateTable(
                name: "subject",
                columns: table => new
                {
                    idSubject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    create_at = table.Column<DateTime>(type: "date", nullable: true),
                    update_at = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subject", x => x.idSubject);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userDocumentType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    userDocument = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UrlPhoto = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    AssetId = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    firtsName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    surName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    age = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    telephone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    institutionalEmail = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    personalEmail = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    userType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    profession = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    create_at = table.Column<DateTime>(type: "date", nullable: true),
                    update_at = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_1", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "courseSubject",
                columns: table => new
                {
                    idCourseSubject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCourse = table.Column<int>(type: "int", nullable: false),
                    idSubject = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseSubject", x => x.idCourseSubject);
                    table.ForeignKey(
                        name: "FK_courseSubject_course",
                        column: x => x.idCourse,
                        principalTable: "course",
                        principalColumn: "idCourse");
                    table.ForeignKey(
                        name: "FK_courseSubject_subject",
                        column: x => x.idSubject,
                        principalTable: "subject",
                        principalColumn: "idSubject");
                });

            migrationBuilder.CreateTable(
                name: "forum",
                columns: table => new
                {
                    idForum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    startDate = table.Column<DateTime>(type: "date", nullable: false),
                    endDate = table.Column<DateTime>(type: "date", nullable: false),
                    isGradable = table.Column<int>(type: "int", nullable: false),
                    UrlPhoto = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    AssetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idCourse = table.Column<int>(type: "int", nullable: false),
                    idSubject = table.Column<int>(type: "int", nullable: false),
                    create_at = table.Column<DateTime>(type: "date", nullable: true),
                    update_at = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forum", x => x.idForum);
                    table.ForeignKey(
                        name: "FK_forum_course",
                        column: x => x.idCourse,
                        principalTable: "course",
                        principalColumn: "idCourse");
                    table.ForeignKey(
                        name: "FK_forum_subject",
                        column: x => x.idSubject,
                        principalTable: "subject",
                        principalColumn: "idSubject");
                });

            migrationBuilder.CreateTable(
                name: "videoConference",
                columns: table => new
                {
                    idVideoConference = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    idCourse = table.Column<int>(type: "int", nullable: false),
                    idSubject = table.Column<int>(type: "int", nullable: false),
                    link = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    startDate = table.Column<DateTime>(type: "date", nullable: false),
                    endDate = table.Column<DateTime>(type: "date", nullable: false),
                    create_at = table.Column<DateTime>(type: "date", nullable: true),
                    update_at = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_videoConference", x => x.idVideoConference);
                    table.ForeignKey(
                        name: "FK_videoConference_course",
                        column: x => x.idCourse,
                        principalTable: "course",
                        principalColumn: "idCourse");
                    table.ForeignKey(
                        name: "FK_videoConference_subject",
                        column: x => x.idSubject,
                        principalTable: "subject",
                        principalColumn: "idSubject");
                });

            migrationBuilder.CreateTable(
                name: "userCourse",
                columns: table => new
                {
                    idUserCourse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idCourse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userCourse", x => x.idUserCourse);
                    table.ForeignKey(
                        name: "FK_userCourse_course",
                        column: x => x.idCourse,
                        principalTable: "course",
                        principalColumn: "idCourse");
                    table.ForeignKey(
                        name: "FK_userCourse_user",
                        column: x => x.idUser,
                        principalTable: "user",
                        principalColumn: "idUser");
                });

            migrationBuilder.CreateTable(
                name: "userSubject",
                columns: table => new
                {
                    idUserSubject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idSubject = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userSubject", x => x.idUserSubject);
                    table.ForeignKey(
                        name: "FK_userSubject_subject",
                        column: x => x.idSubject,
                        principalTable: "subject",
                        principalColumn: "idSubject");
                    table.ForeignKey(
                        name: "FK_userSubject_user",
                        column: x => x.idUser,
                        principalTable: "user",
                        principalColumn: "idUser");
                });

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    idMessage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    route = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    calification = table.Column<int>(type: "int", nullable: true),
                    idForum = table.Column<int>(type: "int", nullable: false),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message", x => x.idMessage);
                    table.ForeignKey(
                        name: "FK_message_forum",
                        column: x => x.idForum,
                        principalTable: "forum",
                        principalColumn: "idForum");
                    table.ForeignKey(
                        name: "FK_message_user",
                        column: x => x.idUser,
                        principalTable: "user",
                        principalColumn: "idUser");
                });

            migrationBuilder.CreateTable(
                name: "answer",
                columns: table => new
                {
                    idAnswer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    route = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: true),
                    idMessage = table.Column<int>(type: "int", nullable: true),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer", x => x.idAnswer);
                    table.ForeignKey(
                        name: "FK_answer_message",
                        column: x => x.idMessage,
                        principalTable: "message",
                        principalColumn: "idMessage");
                    table.ForeignKey(
                        name: "FK_answer_user",
                        column: x => x.idUser,
                        principalTable: "user",
                        principalColumn: "idUser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_answer_idMessage",
                table: "answer",
                column: "idMessage");

            migrationBuilder.CreateIndex(
                name: "IX_answer_idUser",
                table: "answer",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_courseSubject_idCourse",
                table: "courseSubject",
                column: "idCourse");

            migrationBuilder.CreateIndex(
                name: "IX_courseSubject_idSubject",
                table: "courseSubject",
                column: "idSubject");

            migrationBuilder.CreateIndex(
                name: "IX_forum_idCourse",
                table: "forum",
                column: "idCourse");

            migrationBuilder.CreateIndex(
                name: "IX_forum_idSubject",
                table: "forum",
                column: "idSubject");

            migrationBuilder.CreateIndex(
                name: "IX_message_idForum",
                table: "message",
                column: "idForum");

            migrationBuilder.CreateIndex(
                name: "IX_message_idUser",
                table: "message",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_userCourse_idCourse",
                table: "userCourse",
                column: "idCourse");

            migrationBuilder.CreateIndex(
                name: "IX_userCourse_idUser",
                table: "userCourse",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_userSubject_idSubject",
                table: "userSubject",
                column: "idSubject");

            migrationBuilder.CreateIndex(
                name: "IX_userSubject_idUser",
                table: "userSubject",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_videoConference_idCourse",
                table: "videoConference",
                column: "idCourse");

            migrationBuilder.CreateIndex(
                name: "IX_videoConference_idSubject",
                table: "videoConference",
                column: "idSubject");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answer");

            migrationBuilder.DropTable(
                name: "courseSubject");

            migrationBuilder.DropTable(
                name: "userCourse");

            migrationBuilder.DropTable(
                name: "userSubject");

            migrationBuilder.DropTable(
                name: "videoConference");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "forum");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "subject");
        }
    }
}
