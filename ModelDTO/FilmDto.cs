namespace WebApi.ModelDTO
{
    public class FilmDto
    {
        public string Titolo { get; set; }
        public int Anno { get; set; }
        public string Genere { get; set; }
        public int Tipologia { get; set; }
        public string Creatore { get; set; } // Aggiunto per mostrare il creatore nel risultato sia su swagger che su endpoint normali
    }
}
