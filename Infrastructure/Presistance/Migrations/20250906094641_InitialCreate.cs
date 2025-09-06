using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomsCount = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stars = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoomPictures",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPictures", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    NumOfBed = table.Column<int>(type: "int", nullable: false),
                    MaxOccupancy = table.Column<int>(type: "int", nullable: false),
                    RoomSize = table.Column<double>(type: "float", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotel_Pictures",
                columns: table => new
                {
                    HotelPicturesID = table.Column<int>(type: "int", nullable: false),
                    HotelsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel_Pictures", x => new { x.HotelPicturesID, x.HotelsID });
                    table.ForeignKey(
                        name: "FK_Hotel_Pictures_Hotels_HotelsID",
                        column: x => x.HotelsID,
                        principalTable: "Hotels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hotel_Pictures_RoomPictures_HotelPicturesID",
                        column: x => x.HotelPicturesID,
                        principalTable: "RoomPictures",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FristName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservations_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Room_Pictures",
                columns: table => new
                {
                    RoomPicturesID = table.Column<int>(type: "int", nullable: false),
                    RoomsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room_Pictures", x => new { x.RoomPicturesID, x.RoomsID });
                    table.ForeignKey(
                        name: "FK_Room_Pictures_RoomPictures_RoomPicturesID",
                        column: x => x.RoomPicturesID,
                        principalTable: "RoomPictures",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_Pictures_Rooms_RoomsID",
                        column: x => x.RoomsID,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationRooms",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRooms", x => new { x.ReservationId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_ReservationRooms_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "ID", "Address", "Description", "IsDeleted", "Name", "PictureUrl", "RoomsCount", "Stars" },
                values: new object[,]
                {
                    { 1, "Cairo, Egypt", null, false, "Cairo Grand Hotel", null, 0, 5m },
                    { 2, "Alexandria, Egypt", null, false, "Alexandria Beach Resort", null, 0, 4m },
                    { 3, "Luxor, Egypt", null, false, "Luxor Nile View", null, 0, 5m },
                    { 4, "Giza, Egypt", null, false, "Giza Pyramids Inn", null, 0, 3m },
                    { 5, "Sharm El Sheikh, Egypt", null, false, "Sharm El Sheikh Paradise", null, 0, 5m }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "ID", "HotelId", "IsAvailable", "IsDeleted", "MaxOccupancy", "NumOfBed", "PictureUrl", "Price", "RoomNumber", "RoomSize", "RoomType" },
                values: new object[,]
                {
                    { 1, 1, false, false, 2, 2, null, 150m, 0, 0.0, 0 },
                    { 2, 1, false, false, 3, 3, null, 100m, 0, 0.0, 0 },
                    { 3, 2, false, false, 2, 2, null, 200m, 0, 0.0, 0 },
                    { 4, 2, false, false, 4, 4, null, 250m, 0, 0.0, 0 },
                    { 5, 3, false, false, 4, 2, null, 300m, 0, 0.0, 0 },
                    { 6, 3, false, false, 4, 2, null, 120m, 0, 0.0, 0 },
                    { 7, 4, false, false, 2, 2, null, 180m, 0, 0.0, 0 },
                    { 8, 5, false, false, 2, 2, null, 400m, 0, 0.0, 0 },
                    { 9, 5, false, false, 2, 2, null, 90m, 0, 0.0, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_Pictures_HotelsID",
                table: "Hotel_Pictures",
                column: "HotelsID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRooms_RoomId",
                table: "ReservationRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_HotelId",
                table: "Reservations",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Pictures_RoomsID",
                table: "Room_Pictures",
                column: "RoomsID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Hotel_Pictures");

            migrationBuilder.DropTable(
                name: "ReservationRooms");

            migrationBuilder.DropTable(
                name: "Room_Pictures");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "RoomPictures");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
