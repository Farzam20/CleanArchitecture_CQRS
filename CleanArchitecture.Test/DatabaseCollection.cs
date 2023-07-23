namespace CleanArchitecture.Test
{
    [CollectionDefinition("Database Collection", DisableParallelization = true)]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture> { }
}
