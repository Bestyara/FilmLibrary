using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmLibrary.Interfaces;

namespace FilmLibrary.Models
{
    public class Film : IFilm
    {
        private int _id = 0;
        public int ID { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public RankList Rank { get; set; }
        public Film() 
        {
            Name = "";
            Rate = 0;
            Rank = new RankList();
        }
        public Film(string name, int rate, RankList rank)
        {
            ID = _id++;
            Name = name;
            Rate = rate;
            Rank = rank;
        }
    }
}
