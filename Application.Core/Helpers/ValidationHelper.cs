using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.DataTransfer.Dtos.Core;
using Application.Core.Helpers.Interfaces;
using Application.Core.Repositories.Interfaces;

namespace Application.Core.Helpers
{
    public class ValidationHelper<Entity> : IValidationHelper<Entity> where Entity : class
    {
        private readonly IBaseRepository<Entity> _repository;
        public ValidationHelper(IBaseRepository<Entity> repository)
        {
            _repository = repository;
        }
        public async Task<ResponseDto> Unique(string name)
        {
            ResponseDto response = new ResponseDto
            {
                Success = false,
                Message = new List<string[]>(),
                TimeStamp = DateTime.Now
            };

            try
            {
                Regex regex = new Regex(Constants.Constants.Unique_Regex);
                bool regexValid = regex.IsMatch(name);

                List<string[]> errorMessages = new List<string[]>();

                if (string.IsNullOrWhiteSpace(name))
                {
                    errorMessages.Add(new[] { typeof(Entity).Name + " cannot be empty. Name is required" });
                    response.Message = errorMessages;
                }

                if (!regexValid)
                {
                    errorMessages.Add(new[] { $"{name} is not a valid {typeof(Entity).Name} name" });
                    response.Message = errorMessages;
                }

                List<Entity> existingRecord = await _repository.GetAllAsync();

                if (!existingRecord.Any()) return response;

                foreach (Entity item in existingRecord)
                {
                    PropertyInfo propertyInfo = item.GetType().GetProperty("Name");

                    if (propertyInfo != null)
                    {
                        string propertyValue = propertyInfo.GetValue(item).ToString().ToLower();
                        if (propertyValue == name.ToLower())
                        {
                            errorMessages.Add(new[] { $"{name} already exists. {typeof(Entity).Name} name should be unique" });
                            response.Message = errorMessages;
                        }
                    }

                    if (typeof(Entity).Name.ToString() == "Person")
                    {
                        if (item.GetType().GetProperty("EmailAddress").GetValue(item).ToString() == name.ToLower())
                        {
                            errorMessages.Add(new[] { $"{name} already exists. {typeof(Entity).Name}'s email address should be unique" });
                            response.Message = errorMessages;
                        }
                    }
                }

                return response;
            }
            catch (Exception e)
            {
                return response;
            }
        }

    }
}
