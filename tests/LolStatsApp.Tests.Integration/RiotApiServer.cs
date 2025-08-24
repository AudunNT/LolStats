using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace LolStatsApp.Tests.Integration;

public class RiotApiServer : IDisposable
{
    private WireMockServer _server;

    public string Url => _server.Url!;

    public void Start()
    {
        _server = WireMockServer.Start();
    }

    public void SetUpUser(string summonerName)
    {
        _server.Given(Request.Create()
                .WithPath($"/api/stats/{summonerName}")
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(GenerateStatsBody(summonerName))
                .WithHeader("content-type", "application/json; charset=utf-8")
                .WithStatusCode(200));
    }

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }

    private static string GenerateStatsBody(string summonerName)
    {
        return $@"{{
""SummonerName"": ""{summonerName}"",
""Wins"": ""0"",
""Losses"": ""0""}}";
}
}