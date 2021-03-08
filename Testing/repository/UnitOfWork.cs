using System;
using domain;
using domain.NOAAStationAggregate;
using domain.VatsimMETARAggregate;

namespace repository
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly WebApiDbContext _context;
        public INOAAStationRepository Stations { get; }

        public IVatsimMETARRepository METARs { get; }

        public UnitOfWork(WebApiDbContext webapiDbContext, 
                          INOAAStationRepository stationsRepository, 
                          IVatsimMETARRepository metarsRepository)
        {
            this._context = webapiDbContext;
            this.Stations = stationsRepository;
            this.METARs = metarsRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
