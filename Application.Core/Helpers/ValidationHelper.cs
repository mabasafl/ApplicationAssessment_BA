using System;
using System.Collections.Concurrent;
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
using System.Xml.Linq;

namespace Application.Core.Helpers
{
    public class ValidationHelper<Entity> : IValidationHelper<Entity> where Entity : class
    {
        private readonly IBaseRepository<Entity> _repository;
        public ValidationHelper(IBaseRepository<Entity> repository)
        {
            _repository = repository;
        }
        public async Task<ResponseDto> Unique(string name, string regexPattern, string type)
        {
            ResponseDto response = new ResponseDto
            {
                Success = false,
                Message = new List<string[]>(),
                TimeStamp = DateTime.Now
            };

            try
            {
                List<string[]> errorMessages = new List<string[]>();

                
                errorMessages = await IsValid(name, regexPattern, type);
                response.Message = errorMessages;

                List<Entity> existingRecord = await _repository.GetAllAsync();

                if (existingRecord.Any())
                {
                    ConcurrentBag<Entity> records = new ConcurrentBag<Entity>(existingRecord);

                    Parallel.ForEach(records, item =>
                    {
                        response.Message = EmailAddressValidation(item, name, errorMessages);
                        response.Message = NameValidation(item, name, errorMessages);
                    });
                }

                return response;
            }
            catch (Exception e)
            {
                return response;
            }
        }

        private List<string[]> NameValidation(Entity item, string name, List<string[]> errors)
        {
            List<string[]> errorMessages = errors;

            string? nameValue = item.GetType().GetProperty("Name")?.GetValue(item)?.ToString();
            if (nameValue == null) return errorMessages;
            if (nameValue.ToLower() == name.ToLower()) errorMessages.Add(new[] { $"{name} already exists. {typeof(Entity).Name} name should be unique" });

            return errorMessages;
        }  
        
        private List<string[]> EmailAddressValidation(Entity item, string name, List<string[]> errors )
        {
            List<string[]> errorMessages = errors;

            string? emailAddressValue = item.GetType().GetProperty("EmailAddress")?.GetValue(item)?.ToString();
            if (emailAddressValue == null) return errorMessages;
            if (emailAddressValue.ToLower() == name.ToLower()) errorMessages.Add(new[] { $"{name} already exists. {typeof(Entity).Name}'s email address should be unique" });
            
            return errorMessages;
        }
        
        private async Task<List<string[]>> IsValid(string name, string pattern, string type)
        {
            Regex regex = new Regex(pattern);
            bool regexValid = regex.IsMatch(name);
            List<string[]> errorMessages = new List<string[]>();
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) errorMessages.Add(new[] { typeof(Entity).Name + " cannot be empty. Name is required" });
            if (!regexValid) errorMessages.Add(new[] { $"'{name}' is not a valid {typeof(Entity).Name} {type}" });
            return errorMessages;
        }

    }
}
