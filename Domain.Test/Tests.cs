using Dominio.Entities;//class Producto
using Dominio.Enums;//class Categoria
using Dominio.Eventos;
using Dominio.Interfaces;
using Dominio.Servicios;
using Dominio.ValueObjects;//Importa la class Precio
using Moq;

namespace Domain.Test
{
    public class Tests
    {
        [Fact]//Indica que es una prueba unitaria
        public void Precio_Negativo_LanzaExcepcion()
        {
            Assert.Throws<ArgumentException>(() => new Precio(-1));//Valida si lanza una excepcion
        }

        [Fact]
        public void Precio_Valido_SeCreaCorrectamente()
        {
            var precio = new Precio(100);//Nuevo Objeto con valor 100
            Assert.Equal(100, precio.Valor);//Verifica la propiedad Valor
        }
    }

    public class PrecioToStringTests
    {
        [Fact]
        public void Precio_ToString_DevuelveFormatoCorrecto()
        {
            // Arrange
            var precio = new Precio(1234.56m);

            // Act
            var resultado = precio.ToString();

            // Assert
            Assert.Equal("$1,234.56", resultado);
        }
    }

    public class ProductoTests
    {
        [Fact]
        public void Producto_Valido_SeCreaCorrectamente()
        {
            // Arrange
            var precio = new Precio(499.99m);
            var producto = new Producto
            {
                Id = 1,
                Nombre = "Audífonos Bluetooth",
                Precio = precio,
                Categoria = CategoriaProducto.Electronica
            };

            // Assert
            Assert.Equal(1, producto.Id);
            Assert.Equal("Audífonos Bluetooth", producto.Nombre);
            Assert.Equal(499.99m, producto.Precio.Valor);
            Assert.Equal(CategoriaProducto.Electronica, producto.Categoria);
        }
    }

    public class ProductoTestsnegativo
    {
        [Fact]
        public void Producto_PrecioNegativo_LanzaExcepcion()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>//Throws fuerza la excepcion, es decir, si al hacer debug arroja alguna excepcion en la consola aparece como Prueba Correcta, en caso de no mandar excepcion la Prueba Falla
            {
                var producto = new Producto
                {
                    Id = 2,
                    Nombre = "Cámara falsa",
                    Precio = new Precio(-50), // Esto lanza la excepción
                    Categoria = CategoriaProducto.Electronica
                };
            });
        }
    }

    public class CarritoDeComprasTests
    {
        [Fact]
        public void Carrito_AgregarProductos_CalculaTotalCorrectamente()
        {
            // Arrange
            var carrito = new CarritoDeCompras();

            var producto1 = new Producto
            {
                Id = 1,
                Nombre = "Mouse",
                Precio = new Precio(250),
                Categoria = CategoriaProducto.Electronica
            };

            var producto2 = new Producto
            {
                Id = 2,
                Nombre = "Teclado",
                Precio = new Precio(450),
                Categoria = CategoriaProducto.Electronica
            };

            // Act
            carrito.AgregarProducto(producto1);
            carrito.AgregarProducto(producto2);
            var total = carrito.CalcularTotal();

            // Assert
            Assert.Equal(2, carrito.CantidadProductos);
            Assert.Equal(700, total);
        }
    }

    public class ProductoServiceTests
    {
        [Fact]
        public async Task ObtenerProducto_ProductoExiste_DevuelveProducto()
        {
            // Arrange
            var productoEsperado = new Producto
            {
                Id = 1,
                Nombre = "Laptop",
                Precio = new Precio(15000),
                Categoria = CategoriaProducto.Electronica
            };

            var mockRepo = new Mock<IProductoRepository>();
            mockRepo.Setup(r => r.ObtenerPorIdAsync(1))
                    .ReturnsAsync(productoEsperado);

            var servicio = new ProductoService(mockRepo.Object);

            // Act
            var resultado = await servicio.ObtenerProducto(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Laptop", resultado!.Nombre);
            Assert.Equal(15000, resultado.Precio.Valor);
        }
    }

    public class ServicioPromocionTests
    {
        [Fact]
        public async Task AplicarDescuentoVip_ClienteEsVip_AplicaDescuento()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Nombre = "Juan", EsVip = true };

            var productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Laptop", Categoria = CategoriaProducto.Electronica, Precio = new Precio(1000) },
            new Producto { Id = 2, Nombre = "Mouse", Categoria = CategoriaProducto.Electronica, Precio = new Precio(100) },
            new Producto { Id = 3, Nombre = "Camisa", Categoria = CategoriaProducto.Ropa, Precio = new Precio(50) }
        };

            var mockClienteRepo = new Mock<IClienteRepository>();
            mockClienteRepo.Setup(r => r.ObtenerPorIdAsync(1)).ReturnsAsync(cliente);

            var mockProductoRepo = new Mock<IProductoRepository>();
            mockProductoRepo.Setup(r => r.ObtenerTodosAsync()).ReturnsAsync(productos);
            mockProductoRepo.Setup(r => r.ActualizarAsync(It.IsAny<Producto>())).Returns(Task.CompletedTask);

            var mockPublisher = new Mock<IEventPublisher>();

            var servicio = new ServicioPromocion(mockClienteRepo.Object, mockProductoRepo.Object, mockPublisher.Object);

            // Act
            await servicio.AplicarDescuentoVip(1, CategoriaProducto.Electronica, 0.1m);

            // Assert
            Assert.Equal(900, productos[0].Precio.Valor);
            Assert.Equal(90, productos[1].Precio.Valor);
            Assert.Equal(50, productos[2].Precio.Valor); // No cambia, categoría distinta

            mockProductoRepo.Verify(r => r.ActualizarAsync(productos[0]), Times.Once);
            mockProductoRepo.Verify(r => r.ActualizarAsync(productos[1]), Times.Once);
            mockProductoRepo.Verify(r => r.ActualizarAsync(productos[2]), Times.Never);

            //mockPublisher.Verify(p => p.Publicar(It.Is<ProductoActualizadoEvent>(e => e.ProductoId == 1 && e.NuevoPrecio == 900)), Times.Once);//SE AGREGO EL EVENTO
            mockPublisher.Verify(p => p.PublicarAsync(It.Is<ProductoActualizadoEvent>(e => e.ProductoId == 1 && e.NuevoPrecio == 900)), Times.Once);//SE AGREGO EL EVENTO
        }
    }

    public class ProductoActualizadoEventTests
    {
        [Fact]
        public void Evento_SeCreaCorrectamente()
        {
            var evento = new ProductoActualizadoEvent(1, 899.99m);

            Assert.Equal(1, evento.ProductoId);
            Assert.Equal(899.99m, evento.NuevoPrecio);
            Assert.True(evento.Fecha <= DateTime.UtcNow);
        }
    }
}