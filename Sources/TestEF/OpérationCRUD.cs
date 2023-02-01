﻿
using DTO_EF;
using EntityFramework;

Console.WriteLine("Hello, World!");













using (var context = new SqlLiteDbContext())
{
    Console.WriteLine("Test Opération CRUD : Read");
    /*foreach (var champion in context.Champions)
    {
        Console.WriteLine($"{champion}");
    }*/

    //context.Champions.ToList().ForEach(c => Console.WriteLine(c.Name));

    context.Database.EnsureCreated();
    foreach (var champion in context.Champions)
    {
        Console.WriteLine($"{champion.Name}");
    }
}