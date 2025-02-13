using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class User : IdObject
    {
        private readonly List<string> _tags = new List<string>();
        private int _age;
        private string _email;
        private decimal _monthlySalary;
        private string _name;
        private UserTypes _type = UserTypes.Employee;

        public string Email
        {
            get => _email;
            private set => _email = value;
        }

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public UserTypes Type
        {
            get => _type;
            private set => _type = value;
        }

        public decimal MonthlySalary
        {
            get => _monthlySalary;
            private set => _monthlySalary = value;
        }

        public int Age
        {
            get => _age;
            private set => _age = value;
        }

        public IEnumerable<string> Tags
        {
            get => _tags;
            private set => _tags.Initialize(value);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Name was not provided.");
            }
            _name = name;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "Email was not provided.");
            }
            _email = email;
        }

        public void SetType(UserTypes type)
        {
            if (!Enum.IsDefined(typeof(UserTypes), type))
            {
                throw new ArgumentOutOfRangeException(nameof(type), "Provided User Type value is not valid.");
            }
            _type = type;
        }

        public void SetAge(int? age)
        {
            if (age == null)
            {
                throw new ArgumentNullException(nameof(age), "Age was not provided.");
            }
            if (age <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(age), "Age cannot null or be negative.");
            }
            _age = age.Value;
        }

        public void SetMonthlySalary(decimal? annualSalary)
        {
            if (annualSalary == null)
            {
                throw new ArgumentNullException(nameof(annualSalary), "Salary was not provided.");
            }
            if (annualSalary <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(annualSalary), "Annual Salary cannot be null or negative.");
            }
            _monthlySalary = annualSalary.Value / 12;
        }

        public void SetTags(IEnumerable<string> tags)
        {
            _tags.Initialize(tags);
        }
    }
}