namespace Pharmacies.PostIt.Client;

public interface IPostItClient
{
	Task<string> GetPostCode(string address, CancellationToken cancellationToken);
}
