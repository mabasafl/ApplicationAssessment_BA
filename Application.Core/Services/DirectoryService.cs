using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Helpers.Interfaces;
using Application.Core.Helpers;
using Application.Core.Repositories.Interfaces;
using Application.DataTransfer.Dtos.Core;
using Application.Data.Models.Core;
using AutoMapper;
using Application.Core.Interfaces;
using System.Linq.Expressions;

namespace Application.Core.Services
{
    public class DirectoryService<Entity, Dto> : IDirectoryService<Entity, Dto> where Entity : class
    {
        private readonly IBaseRepository<Entity> _repository;
        private readonly IResponseMessageHelper _responseMessageHelper;
        private readonly IMapper _mapper;
        private readonly INewInstanceHelper _instanceHelper;

        public DirectoryService(IBaseRepository<Entity> repository, IMapper mapper, INewInstanceHelper instanceHelper)
        {
            _repository = repository;
            _responseMessageHelper = new ResponseMessageHelper();
            _mapper = mapper;
            _instanceHelper = instanceHelper;
        }
        public async Task<ResponseDto> AddDirectoryAsync(Dto directoryData)
        {
            ResponseDto response = new ResponseDto();

            try
            {
                Entity directoryEntity = _mapper.Map<Dto, Entity>(directoryData);

                bool result = await _repository.AddAsync(directoryEntity);

                if (!result)
                {
                    response = await _responseMessageHelper.ResponseMessage(result,
                        new List<string[]> { new[] { $"adding {typeof(Entity).Name} was not successful", "Please try again later" } });
                }

                response = await _responseMessageHelper.ResponseMessage(result,
                    new List<string[]> { new[] { $"adding {typeof(Entity).Name} was successful", "Thank You" } });

                return response;
            }
            catch (Exception e)
            {
                return response;
            }
        }

        public async Task<ResponseDto> DeleteDirectoryAsync(Entity directoryData)
        {
            ResponseDto response = new ResponseDto();

            try
            {
                bool result = await _repository.DeleteAsync(directoryData);

                if (!result)
                {
                    response = await _responseMessageHelper.ResponseMessage(result,
                        new List<string[]>
                        {
                            new[] { $"deleting {typeof(Entity).Name} was not successful", "Please try again later" }
                        });
                }

                response = await _responseMessageHelper.ResponseMessage(result, new List<string[]> { new[] { $"deleting {typeof(Entity).Name} was successful", "Thank you!" } });

                return response;
            }
            catch (Exception e)
            {
                return response;
            }
        }

        public async Task<List<Dto>> GetAllDirectoryAsync()
        {
            List<Dto> directoryData = new List<Dto>();

            try
            {
                List<Entity> result = await _repository.GetAllAsync();

                if (result.Count == 0) return directoryData;

                directoryData = _mapper.Map<List<Entity>, List<Dto>>(result);

                return directoryData;
            }
            catch (Exception e)
            {
                return directoryData;
            }

        }

        public async Task<Dto> GetDirectoryAsync(int id)
        {
            Dto directoryData = _instanceHelper.CreateInstance<Dto>();

            try
            {
                Entity result = await _repository.GetAsync(id);

                if (result == null) return directoryData;

                directoryData = _mapper.Map<Entity, Dto>(result);

                return directoryData;
            }
            catch (Exception e)
            {
                return directoryData;
            }


        }

        public async Task<Dto> GetDirectoryByNameAsync(Expression<Func<Entity, bool>> predicate)
        {
            Dto directoryData = _instanceHelper.CreateInstance<Dto>();
            
            try
            {
                Entity result = await _repository.GetByNameAsync(predicate);

                if (result == null) return directoryData;

                directoryData = _mapper.Map<Entity, Dto>(result);

                return directoryData;
            }
            catch (Exception e)
            {
                return directoryData;
            }
        }

        public async Task<ResponseDto> UpdateDirectoryAsync(Entity directoryData)
        {
            ResponseDto response = new ResponseDto();

            try
            {
                bool result = await _repository.UpdateAsync(directoryData);

                if (!result)
                {
                    response = await _responseMessageHelper.ResponseMessage(result,
                        new List<string[]> { new[] { $"updating {typeof(Entity).Name} was not successful", "Please try again later" } });
                }

                response = await _responseMessageHelper.ResponseMessage(result, new List<string[]> { new[] { $"updating {typeof(Entity).Name} was successful", "Thank you!" } });

                return response;
            }
            catch (Exception e)
            {
                return response;
            }


        }
    }
}
