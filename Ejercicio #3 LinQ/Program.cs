// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}


public class Estudiante : Persona
{
    public string Curso { get; set; } = string.Empty;
    public int Nota { get; set; }

    public override string ToString()
    {
        return $"{Nombre} - Curso: {Curso} - Nota: {Nota}";
    }
}


public class Profesor : Persona
{
    public string Especialidad { get; set; } = string.Empty;
    public List<string> CursosImpartidos { get; set; } = new List<string>();
}

public class Curso
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Profesor { get; set; } = string.Empty;
    public int Creditos { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("UNIVERSIDAD PANAMERICANA");
        Console.WriteLine("SISTEMA DE GESTIÓN ACADÉMICA - LINQ");
        Console.WriteLine("".PadRight(50, '='));

       
        List<Estudiante> estudiantes = ObtenerEstudiantesEjemplo();
        List<Curso> cursos = ObtenerCursosEjemplo();

        
        EstudiantesAprobados(estudiantes);
        Top5EstudiantesPorCurso(estudiantes);
        PromedioPorCurso(estudiantes);
        Top10EstudiantesGlobal(estudiantes);
        RankingEstudiantes(estudiantes);
        MejorEstudiantePorCurso(estudiantes);
        EstudiantesPorIntervalos(estudiantes);

       
        ExportarResultadosAcademicos(estudiantes, cursos);

        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }

   
    static List<Estudiante> ObtenerEstudiantesEjemplo()
    {
        return new List<Estudiante>
        {
            new Estudiante { Id = 1, Nombre = "Ana García", Curso = "Matemáticas", Nota = 85 },
            new Estudiante { Id = 2, Nombre = "Carlos López", Curso = "Matemáticas", Nota = 92 },
            new Estudiante { Id = 3, Nombre = "María Rodríguez", Curso = "Matemáticas", Nota = 78 },
            new Estudiante { Id = 4, Nombre = "Juan Pérez", Curso = "Programación", Nota = 65 },
            new Estudiante { Id = 5, Nombre = "Laura Martínez", Curso = "Programación", Nota = 88 },
            new Estudiante { Id = 6, Nombre = "Pedro Sánchez", Curso = "Programación", Nota = 45 },
            new Estudiante { Id = 7, Nombre = "Sofía Hernández", Curso = "Programación", Nota = 95 },
            new Estudiante { Id = 8, Nombre = "Diego González", Curso = "Programación", Nota = 72 },
            new Estudiante { Id = 9, Nombre = "Elena Castro", Curso = "Bases de Datos", Nota = 91 },
            new Estudiante { Id = 10, Nombre = "Miguel Ángel Ruiz", Curso = "Bases de Datos", Nota = 84 },
            new Estudiante { Id = 11, Nombre = "Carmen Vargas", Curso = "Bases de Datos", Nota = 79 },
            new Estudiante { Id = 12, Nombre = "Javier Mora", Curso = "Matemáticas", Nota = 68 },
            new Estudiante { Id = 13, Nombre = "Patricia Silva", Curso = "Programación", Nota = 96 },
            new Estudiante { Id = 14, Nombre = "Ricardo Torres", Curso = "Bases de Datos", Nota = 87 },
            new Estudiante { Id = 15, Nombre = "Isabel Reyes", Curso = "Matemáticas", Nota = 74 }
        };
    }

    static List<Curso> ObtenerCursosEjemplo()
    {
        return new List<Curso>
        {
            new Curso { Codigo = "MAT101", Nombre = "Matemáticas", Profesor = "Dr. Pérez", Creditos = 4 },
            new Curso { Codigo = "PROG202", Nombre = "Programación", Profesor = "Ing. García", Creditos = 5 },
            new Curso { Codigo = "BD303", Nombre = "Bases de Datos", Profesor = "MSc. López", Creditos = 4 }
        };
    }

    
    static void EstudiantesAprobados(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== ESTUDIANTES APROBADOS (Nota >= 70) ===");

        var aprobados = from estudiante in estudiantes
                        where estudiante.Nota >= 70
                        orderby estudiante.Nota descending
                        select estudiante;

        foreach (var estudiante in aprobados)
        {
            Console.WriteLine($"- {estudiante.Nombre} - {estudiante.Curso}: {estudiante.Nota}");
        }
        Console.WriteLine($"Total aprobados: {aprobados.Count()}");
        Console.WriteLine();
    }

    
    static void Top5EstudiantesPorCurso(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== TOP 5 ESTUDIANTES POR CURSO ===");

        var topPorCurso = from estudiante in estudiantes
                          group estudiante by estudiante.Curso into cursoGrupo
                          select new
                          {
                              Curso = cursoGrupo.Key,
                              TopEstudiantes = cursoGrupo.OrderByDescending(e => e.Nota)
                                                        .Take(5)
                          };

        foreach (var curso in topPorCurso)
        {
            Console.WriteLine($"\n📚 CURSO: {curso.Curso}");
            int posicion = 1;
            foreach (var estudiante in curso.TopEstudiantes)
            {
                Console.WriteLine($"   {posicion}. {estudiante.Nombre} - Nota: {estudiante.Nota}");
                posicion++;
            }
        }
        Console.WriteLine();
    }


    static void PromedioPorCurso(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== PROMEDIO POR CURSO ===");

        var promedios = from estudiante in estudiantes
                        group estudiante by estudiante.Curso into cursoGrupo
                        select new
                        {
                            Curso = cursoGrupo.Key,
                            Promedio = cursoGrupo.Average(e => e.Nota),
                            Cantidad = cursoGrupo.Count()
                        };

        foreach (var curso in promedios)
        {
            Console.WriteLine($"- {curso.Curso}: {curso.Promedio:F1} (de {curso.Cantidad} estudiantes)");
        }
        Console.WriteLine();
    }

    static void Top10EstudiantesGlobal(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== TOP 10 ESTUDIANTES GLOBAL ===");

        var top10 = estudiantes.OrderByDescending(e => e.Nota)
                              .Take(10);

        int posicion = 1;
        foreach (var estudiante in top10)
        {
            Console.WriteLine($"{posicion}. {estudiante.Nombre} - {estudiante.Curso}: {estudiante.Nota}");
            posicion++;
        }
        Console.WriteLine();
    }

   
    static void RankingEstudiantes(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== RANKING GENERAL DE ESTUDIANTES ===");

        var ranking = estudiantes.OrderByDescending(e => e.Nota)
                               .Select((estudiante, index) => new
                               {
                                   Posicion = index + 1,
                                   Estudiante = estudiante
                               });

        foreach (var item in ranking)
        {
            Console.WriteLine($"{item.Posicion,2}. {item.Estudiante.Nombre,-20} {item.Estudiante.Curso,-15} Nota: {item.Estudiante.Nota}");
        }
        Console.WriteLine();
    }

    
    static void MejorEstudiantePorCurso(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== MEJOR ESTUDIANTE POR CURSO ===");

        var mejores = from estudiante in estudiantes
                      group estudiante by estudiante.Curso into cursoGrupo
                      select new
                      {
                          Curso = cursoGrupo.Key,
                          MejorEstudiante = cursoGrupo.OrderByDescending(e => e.Nota).First(),
                          MejorNota = cursoGrupo.Max(e => e.Nota)
                      };

        foreach (var curso in mejores)
        {
            Console.WriteLine($"- {curso.Curso}: {curso.MejorEstudiante.Nombre} - Nota: {curso.MejorNota}");
        }
        Console.WriteLine();
    }

    
    static void EstudiantesPorIntervalos(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== ESTUDIANTES POR INTERVALOS DE NOTA ===");

        var porIntervalos = from estudiante in estudiantes
                            group estudiante by ObtenerIntervalo(estudiante.Nota) into intervaloGrupo
                            orderby intervaloGrupo.Key
                            select intervaloGrupo;

        foreach (var intervalo in porIntervalos)
        {
            Console.WriteLine($"\n📊 INTERVALO: {intervalo.Key}");
            Console.WriteLine($"Cantidad: {intervalo.Count()} estudiantes");

            foreach (var estudiante in intervalo.OrderByDescending(e => e.Nota))
            {
                Console.WriteLine($"   - {estudiante.Nombre} - {estudiante.Curso}: {estudiante.Nota}");
            }
        }
        Console.WriteLine();
    }

  
    static string ObtenerIntervalo(int nota)
    {
        if (nota >= 80) return "80-100 (Excelente)";
        else if (nota >= 60) return "60-79 (Bueno)";
        else return "0-59 (Necesita mejorar)";
    }

    
    static void ExportarResultadosAcademicos(List<Estudiante> estudiantes, List<Curso> cursos)
    {
        string nombreArchivo = "reporte_academico.txt";

        try
        {
            using (StreamWriter writer = new StreamWriter(nombreArchivo))
            {
                writer.WriteLine("UNIVERSIDAD PANAMERICANA");
                writer.WriteLine("REPORTE ACADÉMICO - SISTEMA LINQ");
                writer.WriteLine("".PadRight(60, '='));
                writer.WriteLine($"Fecha de generación: {DateTime.Now}");
                writer.WriteLine($"Total estudiantes: {estudiantes.Count}");
                writer.WriteLine();

               
                writer.WriteLine("1. RESUMEN POR CURSO:");
                var resumenCursos = from e in estudiantes
                                    group e by e.Curso into g
                                    select new
                                    {
                                        Curso = g.Key,
                                        Promedio = g.Average(e => e.Nota),
                                        Aprobados = g.Count(e => e.Nota >= 70),
                                        Total = g.Count()
                                    };

                foreach (var curso in resumenCursos)
                {
                    writer.WriteLine($"   - {curso.Curso}:");
                    writer.WriteLine($"     Promedio: {curso.Promedio:F1} | Aprobados: {curso.Aprobados}/{curso.Total}");
                }

          
                writer.WriteLine("\n2. TOP 5 ESTUDIANTES GLOBAL:");
                var top5Global = estudiantes.OrderByDescending(e => e.Nota)
                                          .Take(5);
                int pos = 1;
                foreach (var est in top5Global)
                {
                    writer.WriteLine($"   {pos}. {est.Nombre} - {est.Curso}: {est.Nota}");
                    pos++;
                }

                
                writer.WriteLine("\n3. DISTRIBUCIÓN POR INTERVALOS:");
                var intervalos = from e in estudiantes
                                 group e by ObtenerIntervalo(e.Nota) into g
                                 select new { Intervalo = g.Key, Cantidad = g.Count() };

                foreach (var intervalo in intervalos)
                {
                    writer.WriteLine($"   - {intervalo.Intervalo}: {intervalo.Cantidad} estudiantes");
                }

                writer.WriteLine("\n" + "".PadRight(60, '='));
                writer.WriteLine("FIN DEL REPORTE");
            }

            string rutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), nombreArchivo);
            Console.WriteLine($" REPORTE EXPORTADO EXITOSAMENTE");
            Console.WriteLine($" Archivo: {nombreArchivo}");
            Console.WriteLine($" Ubicación: {rutaCompleta}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Error al exportar: {ex.Message}");
        }
    }
}