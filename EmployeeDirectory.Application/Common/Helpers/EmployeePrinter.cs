using EmployeeDirectory.Domain;

namespace EmployeeDirectory.Application.Common.Helpers
{
    public static class EmployeePrinter
    {
        public static void Print(IEnumerable<Employee> employees)
        {
            if (employees == null || !employees.Any())
            {
                Console.WriteLine("No employees found.");
                return;
            }

            var list = employees.ToList();

            string[] headers = { "Full Name", "Date of Birth", "Gender", "Age" };

            var rows = list.Select(e => new[]
            {
                e.FullName,
                e.DateOfBirth.ToString("yyyy-MM-dd"),
                e.Gender.ToString(),
                e.GetEmployeeAge().ToString()
            }).ToList();

            int[] widths = Enumerable.Range(0, headers.Length)
                .Select(i => Math.Max(headers[i].Length, rows.Max(r => r[i].Length)) + 2)
                .ToArray();

            string format = string.Join(" ", Enumerable.Range(0, headers.Length)
                .Select(i => $"{{{i},-{widths[i]}}}"));

            Console.WriteLine(format, headers[0], headers[1], headers[2], headers[3]);

            Console.WriteLine(new string('-', widths.Sum() + headers.Length - 1));

            foreach (var row in rows)
            {
                Console.WriteLine(format, row[0], row[1], row[2], row[3]);
            }
        }
    }
}