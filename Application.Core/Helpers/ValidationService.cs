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
    public class ValidationService<T> : IValidationService<T> where T : class
    {
        private readonly IBaseRepository<T> _repository;
        public ValidationService(IBaseRepository<T> repository)
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
                errorMessages.Add(new[] { typeof(T).Name + " cannot be empty. Name is required" });
            }

            if (!regexValid)
            {
                errorMessages.Add(new [] {$"{name} is not a valid {typeof(T).Name} name"});
            }

            var existingRecord = await _repository.GetAllAsync();
            if (existingRecord != null)
            {
                foreach (var item in existingRecord)
                {
                    if (item.GetType().GetProperty("Name").GetValue(item).ToString().ToLower() == name.ToLower())
                    {
                        errorMessages.Add(new[] {$"{name} already exists. {typeof(T).Name} name should be unique"});
                    }
                }
            }

            //PropertyInfo propety = existingRecord.GetType().GetProperty("Name");
            //if (propety != null)
            //{
            //    var existingName = propety.GetValue(existingRecord);

            //    if (existingName != null)
            //    {
            //        existingName.GetType().GetProperties();
            //    }
            //}
            

            return new ResponseDto
            {
                Success = false,
                Message = errorMessages,
                TimeStamp = DateTime.Now
            };
        }

    }
}
