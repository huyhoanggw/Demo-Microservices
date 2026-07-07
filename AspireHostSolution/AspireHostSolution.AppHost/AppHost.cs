var builder = DistributedApplication.CreateBuilder(args);
var product = builder.AddProject("product-api", @"..\..\ProductApiSolution\ProductApi.Presentation\ProductApi.Presentation.csproj");
var order = builder.AddProject("order-api", @"..\..\OrderApiSolution\OrderApi.Presentation\OrderApi.Presentation.csproj");
var auth = builder.AddProject("authentication-api", @"..\..\AuthenticationApiSolution\AuthenticationApi.Presentation\AuthenticationApi.Presentation.csproj");
builder.AddProject("api-gateway", @"..\..\DemoEcommerce.ApiGatewaySolution\ApiGateway.Presentation\ApiGateway.Presentation.csproj")
    .WaitFor(product).WaitFor(order).WaitFor(auth);

builder.Build().Run();
