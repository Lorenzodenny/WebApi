using Microsoft.Win32;
using WebApi.Abstract;
using WebApi.Controllers;
using WebApi.Extension_Method;

namespace WebApi.Model
{
    public class Documentario : Film
    {
        public string Narratore { get; set; }

        public Documentario(string titolo, int anno, string genere, string narratore) : base(titolo, anno, genere, Tipologia.Documentario)
        {
            Narratore = narratore;
        }

        public override string GetDescription()
        {
            // return FilmHelper.Descrizione(this); // classe e metodo static

            return this.ExtensionDescrizione(); // Usa l'extension method
        }

        public override string ToString()
        {
            return $"{Titolo} di {Narratore} ({Anno})";
        }

    }
}
