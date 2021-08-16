using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;

namespace Api.Services {
    public class ProfileService {
        private readonly ProfileRepository ProfileRepository;

        public ProfileService(ProfileRepository profileRepository)
        {
            ProfileRepository = profileRepository;
        }

        public async Task<User> GetAsync(long id) {
            return await ProfileRepository.FindAsync(id);
        }

        public async Task<User> PutAsync(
            long id,
            string login,
            string email,
            string phone,
            string name,
            string surname
        ) {
            // TODO: check for conflicts
            var result = await ProfileRepository.FindAsync(id);

            if (result is null) {
                return null;
            } 

            result.Login = login;
            result.Email = email;
            result.Phone = phone;
            result.Name = name;
            result.Surname = surname;

            await ProfileRepository.SaveAsync();

            return result;
        }
    }
}