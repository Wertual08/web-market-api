using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.Domain.Services.Result;

namespace Api.Domain.Services {
    public class AdminSectionsService {
        private readonly AdminSectionsRepository AdminSectionsRepository;

        public AdminSectionsService(AdminSectionsRepository adminSectionsRepository)
        {
            AdminSectionsRepository = adminSectionsRepository;
        }
        
        public async Task<IEnumerable<Section>> GetAsync() {
            var sections = await AdminSectionsRepository.ListAsync();

            return from section in sections where section.SectionId == null select section;
        }

        public async Task<Section> GetAsync(long id) {
            return await AdminSectionsRepository.FindAsync(id);
        }

        public async Task<Section> PostAsync(long? sectionId, long? recordId, string name) {
            var result = new Section {
                SectionId = sectionId,
                RecordId = recordId,
                Name = name,
            };
            AdminSectionsRepository.Create(result);
            await AdminSectionsRepository.SaveAsync();

            return result;
        }

        public async Task<ServiceResult<Section>> PutAsync(long id, long? sectionId, long? recordId, string name) {
            var result = await AdminSectionsRepository.FindAsync(id);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            } 

            if (!await AdminSectionsRepository.CheckParentValidAsync(id, sectionId)) {
                return new ServiceResult<Section>(ServiceResultStatus.BadRequest, "Invalid SectionId");
            }

            result.SectionId = sectionId;
            result.RecordId = recordId;
            result.Name = name;
            result.UpdatedAt = DateTime.Now;

            await AdminSectionsRepository.SaveAsync();

            return result;
        }

        public async Task<Section> DeleteAsync(long id) {
            var result = await AdminSectionsRepository.FindAsync(id);

            if (result is null) {
                return null;
            } 

            AdminSectionsRepository.Delete(result);
            await AdminSectionsRepository.SaveAsync();

            return result;
        }
    }
}