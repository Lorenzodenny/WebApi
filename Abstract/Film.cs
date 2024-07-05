using WebApi.Controllers;

namespace WebApi.Abstract
{
    public abstract class Film : IFilm
    {
        public string Titolo { get; set; }
        public int Anno { get; set; }
        public string Genere { get; set; }

        public Tipologia Tipologia { get; set; }

        protected Film () { }
        protected Film(string titolo, int anno, string  genere, Tipologia tipologia)
        {
            Titolo = titolo;
            Anno = anno;
            Genere = genere;
            Tipologia = tipologia;
        }

        public override string ToString()
        {
            return $"{Titolo} ({Anno})";
        }

        public abstract string GetDescription();
    }
}
