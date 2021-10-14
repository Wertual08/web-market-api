using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.Domain.Services.Result;

namespace Api.Domain.Services {
    public class AdminPublicService {
        private readonly PublicRepository PublicRepository;

        public AdminPublicService(PublicRepository publicRepository)
        {
            PublicRepository = publicRepository;
        }

        public async Task UpdateMainSlidesAsync(List<long> recordIds) {
            PublicRepository.ClearMainSlides();
            foreach (var recordId in recordIds) {
                PublicRepository.Create(new MainSlide {
                    RecordId = recordId,
                    //Name = "",
                });
            }

            await PublicRepository.SaveAsync();
        }
    }
}