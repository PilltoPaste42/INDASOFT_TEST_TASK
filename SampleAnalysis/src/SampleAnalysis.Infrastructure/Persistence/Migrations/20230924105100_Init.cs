using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleAnalysis.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventFrameTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFrameTypes_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventFrames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EventFrameTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFrames_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK__EventFram__Event__276EDEB3",
                        column: x => x.EventFrameTypeId,
                        principalTable: "EventFrameTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventFrameTypeValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    EventFrameTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFrameTypeValues_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK__EventFram__Event__38996AB5",
                        column: x => x.EventFrameTypeId,
                        principalTable: "EventFrameTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ParentEventFrameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentEventFrameTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChildEventFrameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChildEventFrameTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Links__ChildEven__4BAC3F29",
                        column: x => x.ChildEventFrameId,
                        principalTable: "EventFrames",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Links__ChildEven__4D94879B",
                        column: x => x.ChildEventFrameTypeId,
                        principalTable: "EventFrameTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Links__ParentEve__4AB81AF0",
                        column: x => x.ParentEventFrameId,
                        principalTable: "EventFrames",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Links__ParentEve__4CA06362",
                        column: x => x.ParentEventFrameTypeId,
                        principalTable: "EventFrameTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventFrameValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    EventFrameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserfieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ValueInt = table.Column<int>(type: "int", nullable: true),
                    ValueFloat = table.Column<double>(type: "float", nullable: true),
                    ValueDatetime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFrameValues_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK__EventFram__Event__36B12243",
                        column: x => x.EventFrameId,
                        principalTable: "EventFrames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__EventFram__Userf__37A5467C",
                        column: x => x.UserfieldId,
                        principalTable: "EventFrameTypeValues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventFrames_EventFrameTypeId",
                table: "EventFrames",
                column: "EventFrameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventFrameTypeValues_EventFrameTypeId",
                table: "EventFrameTypeValues",
                column: "EventFrameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventFrameValues_EventFrameId",
                table: "EventFrameValues",
                column: "EventFrameId");

            migrationBuilder.CreateIndex(
                name: "IX_EventFrameValues_UserfieldId",
                table: "EventFrameValues",
                column: "UserfieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_ChildEventFrameId",
                table: "Links",
                column: "ChildEventFrameId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_ChildEventFrameTypeId",
                table: "Links",
                column: "ChildEventFrameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_ParentEventFrameId",
                table: "Links",
                column: "ParentEventFrameId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_ParentEventFrameTypeId",
                table: "Links",
                column: "ParentEventFrameTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventFrameValues");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "EventFrameTypeValues");

            migrationBuilder.DropTable(
                name: "EventFrames");

            migrationBuilder.DropTable(
                name: "EventFrameTypes");
        }
    }
}
