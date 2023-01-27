using API.Mapper;
using DTO;
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
            if(champ.Count() == 0)
            {
                return NoContent();
            }
            IList<ChampionDto> championDtos= new List<ChampionDto>();
            foreach (var champion in champ)
            {
                championDtos.Add(champion.ToDto());
            }
            return Ok(championDtos);
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
            IList<ChampionDto> championDtos = new List<ChampionDto>();
            foreach (var champion in champ)
            {
                championDtos.Add(champion.ToDto());
            }
            return Ok(championDtos);
        }

        // POST api/<ChampionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ChampionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChampionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
