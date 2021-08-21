using Api.Contexts;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class SectionsRepository : AbstractRepository<Section> {
        public SectionsRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Section> FindAsync(long id) {
            return (
                from section in DbContext.Sections
                where section.Id == id
                select section
            ).Include(x => x.Sections).FirstOrDefaultAsync();
        }

        public Task<List<Section>> ListAsync() {
            return (
                from section in DbContext.Sections 
                orderby section.Id
                select section
            ).ToListAsync();
        }
    }
}