using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using domain;
using domain.NOAAStationAggregate;
using repository;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NOAAStationsController : ControllerBase
    {
        private readonly ILogger<NOAAStationsController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public NOAAStationsController(ILogger<NOAAStationsController> logger,
                                      IUnitOfWork unitOfWork)

        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: api/NOAAStations
        [HttpGet]
        public async Task<IEnumerable<NOAAStation>> GetStations()
        {
            _logger.LogInformation("NOAAStation Called");
            return await _unitOfWork.Stations.GetAll();
        }

        // GET: api/NOAAStations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NOAAStation>> GetNOAAStation(string id)
        {
            var noaaStation = await _unitOfWork.Stations.Get(id);
            //var mETARStation = await _context.Stations.FindAsync(id);

            if (noaaStation == null)
            {
                return NotFound();
            }

            return noaaStation;
        }

        // PUT: api/NOAAStations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutNOAAStation(string id, NOAAStation noaaStation)
        {
            if (id != noaaStation.ICAO)
            {
                return BadRequest();
            }
            else {
                _unitOfWork.Stations.Update(noaaStation);
            }
            return NoContent();
        }

        // POST: api/NOAAStations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NOAAStation>> PostNOAAStation(NOAAStation noaaStation)
        {
            try
            {
                await _unitOfWork.Stations.Add(noaaStation);
            }
            catch (Exception)
            {
                if (await NOAAStationExists(noaaStation.ICAO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMETARStation", new { id = noaaStation.ICAO }, noaaStation);
        }

        // DELETE: api/NOAAStations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NOAAStation>> DeleteNOAAStation(string id)
        {
            var noaaStation = await _unitOfWork.Stations.Get(id);
            
            if (noaaStation == null)
            {
                return NotFound();
            }

            //do deletion
            _unitOfWork.Stations.Delete(noaaStation);

            return noaaStation;
        }

        private async Task<bool> NOAAStationExists(string id)
        {
            return await _unitOfWork.Stations.Get(id) != null;
        }
    }
}
