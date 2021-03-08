using System.Collections.Generic;
using System.Linq;
using domain;
using domain.NOAAStationAggregate;

namespace repository
{
    public class NOAAStationRepository : GenericRepository<NOAAStation>, INOAAStationRepository
    {
        public NOAAStationRepository(WebApiDbContext context) : base(context)
        {
            
        }

        //place data retrieval methods here

    }
}