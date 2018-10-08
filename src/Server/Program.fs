module SignalRSample

open System
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open ChatHub

let inline action<'T> f =
    new Action<'T>(f)

let configureServices (services : IServiceCollection) =
    services
        .AddSignalR()
    |> ignore

let configureSignalR (options : SignalR.HubRouteBuilder) =
    options.MapHub<ChatHub>(new PathString("/hub"))

let configureApp (app : IApplicationBuilder) =
        app.UseDefaultFiles()
           .UseStaticFiles()
           .UseSignalR(action<SignalR.HubRouteBuilder>(configureSignalR))
           |> ignore

[<EntryPoint>]
let main _ =
    WebHost.CreateDefaultBuilder()
        .Configure(action<IApplicationBuilder>(configureApp))
        .ConfigureServices(configureServices)
        .Build()
        .Run()
    0