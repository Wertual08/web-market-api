using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;

namespace Api.Services {
    public class SectionsService {
        private readonly SectionsRepository SectionsRepository;

        public SectionsService(SectionsRepository sectionsRepository)
        {
            SectionsRepository = sectionsRepository;
        }
        
        public async Task<IEnumerable<Section>> GetAsync() {
            var sections = await SectionsRepository.ListAsync();

            return from section in sections where section.SectionId == null select section;
        }
    }
}