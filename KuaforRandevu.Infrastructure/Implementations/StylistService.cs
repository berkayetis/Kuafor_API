using KuaforRandevu.Application.Dtos;
using KuaforRandevu.Application.Exceptions;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Cache.Contracts;
using KuaforRandevu.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using KuaforRandevu.Core.Interfaces;
using KuaforRandevu.Core.Parameters;

namespace Infrastructure.Implementations
{
    public class StylistService : IStylistService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _cache;
        private const string AllKey = "stylists:all";

        public StylistService(IRepositoryManager stylistRepository, IMapper mapper, IRedisCacheService cache)
        {
            _repositoryManager = stylistRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<StylistDto> CreateAsync(CreateStylistDto createStylistDto)
        {
            var stylist = _mapper.Map<Stylist>(createStylistDto);
            await _repositoryManager.StylistRepo.CreateStylist(stylist);

            // Değişiklikleri veritabanına commit et
            await _repositoryManager.SaveAsync();

            try
            {
                // Yeni eklendiği için önbelleği temizleyelim
                await _cache.RemoveAsync(AllKey);
            }
            catch (Exception ex)
            {
                throw new Exception(AllKey + " cache is not available", ex);
            }

            var stylistDto = _mapper.Map<StylistDto>(stylist);
            return stylistDto;
        }

        public async Task<IEnumerable<Stylist>> GetAllAsync()
        {
            // Cache’den oku
            try
            {
                var cached = await _cache.GetAsync<IEnumerable<Stylist>>(AllKey);
                if (cached is not null)
                {
                    return cached;
                }
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Redis cache unavailable, falling back to DB");

                throw new Exception(AllKey + " cache is not available", ex);    
            }

            // get all from database
            var stylistList = await _repositoryManager.StylistRepo.GetAllStylistsAsync();

            // set cache
            await _cache.SetAsync(AllKey, stylistList, TimeSpan.FromMinutes(5));

            return stylistList;
        }

        public async Task<StylistDto?> GetByIdAsync(Guid id)
        {
            var stylist = await _repositoryManager.StylistRepo.GetStylistByIdAsync(id);

            if (stylist == null)
                throw new NotFoundException($"Stylist '{id}' is not found!");

            var stylistDto = _mapper.Map<StylistDto>(stylist);
            return stylistDto;
        }

        public async Task<(IEnumerable<Stylist> Stylists, int TotalCount)> GetAllPagedAsync(PaginationParams paginationParams)
        {
            // Cache’den oku
            try
            {
                var cached = await _cache.GetAsync<(IEnumerable<Stylist> Stylists, int TotalCount)>(AllKey);
                if (cached.Stylists is not null)
                {
                    return cached;
                }
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Redis cache unavailable, falling back to DB");

                throw new Exception(AllKey + " cache is not available", ex);
            }

            var result = await _repositoryManager.StylistRepo.GetAllPagedStylistsAsync(paginationParams);
            return result;
        }
    }
}
