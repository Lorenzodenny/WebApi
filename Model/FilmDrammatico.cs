using WebApi.Abstract;
using WebApi.Controllers;
using WebApi.Extension_Method;

namespace WebApi.Model
{
    public class FilmDrammatico : Film, IHasRegista
    {
        public string Regista { get; set; }

        public FilmDrammatico(string titolo, int anno, string genere, string regista) : base(titolo, anno, genere, Tipologia.Drammatico)
        {
            Regista = regista;
        }

        public override string GetDescription()
        {
            // return FilmHelper.Descrizione(this); // classe e metodo static

            return this.ExtensionDescrizione(); // Usa l'extension method
        }

        public override string ToString()
        {
            return $"{Titolo} di {Regista} ({Anno})";
        }
    }
}
