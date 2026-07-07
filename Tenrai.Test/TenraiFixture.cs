namespace Tenrai.Tests;

public class TenraiFixture
{
	public ITenrai TenraiClient { get; }

	public TenraiFixture()
	{
		TenraiClient = new TenraiClient();
	}
}
