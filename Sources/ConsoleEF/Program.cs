using DTO_EF;
using DTO_EF.Mapper;
using EntityFramework;
using Model;
using System.Data.SqlTypes;
using System.Text;

// Initialisation de la base
using (var _context = new SqlLiteDbContext())
{
    _context.Database.EnsureDeleted();
}

// Ajout d'un champion
using (var _context = new SqlLiteDbContext())
{
    _context.Database.EnsureDeleted();

    _context.Database.EnsureCreated();
    StringBuilder myStringBuilder = new StringBuilder("Ajout du champion ");

    var champion = new Champion("Hugo", ChampionClass.Fighter);
    var championEntity = champion.ToEntity();
    _context.Champions.Add(championEntity);

    myStringBuilder.Append(champion.Name);

    champion = new Champion("Loris", ChampionClass.Marksman);
    championEntity = champion.ToEntity();
    _context.Champions.Add(championEntity);

    Console.WriteLine(myStringBuilder.Append(champion.Name));

    _context.SaveChanges();
}

// Modification d'un champion
using (var _context = new SqlLiteDbContext())
{
    StringBuilder myStringBuilder = new StringBuilder("Modification du champion ");

    var champion = _context.Champions.SingleOrDefault(c => c.Name.Equals("Hugo"));
    myStringBuilder.Append(champion.Name);
    myStringBuilder.Append("\nAncienne classe ");
    myStringBuilder.Append(champion.Class);

    champion.Class = ChampionClass.Tank.ToString();

    myStringBuilder.Append("\nNouvelle classe ");
    myStringBuilder.Append(champion.Class);

    Console.WriteLine(myStringBuilder);

    _context.SaveChanges();
}

// Get champions
using (var _context = new SqlLiteDbContext())
{
    _context.Database.EnsureCreated();
    StringBuilder myStringBuilder = new StringBuilder("Il y a ");

    var champions = _context.Champions;

    foreach (var champion in champions)
    {
        myStringBuilder.Append(champion.Name);
        myStringBuilder.Append("\n");
    }
    Console.WriteLine(myStringBuilder);

    _context.SaveChanges();
}

// Get champion par nom
using (var _context = new SqlLiteDbContext())
{
    _context.Database.EnsureCreated();
    StringBuilder myStringBuilder = new StringBuilder("Il y a ");

    var champions = _context.Champions.Where(c => c.Name.Equals("Hugo"));

    foreach (var champion in champions)
    {
        myStringBuilder.Append(champion.Name);
        myStringBuilder.Append("\n");
    }
    Console.WriteLine(myStringBuilder);

    _context.SaveChanges();
}

// Get nombre champions
using (var _context = new SqlLiteDbContext())
{
    _context.Database.EnsureCreated();
    StringBuilder myStringBuilder = new StringBuilder("Il y a ");

    var champion = _context.Champions.Count();

    myStringBuilder.Append(champion);
    Console.WriteLine(myStringBuilder.Append(" champions"));

    _context.SaveChanges();
}

// Get nombre champion par nom
using (var _context = new SqlLiteDbContext())
{
    _context.Database.EnsureCreated();
    StringBuilder myStringBuilder = new StringBuilder("Il y a  ");

    var champion = _context.Champions.Count(item => item.Name == "Hugo");

    myStringBuilder.Append(champion);
    Console.WriteLine(myStringBuilder.Append(" champions"));

    _context.SaveChanges();
}

// Suppression d'un champion
using (var _context = new SqlLiteDbContext())
{
    StringBuilder myStringBuilder = new StringBuilder("Suppression du champion ");

    var champion = _context.Champions.SingleOrDefault(c => c.Name.Equals("Hugo"));
    _context.Champions.Remove(champion);

    Console.WriteLine(myStringBuilder.Append(champion.Name));

    _context.SaveChanges();
}

Console.Read();