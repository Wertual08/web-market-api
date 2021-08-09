using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;

namespace Api.Services {
    public class AdminSectionsService {
        private readonly AdminSectionsRepository AdminSectionsRepository;

        public AdminSectionsService(AdminSectionsRepository adminSectionsRepository)
        {
            AdminSectionsRepository = adminSectionsRepository;
        }
        
        public async Task<IEnumerable<Section>> GetAsync() {
            var sections = await AdminSectionsRepository.ListAsync();

            List<Section> build(List<Section> list, long? sectionId) {
                List<Section> result = new ();
                foreach (var item in list) {
                    if (item.SectionId == sectionId) {
                        item.Sections = build(list, item.Id);
                        result.Add(item);
                    }
                }
                return result;
            }

            return build(sections, null);
        }

        public async Task<Section> GetAsync(long id) {
            return await AdminSectionsRepository.FindAsync(id);
        }

        public async Task<Section> PostAsync(long? sectionId, string name) {
            var result = new Section {
                SectionId = sectionId,
                Name = name,
            };
            AdminSectionsRepository.Create(result);
            await AdminSectionsRepository.SaveAsync();

            return result;
        }

        public async Task<Section> PutAsync(long id, long? sectionId, string name) {
            var result = await AdminSectionsRepository.FindAsync(id);

            if (result is null) {
                return null;
            } 

            result.SectionId = sectionId;
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