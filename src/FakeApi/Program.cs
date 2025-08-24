using WireMock.ResponseBuilders;
using WireMock.Server;
using Request = WireMock.RequestBuilders.Request;

var wireMockServer = WireMockServer.Start();

Console.WriteLine($"wireMock is running on: {wireMockServer.Url}");

wireMockServer.Given(Request.Create()
    .WithPath("/example")
    .UsingGet())
    .RespondWith(Response.Create()
        .WithBody("wiremock")
        .WithStatusCode(200));

Console.ReadKey();
wireMockServer.Dispose();