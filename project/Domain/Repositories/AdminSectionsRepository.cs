using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class AdminSectionsRepository : AbstractRepository<Section> {
        public AdminSectionsRepository(ApplicationDbContext dbContext) : base(dbContext) {
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

        public void Create(Section section) {
            DbContext.Sections.Add(section);
        }

        public void Delete(Section section) {
            DbContext.Sections.Remove(section);
        }
    }
}