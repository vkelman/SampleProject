using System;
using System.Collections.Generic;
using BusinessEntities;
using JetBrains.Annotations;

namespace Core.Services.Users
{
    public interface ICreateUserService
    {
        User Create(Guid id, string name, string email, UserTypes type, int? age, decimal? annualSalary, IEnumerable<string> tags);
    }
}