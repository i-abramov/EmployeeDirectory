namespace EmployeeDirectory.Persistence
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(EmployeeDirectoryDbContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }
}