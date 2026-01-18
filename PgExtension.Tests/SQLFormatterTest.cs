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
        var result = await LibraryInstaller.InstallAsync();
        if (result != null)
        {
            foreach (var item in result.StandardOutput)
            {
                _testOutputHelper.WriteLine(item);
            }
            foreach (var item in result.StandardError)
            {
                _testOutputHelper.WriteLine(item);
            }
        }
    }
}
