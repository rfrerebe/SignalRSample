module ChatHub

open Microsoft.AspNetCore.SignalR
open FSharp.Control.Tasks.V2.ContextInsensitive


type ChatHub() =
    inherit Hub() with
        member this.NewMessage (args: string*string) =
            task {
                let username,message = args
                return! this.Clients.All.SendAsync("messageReceived", username, message)
            }

