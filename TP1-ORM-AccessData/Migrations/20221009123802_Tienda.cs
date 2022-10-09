﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP1_ORM_AccessData.Migrations
{
    public partial class Tienda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Carrito",
                columns: table => new
                {
                    CarritoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrito", x => x.CarritoId);
                    table.ForeignKey(
                        name: "FK_Carrito_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarritoProducto",
                columns: table => new
                {
                    CarritoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoProducto", x => new { x.CarritoId, x.ProductoId });
                    table.ForeignKey(
                        name: "FK_CarritoProducto_Carrito_CarritoId",
                        column: x => x.CarritoId,
                        principalTable: "Carrito",
                        principalColumn: "CarritoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoProducto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orden",
                columns: table => new
                {
                    OrdenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarritoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orden", x => x.OrdenId);
                    table.ForeignKey(
                        name: "FK_Orden_Carrito_CarritoId",
                        column: x => x.CarritoId,
                        principalTable: "Carrito",
                        principalColumn: "CarritoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "ProductoId", "Codigo", "Descripcion", "Image", "Marca", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1, "20923", "Es una remera de mangas cortas, de caida recta, totalmente ideada para que luzcas nuestra marca con tu mejor potencial.", "https://www.billabong.com.ar/productos/remera-wave2/?variant=429183626", "Billabong", "Remera Wave", 5499m },
                    { 2, "20924", "Big Wave es una remera básica con estampa de marca de lado del frente. Novedad de esta temporada Winter22. Realizada con algodón sustentable", "https://www.billabong.com.ar/productos/remera-big-wave/?variant=419245581", "Billabong", "Remera Big Wave", 5999m },
                    { 3, "92364", " Remera Rip Curl. Relaxed Fit. Estampa frente y espalda. Estampa interna de marca y logo. Grifa logo. 100% Algodón. Jersey 16/1.", "https://www.ripcurlargentina.com/producto_detalle///remerarip_curl_crecent/23284.html", "Rip Curl", "Remera Rip Curl Crecent", 7999m },
                    { 4, "3084", "Reloj Rip Curl Detroit.CUADRANTE: 3 agujas. CARCASA: Acero inoxidable de calidad marina. Lente de cristal mineral Enchapado Iónico. Sumergible hasta 100m. Ancho:48mm. MALLA:Cuero", "https://www.ripcurlargentina.com/producto_detalle///relojrip_curl_detroit/7252.html", "Rip Curl", "Reloj Rip Curl Detroit", 69999m },
                    { 5, "3097", "Billetera Rip Curl 2 en 1.  100% cuero. Protección RFID. Nota gemela. Moneda con cremallera. Varias ranuras para tarjetas. Cartera delgada extraíble. Ventana de identificación.", "https://www.ripcurlargentina.com/producto_detalle///billeterarip_curl_2_en_1/12729.html", "Rip Curl", "Billetera Rip Curl 2 en 1", 8999m },
                    { 6, "3065", "Jogging Rip Curl Logo. Rustico invisible Jogging. Standard fit. Cintura y punos con elastico. Heattransfers logo. Cordon de ajuste al tono. Multietiquetas. 100% Algodon.", "https://www.ripcurlargentina.com/producto_detalle///joggingrip_curl_logo/23364.html", "Rip Curl", "Jogging Rip Curl Logo", 17999m },
                    { 7, "102034", "Remera manga corta Tela: Jersey vigore Fit: Regular Back de talle estampado Tela:100% Algodón", "https://www.quiksilver.com.ar/remera-mc-mirror-logo-neg-quiksilver", "Quiksilver", "Remera MC Mirror", 5999m },
                    { 8, "108115", "TELA: Algodón / Poliéster. Buzo bosa. Tela: Frisa pesada Vigore. Fit: Regular. Estampa en pecho", "https://www.quiksilver.com.ar/buzo-comp-logo-azul-quiksilver", "Quiksilver", "Buzo Comp Logo Azul", 13499m },
                    { 9, "112007", "Zapatilla Bota de caña baja realizada en cuero vacuno descarne y textil. Suela de eva inyectada para mayor confort", "https://www.quiksilver.com.ar/zapatillas-fujia-olv-quiksilver", "Quiksilver", "Zapatillas Fujia Olv", 25999m },
                    { 10, "92753", "El Fundamental es un buzo básico de escote redondo con una etiqueta grifa de Billabong en el pecho que traemos y mejoramos todas las temporadas. Ideal para usarlo todos los días y en todo momento.", "https://www.billabong.com.ar/productos/buzo-fundamental-crew/?variant=426790535", "Billabong", "Buzo Fundamental Crew", 12499m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carrito_ClienteId",
                table: "Carrito",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoProducto_ProductoId",
                table: "CarritoProducto",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_CarritoId",
                table: "Orden",
                column: "CarritoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoProducto");

            migrationBuilder.DropTable(
                name: "Orden");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Carrito");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
