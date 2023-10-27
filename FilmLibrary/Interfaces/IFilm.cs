using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FilmLibrary.Interfaces
{
    public enum RankList
    {
        Bad,
        Good,
        Best,
    }
    public interface IFilm
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public RankList Rank { get; set; }
    }
}
