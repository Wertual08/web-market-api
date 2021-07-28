using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Contexts;
using Api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api.Repositories {
    public class RecordsRepository : AbstractRepository<Record> {
        public RecordsRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Record> FindAsync(long id) {
            return (
                from record in DbContext.Records
                where record.Id == id
                select record
            ).FirstOrDefaultAsync();
        }

        public Task<Record> FindAsync(string identy) {
            var identifier = Guid.Parse(identy);
            return (
                from record in DbContext.Records
                where record.Identifier == identifier
                select record
            ).FirstOrDefaultAsync();
        }

        public Task<List<Record>> ListAsync(int skip, int take) {
            return (
                from record in DbContext.Records
                orderby record.Id
                select record
            ).Skip(skip).Take(take).ToListAsync();
        }

        public void Create(Record record) {
            DbContext.Records.Add(record);
        }

        public void Delete(Record record) {
            DbContext.Records.Remove(record);
        }
    }
}