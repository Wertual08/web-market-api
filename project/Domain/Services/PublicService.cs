using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.Domain.Services.Result;

namespace Api.Domain.Services {
    public class PublicService {
        private readonly PublicRepository PublicRepository;

        public PublicService(PublicRepository publicRepository)
        {
            PublicRepository = publicRepository;
        }

        public async Task<ServiceResult<List<Record>>> GetMainSlidesAsync() {
            var result = await PublicRepository.ListMainSlidesAsync();

            return result;
        }
    }
}