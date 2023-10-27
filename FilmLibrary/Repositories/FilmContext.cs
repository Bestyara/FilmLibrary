using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmLibrary.Models;
using FilmLibrary.Interfaces;
using FilmLibrary.Services;

namespace FilmLibrary.Repositories
{
    public class FilmContext : DbContext
    {
        public DbSet<Film> Films { get; set; } = null!;
        FilmServices fs = new FilmServices();
        public FilmContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=FilmDB.db");
        }

        public void Add()
        {
            string name = "";
            int num = 0;
            RankList rank = new RankList();
            fs.AddService(ref name,ref num,ref rank);
            Films.Add(new Film(name,num,rank));
            SaveChanges();
        }
        public void Clear()
        { 
            Database.EnsureDeletedAsync().Wait();
            Console.WriteLine("БД очищена успешно!");
        }
        public void Print()
        {
            var films = Films.ToList();
            if (films!=null)
                foreach (var i in films)
                {
                    Console.WriteLine("__________________________");
                    Console.WriteLine($"\nНазвание: {i.Name}\n Рейтинг: {i.Rate}\n ОВ: {i.Rank}");
                }
            else
            {
                Console.WriteLine("Библиотека пуста :(");
            }
        }
        public void Find()
        {
            var films = Films.ToList();
            Console.WriteLine("Введите название фильма:");
            var name = Console.ReadLine();
            var film = films
                .Where(x => x.Name.Equals(name))
                .FirstOrDefault();
            fs.FindService(ref film);
        }
    }
}
