using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstract;
using WebApi.Generics;
using WebApi.Model;
using System.Linq;
using System.Text.Json;
using WebApi.Model.Static;
using WebApi.Services;
using WebApi.ModelDTO;
using MyAppConfig = WebApi.Design_Pattern.Singleton.MyAppConfigurationManager;
using WebApi.Design_Pattern.Factory;

namespace WebApi.Controllers
{
    public enum Tipologia
    {
        Azione,
        Commedia,
        Drammatico,
        Documentario,
        Fantascienza,
        Horror,
        Animazione,
        Storico,
        SerieTV
    }

    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly FilmService _filmService;
        private readonly IFilmFactory _filmFactory;


        // Dependency Injection
        public FilmController(FilmService filmService, IFilmFactory filmFactory)
        {
            _filmService = filmService;
            _filmFactory = filmFactory;
        }


        [HttpGet("RecentFilms")]
        public ActionResult<List<Film>> ListFilmsByYear([FromQuery] int? year)
        {
            int yearToUse = year ?? int.Parse(MyAppConfig.Instance.GetSetting("default_year_filter"));
            var films = _filmService.GetFilmsByYear(yearToUse);
            ControllerHelpers.SetCacheHeader(Response, 3600);
            return Ok(films);
        }


        // endpoint che utilizza il metodo con Func
        [HttpGet("FilmTitleLengths")]
        public ActionResult<List<int>> GetFilmTitleLengths()
        {
            var titleLengths = _filmService.CalculateFilmTitleLengths();
            ControllerHelpers.SetCacheHeader(Response, 600);
            return Ok(titleLengths);
        }

        [HttpGet]
        public ActionResult<IEnumerable<FilmDto>> Get([FromQuery] string genere = null, [FromQuery] int? anno = null, [FromQuery] string creatore = null)
        {
            try
            {
                var films = _filmService.FilterFilms(genere, anno, creatore);
                ControllerHelpers.SetCacheHeader(Response, 300);
                return Ok(films);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }





        [HttpGet("{id}")]
        public ActionResult<Film> Get(int id)
        {
            try
            {
                var film = _filmService.GetFilmById(id);
                if (film == null)
                {
                    return NotFound();
                }
                ControllerHelpers.SetCacheHeader(Response, 1200);
                return Ok(film);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }        
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_filmService.DeleteFilm(id))
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }


        [HttpPost]
        public ActionResult<Film> Post([FromBody] JsonElement filmData)
        {
            try
            {
                string titolo = filmData.GetProperty("titolo").GetString();
                int anno = filmData.GetProperty("anno").GetInt32();
                string genere = filmData.GetProperty("genere").GetString();
                string tipologiaString = filmData.GetProperty("tipologia").GetString();
                Tipologia tipologia = Enum.Parse<Tipologia>(tipologiaString, true);

                string creatore = ControllerHelpers.ExtractCreator(filmData, tipologia);
                int numeroEpisodi = filmData.TryGetProperty("numeroEpisodi", out var episodiValue) ? episodiValue.GetInt32() : 0;
                int numeroStagioni = filmData.TryGetProperty("numeroStagioni", out var stagioniValue) ? stagioniValue.GetInt32() : 0;

                Film nuovoFilm = _filmFactory.CreateFilm(tipologia, titolo, anno, genere, creatore, numeroEpisodi, numeroStagioni);
                _filmService.AddFilm(nuovoFilm);
                Response.Headers.Add("Cache-Control", "no-store");
                return CreatedAtAction(nameof(Get), new { id = nuovoFilm.Titolo }, nuovoFilm);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Film> Update(int id, [FromBody] JsonElement filmData)
        {
            try
            {
                string titolo = filmData.GetProperty("titolo").GetString();
                int anno = filmData.GetProperty("anno").GetInt32();
                string genere = filmData.GetProperty("genere").GetString();
                string tipologiaString = filmData.GetProperty("tipologia").GetString();
                Tipologia tipologia = Enum.Parse<Tipologia>(tipologiaString, true);

                string creatore = ControllerHelpers.ExtractCreator(filmData, tipologia);
                int numeroEpisodi = filmData.TryGetProperty("numeroEpisodi", out var episodiValue) ? episodiValue.GetInt32() : 0;
                int numeroStagioni = filmData.TryGetProperty("numeroStagioni", out var stagioniValue) ? stagioniValue.GetInt32() : 0;

                Film updatedFilm = _filmFactory.CreateFilm(tipologia, titolo, anno, genere, creatore, numeroEpisodi, numeroStagioni);
                if (_filmService.UpdateFilm(id, updatedFilm))
                {
                   // ControllerHelpers.SetCacheHeader(Response, 0);
                    return Ok(updatedFilm);
                }
                else
                {
                    return NotFound($"Nessun film trovato con ID {id}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore: {ex.Message}");
            }
        }

    }
}
