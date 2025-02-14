using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Users
{
    public interface IUpdateUserService
    {
        /// <summary>
        /// Update user properties from individual parameters supplied
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="name">name</param>
        /// <param name="email">email</param>
        /// <param name="type">user type</param>
        /// <param name="age">age</param>
        /// <param name="annualSalary">annual salary</param>
        /// <param name="tags">associated tags</param>
        /// <param name="ignoreNullValues">If true, don't replace user properties with nulls (suitable for update operation with optional parameters;
        /// if false, raise a corresponding exception if some of the parameters are nulls (suitable for create operation).</param>
        void Update(User user, string name, string email, UserTypes? type, int? age, decimal? annualSalary, IEnumerable<string> tags, bool ignoreNullValues = false);
    }
}