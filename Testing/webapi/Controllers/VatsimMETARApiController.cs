using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using domain;
using domain.VatsimMETARAggregate;
using repository;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatsimMETARApiController : ControllerBase
    {

        private readonly WebApiDbContext _context;

        public VatsimMETARApiController(WebApiDbContext context)
        {
            _context = context;
        }

        // GET: api/VatsimMETARApi/KDFW
        [HttpGet("{icao}")]
        public async Task<ActionResult<VatsimMETAR>> GetVatsimMetarFromICAO(string icao) {
            var vatsimMETAR = await VatsimMETARHelper.GetVatsimMETARFromIDAsync(icao);

            if(vatsimMETAR == null)
            {
                return new VatsimMETAR();
            } else {
                return vatsimMETAR;
            }
        }
    }
}