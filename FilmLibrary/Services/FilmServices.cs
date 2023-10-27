using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmLibrary.Repositories;
using FilmLibrary.Models;
using FilmLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmLibrary.Services
{
    public class FilmServices
    {
        public FilmServices() { }
        public int LibMenu()
        {
            Console.WriteLine("Библиотека фильмов");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1 - Добавить фильм в библиотеку");
            Console.WriteLine("2 - Очистить БД");
            Console.WriteLine("3 - Найти фильм и изменить или убрать данный фильм");
            Console.WriteLine("4 - Вывести все фильмы в БД");
            Console.WriteLine("0 - Выход\n");
            Console.WriteLine("Введите номер команды:");
            return CheckIntService(Console.ReadLine());
        }
        public static int CheckIntService(string num)
        {
            while (true)
            {
                try
                {
                    Convert.ToInt32(num);
                    return Convert.ToInt32(num);
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nВведите другое значение:");
                    num = Console.ReadLine();
                }
            }
        }
        public static RankList CheckEnumService(string enm)
        {
            while (true)
            {
                if (Enum.IsDefined(typeof(RankList), enm))
                {
                    Enum.TryParse(enm, out RankList rank);
                    return rank;
                }
                else
                {
                    Console.WriteLine("\nВведите другое значение:");
                    enm = Console.ReadLine();
                }
            }
        }
        public void AddService(ref string name, ref int num, ref RankList r)
        {
            FilmContext f = new FilmContext();
            Console.WriteLine("\nВведите название фильма:");
            name = Console.ReadLine();
            Console.WriteLine("\nВведите рейтинг фильма (от 0 до 10)");
            num = CheckIntService(Console.ReadLine());
            while (num > 10 || num < 0)
            {
                Console.WriteLine("Число должно быть от 0 до 10!");
                num = CheckIntService(Console.ReadLine());
            }
            Console.WriteLine("\nВведите ОВ от фильма (Bad, Good, Best)");
            r = CheckEnumService(Console.ReadLine());
        }
        public void FindService(ref Film film)
        {
            if (film != null)
            {
                Console.WriteLine("Фильм найден!");
                Console.WriteLine("__________________________");
                Console.WriteLine($"\nНазвание: {film.Name}\n Рейтинг:{film.Rate}\n ОВ: {film.Rank}");
                Console.WriteLine("__________________________");
                Console.WriteLine("Что необходимо сделать с данным фильмом?");
                Console.WriteLine("1 - Удалить из БД");
                Console.WriteLine("2 - Редактировать данные о фильме");
                Console.WriteLine("0 - Ничего, выйти в главное меню");
                int num = CheckIntService(Console.ReadLine());
                using (FilmContext db = new FilmContext())
                    switch (num)
                    {
                        case 1:
                            db.Films.Remove(film);
                            db.SaveChanges();
                            Console.WriteLine("Удаление произошло успешно!");
                            break;
                        case 2:
                            Console.WriteLine("__________________________");
                            Console.WriteLine("1 - Изменить название фильма");
                            Console.WriteLine("2 - Изменить оценку о фильме");
                            Console.WriteLine("3 - Изменить ОВ от фильма");
                            int numfix = FilmServices.CheckIntService(Console.ReadLine());
                            switch (numfix)
                            {
                                case 1:
                                    Console.WriteLine("Введите название:");
                                    var namefix = Console.ReadLine();
                                    film.Name = namefix;
                                    break;
                                case 2:
                                    Console.WriteLine("Введите оценку для фильма:");
                                    var ratefix = FilmServices.CheckIntService(Console.ReadLine());
                                    while (ratefix > 10 || ratefix < 0)
                                    {
                                        Console.WriteLine("Число должно быть от 0 до 10!");
                                        ratefix = CheckIntService(Console.ReadLine());
                                    }
                                    film.Rate = ratefix;
                                    break;
                                case 3:
                                    Console.WriteLine("Введите ОВ от фильма:");
                                    var enumfix = FilmServices.CheckEnumService(Console.ReadLine());
                                    film.Rank = enumfix;
                                    break;
                                default:
                                    break;
                            }
                            db.Entry(film).State = EntityState.Modified;
                            db.SaveChanges();
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Неккоректный ввод");
                            break;
                    }
            }
            else
            {
                Console.WriteLine("Фильм с таким названием не найден -_-");
            }
        }
    }
}
