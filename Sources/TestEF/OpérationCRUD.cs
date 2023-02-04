
using DTO_EF;
using EntityFramework;

Console.WriteLine("Hello, World!");













using (var context = new SqlLiteDbContext())
{
    Console.WriteLine("Test Opération CRUD : Read");

    context.Database.EnsureCreated();
    foreach (var champion in context.Champions)
    {
        Console.WriteLine($"{champion.Name}");
    }
}