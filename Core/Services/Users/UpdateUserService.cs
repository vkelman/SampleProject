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
            //// Note: we assume that parameter name == null and name == string.Empty are equivalent and should be treated as "skip this parameter"
            if (!ignoreNullValues || (name + string.Empty).Length != 0) user.SetName(name);
            if (!ignoreNullValues || (email + string.Empty).Length != 0) user.SetEmail(email);
            // ReSharper disable once PossibleInvalidOperationException
            if (!ignoreNullValues || type != null) user.SetType(type.Value);
            if (!ignoreNullValues || age != null) user.SetAge(age);
            if (!ignoreNullValues || annualSalary != null) user.SetMonthlySalary(annualSalary);
            if (!ignoreNullValues || tags != null) user.SetTags(tags);
        }
    }
}