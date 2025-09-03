//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Septiembre 2025</date>
//-----------------------------------------------------------------------

using AutoMapper;
using Commons.Dtos.Configurations;
using Commons.Dtos.Domains;
using Infraestructure.Entities;
using Infraestructure.Interfaces;
using Interfaces.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Services.Resources;



namespace Services.Services
{
    public class DepartamentService : IDepartamentService
    {
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitofwork;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper _mapper;

        public DepartamentService(IConfiguration _configuration, IUnitOfWork _unitofwork, IMemoryCache _memoryCache, IMapper mapper)
        {
            configuration = _configuration;
            unitofwork = _unitofwork;
            memoryCache = _memoryCache;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<ResultModel<DepartamentDto[]>> DepartamentList()
        {
            return await memoryCache.GetOrCreateAsync(CachingList.ListDepartaments, async cacheEntry =>
            {
                Cache cacheSettings = configuration.GetSection("Cache").Get<Cache>() ?? new Cache { ExpirationCacheInHours = 1 };
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromHours(cacheSettings.ExpirationCacheInHours));
                cacheEntry.SetPriority(CacheItemPriority.Normal);

                try
                {
                    IEnumerable<Departament> departaments = await unitofwork.GetRepository<Departament>().Get();
                    DepartamentDto[] departamentArray = _mapper.Map<DepartamentDto[]>(departaments);

                    return new ResultModel<DepartamentDto[]>
                    {
                        HasError = false,
                        Data = departamentArray,
                        Messages = "Departaments listed successfully"
                    };
                }
                catch (Exception ex)
                {
                    return new ResultModel<DepartamentDto[]>
                    {
                        HasError = true,
                        Messages = "Technical error listing departaments",
                        Data = Array.Empty<DepartamentDto>(),
                        ExceptionMessage = ex.ToString()
                    };
                }
            });
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> DepartamentAdd(DepartamentDto departamentDto)
        {
            try
            {
                Departament departament = _mapper.Map<Departament>(departamentDto);
                unitofwork.GetRepository<Departament>().Add(departament);

                int rowsAffected = await unitofwork.SaveChangesAsync();
                if (rowsAffected <= 0)
                {
                    return new ResultModel<string>
                    {
                        HasError = true,
                        Messages = "Departament could not be created",
                        Data = string.Empty
                    };
                }

                memoryCache.Remove(CachingList.ListDepartaments);

                return new ResultModel<string>
                {
                    HasError = false,
                    Messages = "Departament successfully created",
                    Data = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<string>
                {
                    HasError = true,
                    Messages = $"Technical error creating departament: {ex.Message}",
                    ExceptionMessage = ex.ToString(),
                    Data = string.Empty
                };
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<DepartamentDto>> GetDepartamentByDepartamentId(int id)
        {
            if (id <= 0)
            {
                return new ResultModel<DepartamentDto>
                {
                    HasError = true,
                    Messages = "Invalid departament ID",
                    ExceptionMessage = string.Empty,
                    Data = null
                };
            }

            try
            {
                ResultModel<DepartamentDto[]>? cachedList = memoryCache.Get<ResultModel<DepartamentDto[]>>(CachingList.ListDepartaments);

                if (cachedList?.Data != null && !cachedList.HasError)
                {
                    DepartamentDto? cachedDepartament = cachedList.Data.FirstOrDefault(x => x.DepartamentId == id);

                    return new ResultModel<DepartamentDto>
                    {
                        HasError = false,
                        Messages = cachedDepartament != null ? "Departament found" : "Departament not found",
                        ExceptionMessage = string.Empty,
                        Data = cachedDepartament
                    };
                }

                Departament? departament = await unitofwork.GetRepository<Departament>()
                    .Find(d => d.DepartamentId == id);

                if (departament == null)
                {
                    return new ResultModel<DepartamentDto>
                    {
                        HasError = false,
                        Messages = "Departament not found",
                        ExceptionMessage = string.Empty,
                        Data = null
                    };
                }

                DepartamentDto departamentDto = _mapper.Map<DepartamentDto>(departament);
                return new ResultModel<DepartamentDto>
                {
                    HasError = false,
                    Messages = "Departament found successfully",
                    ExceptionMessage = string.Empty,
                    Data = departamentDto
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<DepartamentDto>
                {
                    HasError = true,
                    Messages = $"Technical error retrieving departament: {ex.Message}",
                    ExceptionMessage = ex.ToString(),
                    Data = null
                };
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> DepartamentUpdate(DepartamentDto departamentDto)
        {
            try
            {
                if (departamentDto?.DepartamentId <= 0)
                    return new ResultModel<string>
                    {
                        HasError = true,
                        Messages = "Invalid departament ID",
                        ExceptionMessage = string.Empty,
                        Data = null
                    };

                if (string.IsNullOrWhiteSpace(departamentDto.Name))
                    return new ResultModel<string>
                    {
                        HasError = true,
                        Messages = "Departament name is required",
                        ExceptionMessage = string.Empty,
                        Data = null
                    };

                Departament? departament = await unitofwork.GetRepository<Departament>()
                    .Find(d => d.DepartamentId == departamentDto.DepartamentId);

                if (departament == null)
                    return new ResultModel<string>
                    {
                        HasError = false,
                        Messages = "Departament not found",
                        ExceptionMessage = string.Empty,
                        Data = null
                    };

                bool hasChanges = departament.Name != departamentDto.Name ||
                                  departament.State != departamentDto.State;

                if (!hasChanges)
                    return new ResultModel<string>
                    {
                        HasError = false,
                        Messages = "No changes detected, departament is up to date",
                        ExceptionMessage = string.Empty,
                        Data = null
                    };

                if (departament.Name != departamentDto.Name)
                {
                    IEnumerable<Departament> existingNames = await unitofwork.GetRepository<Departament>()
                        .Get(d => d.Name.ToLower() == departamentDto.Name.ToLower() &&
                                  d.DepartamentId != departamentDto.DepartamentId);

                    bool nameExists = existingNames.Any();

                    if (nameExists)
                        return new ResultModel<string>
                        {
                            HasError = true,
                            Messages = "A departament with this name already exists",
                            ExceptionMessage = string.Empty,
                            Data = null
                        };
                }

                departament.Name = departamentDto.Name;
                departament.State = departamentDto.State;

                int rowsAffected = await unitofwork.SaveChangesAsync();
                if (rowsAffected <= 0)
                    return new ResultModel<string>
                    {
                        HasError = true,
                        Messages = "Failed to update departament",
                        ExceptionMessage = string.Empty,
                        Data = null
                    };

                memoryCache.Remove(CachingList.ListDepartaments);

                return new ResultModel<string>
                {
                    HasError = false,
                    Messages = "Departament updated successfully",
                    ExceptionMessage = string.Empty,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<string>
                {
                    HasError = true,
                    Messages = $"Technical error updating departament: {ex.Message}",
                    ExceptionMessage = ex.ToString(),
                    Data = null
                };
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> DepartamentDelete(int departamentId)
        {
            try
            {
                if (departamentId <= 0)
                    return new ResultModel<string>
                    {
                        HasError = true,
                        Messages = "Invalid departament ID",
                        ExceptionMessage = string.Empty,
                        Data = null
                    };

                Departament departament = new Departament { DepartamentId = departamentId };

                unitofwork.GetRepository<Departament>().Remove(departament);

                int rowsAffected = await unitofwork.SaveChangesAsync();

                if (rowsAffected == 0)
                {
                    return new ResultModel<string>
                    {
                        HasError = false,
                        Messages = "Departament not found",
                        ExceptionMessage = string.Empty,
                        Data = null
                    };
                }

                memoryCache.Remove(CachingList.ListDepartaments);

                return new ResultModel<string>
                {
                    HasError = false,
                    Messages = "Departament deleted successfully",
                    ExceptionMessage = string.Empty,
                    Data = null
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultModel<string>
                {
                    HasError = false,
                    Messages = "Departament was already deleted or does not exist",
                    ExceptionMessage = string.Empty,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<string>
                {
                    HasError = true,
                    Messages = $"Technical error deleting departament: {ex.Message}",
                    ExceptionMessage = ex.ToString(),
                    Data = null
                };
            }
        }
    }
}