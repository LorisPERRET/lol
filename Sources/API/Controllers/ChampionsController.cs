using DTO_API;
using DTO_API.Mapper;
using Microsoft.AspNetCore.Mvc;
using Model;
using StubLib;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionsController : ControllerBase
    {
        private readonly IDataManager _dataManager;
        public ChampionsController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        // GET: api/<ChampionsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            IEnumerable<Champion> champ = await _dataManager.ChampionsMgr.GetItems(0, await _dataManager.ChampionsMgr.GetNbItems());
            if (champ.Count() == 0)
            {
                return NoContent();
            }
            return Ok(champ.ToDtos());
        }

        // GET api/<ChampionsController>/5
        [HttpGet("{nom}")]
        public async Task<IActionResult> Get(string nom)
        {
            IEnumerable<Champion> champ = await _dataManager.ChampionsMgr.GetItemsByName(nom, 0, await _dataManager.ChampionsMgr.GetNbItemsByName(nom), null);
            if (champ.Count() == 0)
            {
                return NoContent();
            }
            return Ok(champ.ToDtos());
        }

        // POST api/<ChampionsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChampionDto champion)
        {
            if(await _dataManager.ChampionsMgr.GetNbItemsByName(champion.Name) == 0)
            {
                await _dataManager.ChampionsMgr.AddItem(champion.ToChampion());
                return CreatedAtAction(nameof(Get), champion.Name, champion);
            }
            else
            {
                return BadRequest();
            }
            
            
        }

        // PUT api/<ChampionsController>/5
        [HttpPut("{nom}")]
        public async Task<IActionResult> Put(string nom, [FromBody] ChampionDto champion)
        {
            var oldChampion = await _dataManager.ChampionsMgr.GetItemsByName(nom, 0, await _dataManager.ChampionsMgr.GetNbItemsByName(nom));
            var newChampion = await _dataManager.ChampionsMgr.UpdateItem(oldChampion.FirstOrDefault(),champion.ToChampion());
            return Ok(newChampion.ToDto());
            
        }

        // DELETE api/<ChampionsController>/5
        [HttpDelete("{nom}")]
        public async Task<IActionResult> Delete(string nom)
        {
            IEnumerable<Champion> champ = await _dataManager.ChampionsMgr.GetItemsByName(nom, 0,
                await _dataManager.ChampionsMgr.GetNbItemsByName(nom), null);
            
            bool res = await _dataManager.ChampionsMgr.DeleteItem(champ.FirstOrDefault());
            if (res)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
