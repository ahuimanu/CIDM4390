using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using domain;
using domain.VatsimMETARAggregate;

namespace repository
{
    public class VatsimMETARRepository : GenericRepository<VatsimMETAR>, IVatsimMETARRepository
    {

        public VatsimMETARRepository(WebApiDbContext context) : base(context){}
        //place data retrieval methods here
        // public async Task<IEnumerable<VatsimMETAR>> GetAll() {
        //     return await _context.VatsimMETARs;
        // }
    }
}