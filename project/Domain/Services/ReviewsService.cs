using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.Domain.Services.Result;

namespace Api.Domain.Services {
    public class ReviewsService {
        private readonly ReviewsRepository ReviewsRepository;

        public ReviewsService(ReviewsRepository reviewsRepository) {
            ReviewsRepository = reviewsRepository;
        }
        
        public async Task<ServiceResult<IEnumerable<Review>>> ListAsync(int page) {
            int pageSize = 32;
            var reviews = await ReviewsRepository.ListAsync(page * pageSize, pageSize);

            return reviews;
        }

        public async Task<ServiceResult<Review>> GetAsync(long id) {
            var result = await ReviewsRepository.FindAsync(id);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            }

            return result;
        }

        public async Task<ServiceResult<Review>> CreateAsync(
            long? userId, 
            int? grade,
            string name,
            string email,
            string description
        ) {
            var review = new Review {
                UserId = userId,
                Grade = grade,
                Name = name,
                Email = email,
                Description = description,
            };

            ReviewsRepository.Create(review);
            await ReviewsRepository.SaveAsync();

            return review;
        }

        public async Task<ServiceResult<Review>> DeleteAsync(long id) {
            var result = await ReviewsRepository.FindAsync(id);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            } 

            ReviewsRepository.Delete(result);

            await ReviewsRepository.SaveAsync();

            return result;
        }
    }
}