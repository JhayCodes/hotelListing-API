using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Country> Countries => Countries ?? new GenericRepository<Country>(_context);

        public IGenericRepository<Hotel> Hotels => _hotels ?? new GenericRepository<Hotel>(_context);

        //free up memory
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
