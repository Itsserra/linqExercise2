using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace linqExercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please make sure to copy the 'sample input.txt' in the 'bin/debud/netcoreapp3.0' directory");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
            var logFile = File.ReadAllLines("sample input.txt");
            var list = new List<string>(logFile);

            var modifiedList = list.GroupBy(x => x.First())
                .Select(x => new
                {
                    key = x.Key,
                    values = x.GroupBy(y => y.Last())
                    .Select(y => new
                    {
                        key = y.Key,
                        values = y.GroupBy(z => z[1])
                       .Select(z => new
                       {
                           key = z.Key,
                           values = z.GroupBy(w => w.Length)
                           .Select(w => new
                           {
                               key = w.Key,
                               values = w.OrderByDescending(q => q[2])
                           })
                       })
                    })
                }).ToList();

            modifiedList.ForEach(x =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Grouped By first character: {x.key}");
                x.values.ToList().ForEach(y =>
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"\tGrouped By last character: {y.key}");
                    y.values.ToList().ForEach(z =>
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\t\tGrouped By second character: {z.key}");
                        z.values.ToList().ForEach(w =>
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine($"\t\t\tGrouped By length: {w.key}");
                            w.values.ToList()
                            .ForEach(q =>
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"\t\t\t\t\t\t {q}");
                            });
                        });
                    });
                });
            });
        }
    }
}
