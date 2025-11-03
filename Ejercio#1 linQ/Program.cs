// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

// Clases primero
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}

public class Estudiante
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Nota { get; set; }
}

// Programa principal
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("UNIVERSIDAD PANAMERICANA");
        Console.WriteLine("Ejercicios de LINQ\n");

        Ejercicio1();
        Ejercicio2();
        Ejercicio3();

        Console.ReadLine();
    }

    static void Ejercicio1()
    {
        Console.WriteLine("=== EJERCICIO 1: Números Pares ===");

        List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

        var numerosPares = from num in numeros
                           where num % 2 == 0
                           select num;

        Console.WriteLine("Lista original: " + string.Join(", ", numeros));
        Console.WriteLine("Números pares: " + string.Join(", ", numerosPares));
        Console.WriteLine();
    }

    static void Ejercicio2()
    {
        Console.WriteLine("=== EJERCICIO 2: Productos con precio > 100 ===");

        List<Producto> productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Laptop", Precio = 1200.50m },
            new Producto { Id = 2, Nombre = "Mouse", Precio = 25.99m },
            new Producto { Id = 3, Nombre = "Teclado", Precio = 75.00m },
            new Producto { Id = 4, Nombre = "Monitor", Precio = 300.00m },
            new Producto { Id = 5, Nombre = "Tablet", Precio = 450.00m },
            new Producto { Id = 6, Nombre = "Auriculares", Precio = 80.00m }
        };

        var productosCaros = from producto in productos
                             where producto.Precio > 100
                             select producto;

        Console.WriteLine("Todos los productos:");
        foreach (var producto in productos)
        {
            Console.WriteLine($"- {producto.Nombre}: ${producto.Precio}");
        }

        Console.WriteLine("\nProductos con precio mayor a $100:");
        foreach (var producto in productosCaros)
        {
            Console.WriteLine($"- {producto.Nombre}: ${producto.Precio}");
        }
        Console.WriteLine();
    }

    static void Ejercicio3()
    {
        Console.WriteLine("=== EJERCICIO 3: Estudiantes agrupados por nota ===");

        List<Estudiante> estudiantes = new List<Estudiante>
        {
            new Estudiante { Id = 1, Nombre = "Ana García", Nota = 85 },
            new Estudiante { Id = 2, Nombre = "Carlos López", Nota = 92 },
            new Estudiante { Id = 3, Nombre = "María Rodríguez", Nota = 78 },
            new Estudiante { Id = 4, Nombre = "Juan Pérez", Nota = 85 }, // Misma nota que Ana
            new Estudiante { Id = 5, Nombre = "Laura Martínez", Nota = 92 }, // Misma nota que Carlos
            new Estudiante { Id = 6, Nombre = "Pedro Sánchez", Nota = 78 }, // Misma nota que María
            new Estudiante { Id = 7, Nombre = "Sofía Hernández", Nota = 90 },
            new Estudiante { Id = 8, Nombre = "Diego González", Nota = 85 } // Misma nota que Ana y Juan
        };

        // CORRECCIÓN: Agrupar por la nota específica, no por rangos
        var estudiantesPorNota = from estudiante in estudiantes
                                 group estudiante by estudiante.Nota into grupoNota                                 orderby grupoNota.Key
                                 select grupoNota;

        Console.WriteLine("Lista de estudiantes:");
        foreach (var estudiante in estudiantes)
        {
            Console.WriteLine($"- {estudiante.Nombre}: {estudiante.Nota}");
        }

        Console.WriteLine("\nEstudiantes agrupados por nota:");
        foreach (var grupo in estudiantesPorNota)
        {
            Console.WriteLine($"\nNota: {grupo.Key}");
            foreach (var estudiante in grupo)
            {
                Console.WriteLine($"  - {estudiante.Nombre}");
            }
        }
    }
}