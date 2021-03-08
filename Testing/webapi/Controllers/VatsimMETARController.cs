using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using domain;
using domain.VatsimMETARAggregate;
using repository;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatsimMETARController : ControllerBase
    {
        private readonly ILogger<VatsimMETARController> _logger;
        private readonly IUnitOfWork _unitOfWork;        

        public VatsimMETARController(ILogger<VatsimMETARController> logger,
                                     IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: api/VatsimMETAR
        [HttpGet]
        public async Task<IEnumerable<VatsimMETAR>> GetVatsimMETAR()
        {
            _logger.LogInformation("VatsimMETAR Called");
            return await _unitOfWork.METARs.GetAll();
        }

        // GET: api/VatsimMETAR/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VatsimMETAR>> GetVatsimMETAR(string id)
        {
            var vatsimMETAR = await _unitOfWork.METARs.Get(id);

            if (vatsimMETAR == null)
            {
                return NotFound();
            }

            return vatsimMETAR;
        }

        // PUT: api/VatsimMETAR/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutVatsimMETAR(string id, VatsimMETAR vatsimMETAR)
        {

            if (id != vatsimMETAR.ICAO)
            {
                return BadRequest();
            }
            else {
                _unitOfWork.METARs.Update(vatsimMETAR);
            }
            return NoContent();
        }

        // POST: api/VatsimMETAR
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<VatsimMETAR>> PostVatsimMETAR(VatsimMETAR vatsimMETAR)
        {
            try
            {
                await _unitOfWork.METARs.Add(vatsimMETAR);
            }
            catch (Exception)
            {
                if (await VatsimMETARExists(vatsimMETAR.ICAO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVatsimMETAR", new { id = vatsimMETAR.RetreivedTimeStamp }, vatsimMETAR);            
        }

        // DELETE: api/VatsimMETAR/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VatsimMETAR>> DeleteVatsimMETAR(string id)
        {

            var vatsimMetar = await _unitOfWork.METARs.Get(id);
            
            if (vatsimMetar == null)
            {
                return NotFound();
            }

            //do deletion
            _unitOfWork.METARs.Delete(vatsimMetar);

            return vatsimMetar;
        }

        private async Task<bool> VatsimMETARExists(string id)
        {
            return await _unitOfWork.METARs.Get(id) != null;
        }
    }
}
