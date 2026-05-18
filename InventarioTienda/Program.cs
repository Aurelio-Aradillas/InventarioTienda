using System;
using System.Collections.Generic;
using System.Security.Authentication;

List<Producto> inventario = new List<Producto>(); // Lista para almacenar los productos en el inventario
bool salir = false;

while (!salir)
{
    Console.Clear();
    Console.WriteLine("==============================");
    Console.WriteLine("   Inventario de la Tienda   ");
    Console.WriteLine("==============================");
    Console.WriteLine("1. Agregar producto");
    Console.WriteLine("2. Actualizar inventario");
    Console.WriteLine("3. Consultar inventario");
    Console.WriteLine("4. Salir");
    Console.WriteLine("==============================");
    Console.WriteLine("Seleccione una opción: ");

    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            AgregarProducto();
            break;
        case "2":
            ActualizarInventario();
            break;
        case "3":
            ConsultarInventario();
            break;
        case "4":
            salir = true;
            Console.WriteLine("\nSaliendo del sistema...");
            break;
        default:
            Console.WriteLine("\n Opción no válida. Presione ENTER para intentar de nuevo.");
            Console.ReadLine();
            break;
    }
}

void AgregarProducto()
{
    Console.Clear();
    Console.WriteLine("--- REGISTRAR NUEVO PRODUCTO ---");

    Console.Write("Ingrese el nombre del producto: ");
    string nombre = Console.ReadLine();
    // Validación: que el nombre no sea espacios en blanco
    if (string.IsNullOrEmpty(nombre))
    {
        Console.WriteLine("\nError: El nombre no puede estar vacío");
        Console.ReadLine();
        return; // Rompe la función y regresa al menú
    }

    try
    {
        //Pedir y validar stock
        Console.WriteLine("Ingrese la cantidad inicial en inventario: ");
        int cantidad = int.Parse(Console.ReadLine());

        if (cantidad < 0)
        {
            Console.WriteLine("\nError: La cantidad no puede ser negativa");
            Console.ReadLine();
            return;
        }

        //Pedir y validar precio
        Console.WriteLine("Ingrese el precio del producto: ");
        decimal precio = decimal.Parse(Console.ReadLine());

        if (precio <= 0)
        {
            Console.WriteLine("\nError: El precio debe ser mayor a cero");
            Console.ReadLine();
            return;
        }

        // Crear el producto y agregarlo al inventario
        Producto nuevo = new Producto(nombre, cantidad, precio);
        inventario.Add(nuevo);

        Console.WriteLine($"\nProducto '{nombre}' registrado con exito");

    }
    catch (FormatException)
    {
        // Captura cualquier error de formato (por ejemplo, si el usuario ingresa texto en lugar de números)
        Console.WriteLine($"\nError: Formato incorrecto. En inventario y precio debe ingresar números");
    }

    Console.WriteLine("\nPresione ENTER para regresar al menú principal");
    Console.ReadLine();
}

void ConsultarInventario()
{
    Console.Clear();
    Console.WriteLine("--- ESTADO DEL INVENTARIO ---");

    if (inventario.Count == 0)
    {
        Console.WriteLine("El inventario está vacío.");
    }
    else
    {
        // Mostrar encabezados de la tabla
        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine("{0,-20} {1,-10} {2,-10}", "Nombre", "Cantidad", "Precio");
        Console.WriteLine("\n------------------------------------------------");

        //Ciclo foreach para mostrar cada producto en el inventario
        foreach (Producto p in inventario)
        {
            // {0,-20} alinea el texto a la izquierda dejando 20 espacios (como columnas)
            // :C2 le da formato de moneda (Currency) al precio
            Console.WriteLine("{0,-20} {1,-10} {2,-10:C}", p.Nombre, p.Cantidad, p.Precio);
        }
        Console.WriteLine("------------------------------------------------");
    }
    Console.WriteLine("\nPresione ENTER para regresar al menú principal");
    Console.ReadLine();
}

void ActualizarInventario()
{
    Console.Clear();
    Console.WriteLine("--- ACTUALIZAR INVENTARIO ---");

    Console.Write("Ingrese el nombre del producto que busca: ");
    string nombreBuscar = Console.ReadLine();

    Producto productoEncontrado = null;

    // Buscar el producto en el inventario
    foreach (Producto p in inventario)
    {
        //OrdinalIgnoreCase ignora mayúsculas y minúsculas al comparar los nombres
        if (p.Nombre.Equals(nombreBuscar, StringComparison.OrdinalIgnoreCase))
        {
            productoEncontrado = p;
            break; // Rompe el ciclo una vez que encuentra el producto
        }
    }
    //Si la variable sigue vacia, significa que no existe el producto en el inventario
    if (productoEncontrado == null)
    {
        Console.WriteLine("\nProducto no encontrado en el inventario.");
        Console.ReadLine();
        return;
    }

    Console.WriteLine($"\nProducto encontrado: {productoEncontrado.Nombre}");
    Console.WriteLine($"Cantidad actual: {productoEncontrado.Cantidad}");

    try
    {
        Console.WriteLine("Ingrese la nueva cantidad en inventario: ");
        int nuevaCantidad = int.Parse(Console.ReadLine());

        if (nuevaCantidad < 0)
        {
            Console.WriteLine("\nError: La cantidad no puede ser negativa");
        }
        else
        {
            // Modificamos el valor usando el 'set' de la propiedad Cantidad
            productoEncontrado.Cantidad = nuevaCantidad;
            Console.WriteLine("\nInventario actualizado con éxito");
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("\nError: Formato incorrecto. La cantidad debe ser un número entero.");
    }

    Console.WriteLine("\nPresione ENTER para regresar al menú principal");
    Console.ReadLine();
}

class Producto
{
    //Propiedades del producto con sus permisos de lectura (get) y escritura (set)
    public string Nombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    //Constructor: permite crear y llenar el producto en una sola linea
    public Producto(string nombre, int cantidad, decimal precio)
    {
        Nombre = nombre;
        Cantidad = cantidad;
        Precio = precio;
    }
}
