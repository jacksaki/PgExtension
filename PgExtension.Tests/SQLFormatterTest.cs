using SQLFormatter;
using Xunit.Abstractions;

namespace PgExtension.Tests;

public class SQLFormatterTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public SQLFormatterTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task TestAsync()
    {
        await new LibraryInstaller().InstallAsync();
    }
}
