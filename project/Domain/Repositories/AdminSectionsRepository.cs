using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
            ).Include(x => x.Sections).Include(x => x.Record).FirstOrDefaultAsync();
        }

        public Task<List<Section>> ListAsync() {
            return (
                from section in DbContext.Sections 
                orderby section.Id
                select section
            ).Include(x => x.Record).ToListAsync();
        }

        public void Create(Section section) {
            DbContext.Sections.Add(section);
        }

        public void Delete(Section section) {
            DbContext.Sections.Remove(section);
        }

        public async Task<bool> CheckParentValidAsync(long id, long? parentId) {
            if (parentId is null) {
                return true;
            }
            if (parentId == id) { 
                return false;
            }

            var child = await DbContext.Sections.FromSqlRaw(@"
            WITH RECURSIVE section_tree AS (
                SELECT id, section_id
                FROM sections    
                WHERE sections.id = {0}         
                UNION ALL
                SELECT int.id, int.section_id
                FROM sections AS int    
                INNER JOIN section_tree AS prev 
                ON prev.id = int.section_id
            )
            SELECT 
                id as id, 
                '' as name, 
                '' as created_at, 
                '' as updated_at, 
                null AS record_id, 
                null AS section_id 
            FROM section_tree
            ", id).ToListAsync();

            return !child.Exists(section => section.Id == parentId);
        }
    }
}