using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;

namespace Api.Domain.Services {
    public class ProfileService {
        private readonly ProfileRepository ProfileRepository;

        public ProfileService(ProfileRepository profileRepository)
        {
            ProfileRepository = profileRepository;
        }

        public async Task<User> GetAsync(long id) {
            return await ProfileRepository.FindAsync(id);
        }

        public async Task<(User, string)> SetAsync(
            long id,
            string login,
            string email,
            string phone,
            string name,
            string surname
        ) {
            var conflict = await ProfileRepository.FindAsync(
                id,
                email,
                phone
            );
            if (conflict is not null) {
                if (conflict.Email == email) {
                    return (null, nameof(conflict.Email));
                }
                if (conflict.Phone == phone) {
                    return (null, nameof(conflict.Phone));
                }
            }

            var result = await ProfileRepository.FindAsync(id);

            if (result is null) {
                return (null, null);
            } 

            result.Email = email;
            result.Phone = phone;
            result.Name = name;
            result.Surname = surname;

            await ProfileRepository.SaveAsync();

            return (result, null);
        }
    }
}