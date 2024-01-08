using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Data.Dtos.Core;
using Application.Core.Helpers.Interfaces;
using Application.Core.Repositories.Interfaces;

namespace Application.Core.Helpers
{
    public class ValidationService<Entity> : IValidationService<Entity> where Entity : class
    {
        private readonly IBaseRepository<Entity> _repository;
        public ValidationService(IBaseRepository<Entity> repository)
        {
            _repository = repository;
        }
        public async Task<ResponseDto> Unique(string name)
        {
            var regex = new Regex(Constants.Constants.Unique_Regex);
            var regexValid = regex.IsMatch(name);

            List<string[]> errorMessages = new List<string[]>();

            if (string.IsNullOrWhiteSpace(name))
            {
                errorMessages.Add(new[] { typeof(Entity).Name + " cannot be empty. Name is required" });
            }

            if (!regexValid)
            {
                errorMessages.Add(new [] {$"{name} is not a valid {typeof(Entity).Name} name"});
            }

            var existingRecord = await _repository.GetAllAsync();
            if (existingRecord != null)
            {
                foreach (var item in existingRecord)
                {
                    var propertyName = item.GetType().GetProperty("Name");
                    ;
                    if (propertyName != null)
                    {
                        string propertyValue = propertyName.GetValue(item).ToString().ToLower();
                        if (propertyValue== name.ToLower())
                        {
                            errorMessages.Add(new[] {$"{name} already exists. {typeof(Entity).Name} name should be unique"});
                        }
                    }
                    
                    if (typeof(Entity).Name.ToString() == "Person")
                    {
                        if (item.GetType().GetProperty("EmailAddress").GetValue(item).ToString() == name.ToLower())
                        {
                            errorMessages.Add(new [] {$"{name} already exists. {typeof(Entity).Name}'s email address should be unique"});
                        }
                    }
                }
            }
            
            return new ResponseDto
            {
                Success = false,
                Message = errorMessages,
                TimeStamp = DateTime.Now
            };
        }

    }
}
