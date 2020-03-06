using StartupHouse.Database.Entities;
using StartupHouse.Database.Interfaces;

namespace StartupHouse.Database.Repository
{
    public class ContextScope : IContextScope
    {
        private readonly ShDbContext _context;

        public ContextScope(ShDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
