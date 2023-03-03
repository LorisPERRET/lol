using DTO_API;
using DTO_API.Mapper;
using Microsoft.AspNetCore.Mvc;
using Model;
using StubLib;
using System;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionsController : ControllerBase
    {
        private readonly IDataManager _dataManager;
        private readonly ILogger _logger;

        public ChampionsController(IDataManager dataManager, ILogger logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        // GET: api/<ChampionsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Champion> champ = await _dataManager.ChampionsMgr.GetItems(0, 
                                                    await _dataManager.ChampionsMgr.GetNbItems());
                if (champ.Count() == 0)
                {
                    _logger.LogInformation("Aucun champion n'a été trouvé");
                    return NoContent();
                }
                return Ok(champ.ToDtos());
            } catch (Exception)
            {
                _logger.LogWarning("Une erreur est survenue en lien avec le serveur");
                return StatusCode(((int)HttpStatusCode.InternalServerError), new { message = "Erreur interne du serveur." });
            }
            
        }

        // GET api/<ChampionsController>/5
        [HttpGet("{nom}")]
        public async Task<IActionResult> Get(string nom)
        {
            try
            {
                IEnumerable<Champion> champ = await _dataManager.ChampionsMgr.GetItemsByName(nom, 0, 
                                                    await _dataManager.ChampionsMgr.GetNbItemsByName(nom), null);
                if (champ.Count() == 0)
                {
                    _logger.LogInformation("Aucun champion n'a été trouvé");
                    return NoContent();
                }
                return Ok(champ.Single().ToDto());
            } catch (Exception)
            {
                _logger.LogWarning("Une erreur est survenue en lien avec le serveur");
                return StatusCode(((int) HttpStatusCode.InternalServerError), new { message = "Erreur interne du serveur." });
            }
        }

        // POST api/<ChampionsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChampionDto champion)
        {
            try
            {
                if (await _dataManager.ChampionsMgr.GetNbItemsByName(champion.Name) == 0)
                {
                    await _dataManager.ChampionsMgr.AddItem(champion.ToChampion());
                    return CreatedAtAction(nameof(Get), champion.Name, champion);
                }
                _logger.LogInformation("Le champion voulu est déja existant");
                return BadRequest();
            } catch (Exception)
            {
                _logger.LogWarning("Une erreur est survenue en lien avec le serveur");
                return StatusCode(((int)HttpStatusCode.InternalServerError), new { message = "Erreur interne du serveur." });
            }
        }

        // PUT api/<ChampionsController>/5
        [HttpPut("{nom}")]
        public async Task<IActionResult> Put(string nom, [FromBody] ChampionDto champion)
        {
            try
            {
                if (!nom.Equals(champion.Name))
                {
                    _logger.LogInformation("Vous ne pouvez pas modifier le nom d'un champion");
                    return BadRequest();
                }
                var oldChampion = await _dataManager.ChampionsMgr.GetItemsByName(nom, 0, 
                                        await _dataManager.ChampionsMgr.GetNbItemsByName(nom));
                var newChampion = await _dataManager.ChampionsMgr.UpdateItem(oldChampion.FirstOrDefault(),champion.ToChampion());
                return Ok(newChampion.ToDto());
            } catch (Exception)
            {
                _logger.LogWarning("Une erreur est survenue en lien avec le serveur");
                return StatusCode(((int)HttpStatusCode.InternalServerError), new { message = "Erreur interne du serveur." });
            }
        }

        // DELETE api/<ChampionsController>/5
        [HttpDelete("{nom}")]
        public async Task<IActionResult> Delete(string nom)
        {
            try
            {
                var champ = await _dataManager.ChampionsMgr.GetItemsByName(nom, 0, await _dataManager.ChampionsMgr.GetNbItemsByName(nom));
            
                bool res = await _dataManager.ChampionsMgr.DeleteItem(champ.FirstOrDefault());
                if (res)
                {
                    return Ok(nom);
                }
                _logger.LogInformation("Le champion n'a pas été supprimer");
                return BadRequest();
            } catch (Exception)
            {
                _logger.LogWarning("Une erreur est survenue en lien avec le serveur");
                return StatusCode(((int)HttpStatusCode.InternalServerError), new { message = "Erreur interne du serveur." });
            }
        }
    }
}
