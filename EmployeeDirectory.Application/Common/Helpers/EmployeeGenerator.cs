using EmployeeDirectory.Domain;
using EmployeeDirectory.Domain.Enums;

namespace EmployeeDirectory.Application.Common.Helpers
{
    public static class EmployeeGenerator
    {
        private static readonly string[] MaleFirstNames =
        {
            "Ivan", "Petr", "Sergey", "Dmitriy", "Andrey",
            "Nikolay", "Aleksey", "Vladimir", "Mikhail", "Yuriy"
        };

        private static readonly string[] FemaleFirstNames =
        {
            "Anna", "Elena", "Olga", "Natalya", "Ekaterina",
            "Marina", "Tatiana", "Irina", "Svetlana", "Galina"
        };

        private static readonly string[] LastNamesMale =
        {
            "Ivanov", "Petrov", "Sidorov", "Smirnov", "Kuznetsov",
            "Popov", "Volkov", "Fedorov", "Egorov", "Morozov"
        };

        private static readonly string[] LastNamesFemale =
        {
            "Ivanova", "Petrova", "Sidorova", "Smirnova", "Kuznetsova",
            "Popova", "Volkova", "Fedorova", "Egorova", "Morozova"
        };

        private static readonly string[] PatronymicsMale =
        {
            "Ivanovich", "Petrovich", "Sergeevich", "Nikolaevich", "Andreevich",
            "Mikhaylovich", "Vladimirovich", "Yurievich", "Pavlovich", "Alexeevich"
        };

        private static readonly string[] PatronymicsFemale =
        {
            "Ivanovna", "Petrovna", "Sergeevna", "Nikolaevna", "Andreevna",
            "Mikhaylovna", "Vladimirovna", "Yurievna", "Pavlovna", "Alexeevna"
        };

        private static readonly Random _random = new();

        public static List<Employee> Generate(int count)
        {
            var employees = new List<Employee>(count);

            for (int i = 0; i < count; i++)
            {
                bool isMale = i % 2 == 0;
                employees.Add(CreateRandomEmployee(isMale ? EmployeeGender.Male : EmployeeGender.Female));
            }

            return employees;
        }

        public static List<Employee> GenerateWithParameters(int count, EmployeeGender gender, char lastNameStartsWith)
        {
            var employees = new List<Employee>(count);

            for (int i = 0; i < count; i++)
            {
                var employee = CreateRandomEmployee(gender);

                var parts = employee.FullName.Split(' ');
                if (parts.Length >= 1)
                {
                    parts[0] = $"{char.ToUpper(lastNameStartsWith)}{parts[0].Substring(1)}";
                    employee.FullName = string.Join(' ', parts);
                }

                employees.Add(employee);
            }

            return employees;
        }

        private static Employee CreateRandomEmployee(EmployeeGender gender)
        {
            bool isMale = gender == EmployeeGender.Male;

            string firstName = isMale
                ? MaleFirstNames[_random.Next(MaleFirstNames.Length)]
                : FemaleFirstNames[_random.Next(FemaleFirstNames.Length)];

            string lastName = isMale
                ? LastNamesMale[_random.Next(LastNamesMale.Length)]
                : LastNamesFemale[_random.Next(LastNamesFemale.Length)];

            string patronymic = isMale
                ? PatronymicsMale[_random.Next(PatronymicsMale.Length)]
                : PatronymicsFemale[_random.Next(PatronymicsFemale.Length)];

            string fullName = $"{lastName} {firstName} {patronymic}";

            return new Employee
            {
                ID = Guid.NewGuid(),
                FullName = fullName,
                DateOfBirth = DateOnly.FromDateTime(
                    DateTime.Today.AddDays(-_random.Next(7000, 20000))),
                Gender = gender
            };
        }
    }
}