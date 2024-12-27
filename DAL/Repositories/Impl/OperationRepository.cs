using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces;

namespace Catalog.DAL.EF.Entities.Repositories.Impl.Interfaces
{
    public class OperationRepository : BaseRepository<Operation>, IOperationRepository
    {
        private readonly ApplicationDbContext _context;

        public OperationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Operation> FindByName(string name)
        {

            return _context.Set<Operation>().Where(o => o.OperationName.Contains(name)).ToList();
        }
    }

}
