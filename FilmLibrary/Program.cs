using FilmLibrary.Repositories;
using FilmLibrary.Services;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int casenum = 0;
            while (true)
            {
                using (FilmContext db = new FilmContext())
                {
                    FilmServices fs = new FilmServices();
                    casenum = fs.LibMenu();
                    switch (casenum)
                    {
                        case 1://Add
                            db.Add();
                            break;
                        case 2://Clear
                            db.Clear();
                            break;
                        case 3://Find and change or remove
                            db.Find();
                            break;
                        case 4://Print
                            db.Print();
                            break;
                        case 0://Exit
                            break;
                        default:
                            Console.WriteLine("\nВведите число от 0 до 4!");
                            break;
                    }
                    Console.WriteLine("\nНажмите любую клавишу для продолжения");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                if (casenum == 0)
                    break;
            }
        }
    }
}
