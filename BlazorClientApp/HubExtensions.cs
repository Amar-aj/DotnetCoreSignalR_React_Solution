using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorClientApp;

public static class HubExtensions
{
    //public static HubConnection TryInitialize(this HubConnection hubConnection, NavigationManager navigationManager, ILocalStorageService _localStorage)
    //{
    //    if (hubConnection == null)
    //    {
    //        hubConnection = new HubConnectionBuilder()
    //                          .WithUrl(navigationManager.ToAbsoluteUri("https://localhost:7250/chathub"), options => {
    //                              options.AccessTokenProvider = async () => (await _localStorage.GetItemAsync<string>("authToken"));
    //                          })
    //                          .WithAutomaticReconnect()
    //                          .Build();
    //    }
    //    return hubConnection;
    //}
    public static HubConnection TryInitialize(this HubConnection hubConnection, NavigationManager navigationManager)
    {
        if (hubConnection == null)
        {
            hubConnection = new HubConnectionBuilder()
                              .WithUrl(navigationManager.ToAbsoluteUri("https://localhost:7250/chathub"))
                              .Build();
        }
        return hubConnection;
    }
}