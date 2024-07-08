using WebApi.Abstract;
using WebApi.Generics;
using WebApi.Model;
using WebApi.Model.Static;
using WebApi.ModelDTO;

namespace WebApi.Services
{
    public class FilmService
    {
        private readonly Container<Film> _filmContainer;
        private bool _isInitializing;
        public FilmService()
        {
            _isInitializing = true;
            _filmContainer = new Container<Film>();
            SetupLogging(isProduction: true);
            InitializeFilms();
            _isInitializing = false;
        }

        private void SetupLogging(bool isProduction)
        {
            LogHelper.Instance.ConfigureLogging(isProduction);
            _filmContainer.LogAction = LogHelper.Instance.Log;
        }

        private void InitializeFilms()
        {
            _filmContainer.AddItem(new Documentario("Planet Earth", 2006, "Documentario", "David Attenborough"));
            _filmContainer.AddItem(new FilmDrammatico("Schindler's List", 1993, "Drammatico", "Steven Spielberg"));
            _filmContainer.AddItem(new FilmAzione("Mad Max: Fury Road", 2015, "Azione", "George Miller"));
            _filmContainer.AddItem(new FilmCommedia("The Grand Budapest Hotel", 2014, "Commedia", "Wes Anderson"));
            _filmContainer.AddItem(new FilmHorror("Get Out", 2017, "Horror", "Jordan Peele"));
            _filmContainer.AddItem(new FilmFantascienza("Blade Runner 2049", 2017, "Fantascienza", "Denis Villeneuve"));
            _filmContainer.AddItem(new FilmAnimazione("Toy Story", 1995, "Animazione", "Pixar Animation Studios"));
            _filmContainer.AddItem(new FilmStorico("Braveheart", 1995, "Storico", "Mel Gibson"));
        }

        public void PrintAllFilms()
        {
            string presentazione = "\nLA LISTA DEI FILM AGGIORNATA è LA SEGUENTE :\n";
            _filmContainer.LogAction.Invoke(presentazione);
            foreach (var film in _filmContainer.GetAllItems())
            {
                string filmDetails = $"Film: {film.Titolo}, Anno: {film.Anno}, Genere: {film.Genere}";

                // Aggiungi informazioni specifiche in base al tipo di film
                if (film is IHasRegista registaFilm)
                {
                    filmDetails += $", Regista: {registaFilm.Regista}";
                }
                else if (film is Documentario documentario)
                {
                    filmDetails += $", Narratore: {documentario.Narratore}";
                }
                else if (film is FilmAnimazione animazione)
                {
                    filmDetails += $", Studio di Animazione: {animazione.StudioAnimazione}";
                }


                _filmContainer.LogAction?.Invoke(filmDetails);
            }
        }

        public List<FilmDto> FilterFilms(string genere, int? anno, string creatore)
        {
            IEnumerable<Film> filtered = _filmContainer.GetAllItems();

            if (!string.IsNullOrEmpty(genere))
            {
                filtered = filtered.Where(f => f.Genere.Equals(genere, StringComparison.OrdinalIgnoreCase));
            }

            if (anno.HasValue)
            {
                filtered = filtered.Where(f => f.Anno == anno.Value);
            }

            if (!string.IsNullOrEmpty(creatore))
            {
                filtered = filtered.Where(f => (f as IHasRegista)?.Regista == creatore ||
                                               (f as Documentario)?.Narratore == creatore ||
                                               (f as FilmAnimazione)?.StudioAnimazione == creatore);
            }

            return filtered.Select(f => new FilmDto
            {
                Titolo = f.Titolo,
                Anno = f.Anno,
                Genere = f.Genere,
                Tipologia = (int)f.Tipologia, 
                Creatore = (f as IHasRegista)?.Regista ??
                           (f as Documentario)?.Narratore ??
                           (f as FilmAnimazione)?.StudioAnimazione,
                NumeroEpisodi = (f as SerieTV)?.NumeroEpisodi, // Aggiunto per SerieTV
                NumeroStagioni = (f as SerieTV)?.NumeroStagioni // Aggiunto per SerieTV
            }).ToList();
        }


        public List<Film> GetFilmsByYear(int year)
        {
            return _filmContainer.FindFilms(film => film.Anno >= year);
        }
        

        public List<Film> GetAllFilms()
        {
            return _filmContainer.GetAllItems();
        }

        public Film GetFilmById(int id)
        {
            return _filmContainer.GetItemById(id);
        }

        public bool AddFilm(Film film)
        {
            _filmContainer.AddItem(film);
            PrintAllFilms();
            return true;
        }

        public bool UpdateFilm(int id, Film updatedFilm)
        {
            var film = GetFilmById(id);
            if (film != null)
            {
                // Aggiorna le proprietà comuni
                film.Titolo = updatedFilm.Titolo;
                film.Anno = updatedFilm.Anno;
                film.Genere = updatedFilm.Genere;

                // Aggiorna proprietà specifiche in base al tipo
                if (film is IHasRegista filmWithDirector && updatedFilm is IHasRegista updatedFilmWithDirector)
                {
                    filmWithDirector.Regista = updatedFilmWithDirector.Regista;
                }
                else if (film is Documentario doc && updatedFilm is Documentario updatedDoc)
                {
                    doc.Narratore = updatedDoc.Narratore;
                }
                else if (film is FilmAnimazione anim && updatedFilm is FilmAnimazione updatedAnim)
                {
                    anim.StudioAnimazione = updatedAnim.StudioAnimazione;
                }

                // Chiamata a UpdateItem per eseguire l'aggiornamento e loggare l'azione
                _filmContainer.UpdateItem(id, updatedFilm);

                PrintAllFilms();
                return true;
            }
            return false;
        }

        // Nuovo metodo che utilizza Func
        public List<int> CalculateFilmTitleLengths()
        {
            return _filmContainer.ApplyFunction(film => film.Titolo.Length);
        }

        public bool DeleteFilm(int index)
        {
            if (index >= 0 && index < _filmContainer.GetAllItems().Count)
            {
                _filmContainer.RemoveItem(index);
                PrintAllFilms();
                return true;
            }
            return false;
        }
    }
}
