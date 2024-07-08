using Microsoft.Win32;
using WebApi.Abstract;
using WebApi.Controllers;

namespace WebApi.Model
{
    // SerieTV eredita da Film e include proprietà aggiuntive per episodi e stagioni
    public class SerieTV : Film, IHasRegista
    {
        public string Regista { get; set; }
        public int NumeroEpisodi { get; set; }
        public int NumeroStagioni { get; set; }

        public SerieTV(string titolo, int anno, string genere, string regista, int numeroEpisodi, int numeroStagioni)
            : base(titolo, anno, genere, Tipologia.SerieTV)
        {
            Regista = regista;
            NumeroEpisodi = numeroEpisodi;
            NumeroStagioni = numeroStagioni;
        }

        public override string GetDescription()
        {
            // Puoi decidere di includere o meno 'Regista' nella descrizione, a seconda delle tue necessità.
            return $"Serie TV: {Titolo} del Regista: {Regista}, {NumeroStagioni} stagioni, {NumeroEpisodi} episodi.";
        }
    }

}
