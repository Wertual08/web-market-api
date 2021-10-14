using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class PublicRepository : AbstractRepository<Category> {
        public PublicRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<List<Record>> ListMainSlidesAsync() {
            return (
                from mainSlide in DbContext.MainSlides
                join record in DbContext.Records
                on mainSlide.RecordId equals record.Id
                select record
            ).ToListAsync();
        }

        public void ClearMainSlides() {
            DbContext.MainSlides.RemoveRange(DbContext.MainSlides);
        }

        public void Create(MainSlide mainSlide) {
            DbContext.MainSlides.Add(mainSlide);
        }
    }
}