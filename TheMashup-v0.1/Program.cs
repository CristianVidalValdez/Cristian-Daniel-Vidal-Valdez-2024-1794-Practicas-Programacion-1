using System;
using System.Collections.Generic;
using System.Linq;

namespace TheMashup
{
    public class Postre
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Precio { get; set; }
    }

    class Program
    {
        static List<Postre> inv = new List<Postre>();

        static void Main()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=========================================================");
                Console.WriteLine("=== SISTEMA DE POSTRES | Cristian Vidal (2024-1794) ===");
                Console.WriteLine("=========================================================");
                Console.WriteLine("1. Agregar");
                Console.WriteLine("2. Listar");
                Console.WriteLine("3. Buscar");
                Console.WriteLine("4. Modificar");
                Console.WriteLine("5. Eliminar");
                Console.WriteLine("6. Salir");
                Console.Write("\nOpción: ");

                string op = Console.ReadLine() ?? "";

                try
                {
                    switch (op)
                    {
                        case "1": Agregar(); break;
                        case "2": Listar(); break;
                        case "3": Buscar(); break;
                        case "4": Modificar(); break;
                        case "5": Eliminar(); break;
                        case "6": salir = true; break;
                    }
                }
                catch
                {
                    Console.WriteLine("Error. Presione una tecla.");
                    Console.ReadKey();
                }
            }
        }

        static void Agregar()
        {
            Console.Clear();
            Console.Write("Ingrese el Id (01, 02, etc.): ");
            string id = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(id) || inv.Any(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase))) 
            {
                Console.WriteLine("Id inválido o repetido.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nombre: ");
            string nom = Console.ReadLine() ?? "";
            
            Console.Write("Categoría: ");
            string cat = Console.ReadLine() ?? "";

            Console.Write("Precio: ");
            if (!decimal.TryParse(Console.ReadLine() ?? "", out decimal pre) || pre < 0) 
            {
                Console.WriteLine("Precio inválido.");
                Console.ReadKey();
                return;
            }

            inv.Add(new Postre { Id = id, Nombre = nom, Categoria = cat, Precio = pre });
            Console.WriteLine("Agregado. Presione una tecla...");
            Console.ReadKey();
        }

        static void Listar()
        {
            Console.Clear();
            if (inv.Count == 0) Console.WriteLine("Vacío.");
            else foreach (var p in inv) Console.WriteLine($"Id: {p.Id} | Nombre: {p.Nombre} | Categoría: {p.Categoria} | Precio: ${p.Precio}");
            
            Console.WriteLine("\nPresione una tecla...");
            Console.ReadKey();
        }

        static void Buscar()
        {
            Console.Clear();
            Console.WriteLine("1. Por Id\n2. Por Nombre\n3. Por Categoría");
            Console.Write("Opción: ");
            string crit = Console.ReadLine() ?? "";
            
            Console.Write("Buscar: ");
            string term = (Console.ReadLine() ?? "").ToLower();

            var res = new List<Postre>();

            if (crit == "1") res = inv.Where(p => p.Id.ToLower().Contains(term)).ToList();
            else if (crit == "2") res = inv.Where(p => p.Nombre.ToLower().Contains(term)).ToList();
            else if (crit == "3") res = inv.Where(p => p.Categoria.ToLower().Contains(term)).ToList();

            foreach (var p in res) Console.WriteLine($"- [{p.Id}] {p.Nombre} ({p.Categoria}) -> ${p.Precio}");
            
            Console.WriteLine("\nPresione una tecla...");
            Console.ReadKey();
        }

        static void Modificar()
        {
            Console.Clear();
            Console.Write("Id a modificar: ");
            string id = Console.ReadLine() ?? "";

            var p = inv.FirstOrDefault(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (p != null)
            {
                Console.Write("Nuevo Nombre (Enter para omitir): ");
                string nom = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(nom)) p.Nombre = nom;

                Console.Write("Nueva Categoría (Enter para omitir): ");
                string cat = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(cat)) p.Categoria = cat;

                Console.Write("Nuevo Precio (Enter para omitir): ");
                string preStr = Console.ReadLine() ?? "";
                if (decimal.TryParse(preStr, out decimal pre) && pre >= 0) p.Precio = pre;
                
                Console.WriteLine("Modificado.");
            }
            else Console.WriteLine("No encontrado.");

            Console.ReadKey();
        }

        static void Eliminar()
        {
            Console.Clear();
            Console.Write("Id a eliminar: ");
            string id = Console.ReadLine() ?? "";

            var p = inv.FirstOrDefault(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (p != null)
            {
                Console.Write($"¿Eliminar '{p.Nombre}'? (s/n): ");
                if ((Console.ReadLine() ?? "").ToLower() == "s") 
                {
                    inv.Remove(p);
                    Console.WriteLine("Eliminado.");
                }
            }
            else Console.WriteLine("No encontrado.");

            Console.ReadKey();
        }
    }
}