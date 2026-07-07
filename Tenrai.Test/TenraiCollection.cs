using Xunit;

namespace Tenrai.Tests;

[CollectionDefinition("TenraiTests")]
public class TenraiCollection : ICollectionFixture<TenraiFixture>
{
}
