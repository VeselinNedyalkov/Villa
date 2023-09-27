﻿using MagicVilla_web.Models.DTO;

namespace MagicVilla_web.Services.Contacts
{
    public interface IVillaSurvice
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaCreateDTO dto , string token);
        Task<T> UpdateAsync<T>(VillaUpdateDTO dto , string token);
        Task<T> DeleteAsync<T>(int id , string token);
    }
}
