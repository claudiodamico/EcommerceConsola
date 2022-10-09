using Microsoft.EntityFrameworkCore;
using TP1_ORM_AccessData.Entities;

namespace TP1_ORM_AccessData.Data
{
    public class TiendaDbContext : DbContext
    {
        public TiendaDbContext() { }

        public TiendaDbContext(DbContextOptions<TiendaDbContext> options)
            : base(options) { }

        public virtual DbSet<Carrito> Carritos { get; set; } = null!;
        public virtual DbSet<CarritoProducto> CarritoProductos { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Orden> Ordenes { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=DbTiendaClaudio;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrito>(entity =>
            {
                entity.ToTable("Carrito");

                entity.Property(e => e.CarritoId).ValueGeneratedNever();

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Carritos)
                    .HasForeignKey(d => d.ClienteId);
            });

            modelBuilder.Entity<CarritoProducto>(entity =>
            {
                entity.HasKey(e => new { e.CarritoId, e.ProductoId });

                entity.ToTable("CarritoProducto");

                entity.HasOne(d => d.Carrito)
                    .WithMany(p => p.CarritoProductos)
                    .HasForeignKey(d => d.CarritoId);

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.CarritoProductos)
                    .HasForeignKey(d => d.ProductoId);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Apellido).HasMaxLength(25);

                entity.Property(e => e.Dni)
                    .HasMaxLength(10)
                    .HasColumnName("DNI");

                entity.Property(e => e.Nombre).HasMaxLength(25);

                entity.Property(e => e.Telefono).HasMaxLength(13);
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.ToTable("Orden");

                entity.HasIndex(e => e.CarritoId)
                    .IsUnique();

                entity.Property(e => e.OrdenId).ValueGeneratedNever();

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Carrito)
                    .WithOne(p => p.Orden)
                    .HasForeignKey<Orden>(d => d.CarritoId);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.Property(e => e.ProductoId);

                entity.Property(e => e.Codigo).HasMaxLength(25);

                entity.Property(e => e.Marca).HasMaxLength(25);

                entity.Property(e => e.Precio).HasColumnType("decimal(15, 2)");
            });

            //INSERTS

            modelBuilder.Entity<Producto>().HasData(
            new Producto
            {
                ProductoId = 1,
                Nombre = "Remera Wave",
                Descripcion = "Es una remera de mangas cortas, de caida recta, totalmente ideada para que luzcas nuestra marca con tu mejor potencial.",
                Marca = "Billabong",
                Codigo = "20923",
                Precio = 5499,
                Image = "https://www.billabong.com.ar/productos/remera-wave2/?variant=429183626",
            },
            new Producto
            {
                ProductoId = 2,
                Nombre = "Remera Big Wave",
                Descripcion = "Big Wave es una remera básica con estampa de marca de lado del frente. Novedad de esta temporada Winter22. Realizada con algodón sustentable",
                Marca = "Billabong",
                Codigo = "20924",
                Precio = 5999,
                Image = "https://www.billabong.com.ar/productos/remera-big-wave/?variant=419245581",
            },
            new Producto
            {
                ProductoId = 3,
                Nombre = "Remera Rip Curl Crecent",
                Descripcion = " Remera Rip Curl. Relaxed Fit. Estampa frente y espalda. Estampa interna de marca y logo. Grifa logo. 100% Algodón. Jersey 16/1.",
                Marca = "Rip Curl",
                Codigo = "92364",
                Precio = 7999,
                Image = "https://www.ripcurlargentina.com/producto_detalle///remerarip_curl_crecent/23284.html",
            },
            new Producto
            {
                ProductoId = 4,
                Nombre = "Reloj Rip Curl Detroit",
                Descripcion = "Reloj Rip Curl Detroit.CUADRANTE: 3 agujas. CARCASA: Acero inoxidable de calidad marina. Lente de cristal mineral Enchapado Iónico. Sumergible hasta 100m. Ancho:48mm. MALLA:Cuero",
                Marca = "Rip Curl",
                Codigo = "3084",
                Precio = 69999,
                Image = "https://www.ripcurlargentina.com/producto_detalle///relojrip_curl_detroit/7252.html",
            },
            new Producto
            {
                ProductoId = 5,
                Nombre = "Billetera Rip Curl 2 en 1",
                Descripcion = "Billetera Rip Curl 2 en 1.  100% cuero. Protección RFID. Nota gemela. Moneda con cremallera. Varias ranuras para tarjetas. Cartera delgada extraíble. Ventana de identificación.",
                Marca = "Rip Curl",
                Codigo = "3097",
                Precio = 8999,
                Image = "https://www.ripcurlargentina.com/producto_detalle///billeterarip_curl_2_en_1/12729.html",
            },
            new Producto
            {
                ProductoId = 6,
                Nombre = "Jogging Rip Curl Logo",
                Descripcion = "Jogging Rip Curl Logo. Rustico invisible Jogging. Standard fit. Cintura y punos con elastico. Heattransfers logo. Cordon de ajuste al tono. Multietiquetas. 100% Algodon.",
                Marca = "Rip Curl",
                Codigo = "3065",
                Precio = 17999,
                Image = "https://www.ripcurlargentina.com/producto_detalle///joggingrip_curl_logo/23364.html",
            },
            new Producto
            {
                ProductoId = 7,
                Nombre = "Remera MC Mirror",
                Descripcion = "Remera manga corta Tela: Jersey vigore Fit: Regular Back de talle estampado Tela:100% Algodón",
                Marca = "Quiksilver",
                Codigo = "102034",
                Precio = 5999,
                Image = "https://www.quiksilver.com.ar/remera-mc-mirror-logo-neg-quiksilver",
            },
            new Producto
            {
                ProductoId = 8,
                Nombre = "Buzo Comp Logo Azul",
                Descripcion = "TELA: Algodón / Poliéster. Buzo bosa. Tela: Frisa pesada Vigore. Fit: Regular. Estampa en pecho",
                Marca = "Quiksilver",
                Codigo = "108115",
                Precio = 13499,
                Image = "https://www.quiksilver.com.ar/buzo-comp-logo-azul-quiksilver",
            },
            new Producto
            {
                ProductoId = 9,
                Nombre = "Zapatillas Fujia Olv",
                Descripcion = "Zapatilla Bota de caña baja realizada en cuero vacuno descarne y textil. Suela de eva inyectada para mayor confort",
                Marca = "Quiksilver",
                Codigo = "112007",
                Precio = 25999,
                Image = "https://www.quiksilver.com.ar/zapatillas-fujia-olv-quiksilver",
            },
            new Producto
            {
                ProductoId = 10,
                Nombre = "Buzo Fundamental Crew",
                Descripcion = "El Fundamental es un buzo básico de escote redondo con una etiqueta grifa de Billabong en el pecho que traemos y mejoramos todas las temporadas. Ideal para usarlo todos los días y en todo momento.",
                Marca = "Billabong",
                Codigo = "92753",
                Precio = 12499,
                Image = "https://www.billabong.com.ar/productos/buzo-fundamental-crew/?variant=426790535",
            });
        }
    }
}