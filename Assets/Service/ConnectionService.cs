using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using System.Timers;


public class ConnectionService
{
    private static Timer aTimer;
    static HttpClient client = new HttpClient();
    private static string serviceUrl = "https://localhost:7062/api/login";


    static ConnectionService()
    {
        serviceUrl = "https://localhost:7062/api/login";

#if UNITY_EDITOR
        serviceUrl = "https://localhost:7062/api/login";
#endif

    }

    public static async Task<string> GetTokenAsync(LoginVM loginInfo)
    {
        Debug.Log("GetTokenAsync");
        var url = serviceUrl + "/CreateToken";
        var response = await UnityRequestClient.Post<TokenResponse>(url, loginInfo);
        Debug.Log("token created");
        return response.Token;
    }

    public static async Task<MessageVM> RequestConnection(string account)
    {
        Debug.Log("GetTokenAsync");

        var url = $"{serviceUrl}/RequestConnection?account={account}";

        return await UnityRequestClient.Get<MessageVM>(url);
    }
}
