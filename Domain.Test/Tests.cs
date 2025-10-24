using Dominio.Entities;//class Producto
using Dominio.Enums;//class Categoria
using Dominio.ValueObjects;//Importa la class Precio

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
            var precio = new Precio(101);//Nuevo Objeto con valor 100
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
}