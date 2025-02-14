using System.Collections.Generic;
using BusinessEntities;
using Common;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateUserService : IUpdateUserService
    {
        /// <inheritdoc cref="IUpdateUserService.Update"/>>
        public void Update(User user, string name, string email, UserTypes? type, int? age, decimal? annualSalary, IEnumerable<string> tags, bool ignoreNullValues = false)
        {
            if (ignoreNullValues)
            {
                name = (name + string.Empty).Length == 0 ? user.Name : name;
                email = (email + string.Empty).Length == 0 ? user.Email : email;
                type = type ?? user.Type;
                age = age ?? user.Age;
                annualSalary = annualSalary ?? user.MonthlySalary * 12;
            }

            user.SetName(name);
            user.SetEmail(email);
            // ReSharper disable once PossibleInvalidOperationException
            user.SetType(type.Value);
            user.SetAge(age);
            user.SetMonthlySalary(annualSalary);
            user.SetTags(tags);
        }
    }
}