using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelListing.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //Dependency Injection
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }


        //implementing interface
        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
                
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db; //gets all records in that table
            if(includes != null)
            {
                foreach(var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.AsNoTracking()
                .FirstOrDefaultAsync(expression);

        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db; //gets all records in that table

            //check for expression, eg:(where(i => i.id))
            if(expression != null)
            {
               query = query.Where(expression);
            }
            

            //check for includes
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            //checks for order
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking()
                .ToListAsync();
        }


        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
             await _db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
           _db.Attach(entity); //check if there's a difference btn this and what's in the db
            _context.Entry(entity).State = EntityState.Modified; //if modified then update
        }
    }
}
