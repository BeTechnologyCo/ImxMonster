using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Debug = UnityEngine.Debug;

namespace Assets.Service
{
    public class MonsterService 
    {
        static HttpClient client = new HttpClient();
        private static string serviceUrl = "https://localhost:7062/api/monster";


        static MonsterService()
        {
            serviceUrl = "https://localhost:7062/api/monster";

#if UNITY_EDITOR
            serviceUrl = "https://localhost:7062/api/monster";
#endif

        }

        public static async Task<List<TokenDto>> MintMonster(AddMonsterDto monsterInfo)
        {
            Debug.Log("MintMonster");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GameContext.Instance.Token);

                var response = await client.PostAsJsonAsync(serviceUrl + "/MintMonster", monsterInfo);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var payload = JsonConvert.DeserializeObject<List<TokenDto>>(res);
                    Debug.Log("monster created");

                    return payload;
                }
                else
                {
                    throw new InvalidOperationException("Error server " + response.ReasonPhrase);
                }
            }
        }

        public static async Task<bool> UpdateMonster(UpdateMonsterDto monsterInfo)
        {
            Debug.Log("UpdateMonster");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GameContext.Instance.Token);

                var response = await client.PostAsJsonAsync(serviceUrl + "/UpdateMonster", monsterInfo);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var payload = JsonConvert.DeserializeObject<bool>(res);
                    Debug.Log("monster loaded");

                    return payload;
                }
                else
                {
                    throw new InvalidOperationException("Error server " + response.ReasonPhrase);
                }
            }
        }

        public static async Task<List<TokenDto>> GetMonsters()
        {
            Debug.Log("GetMonsters");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + GameContext.Instance.Token);

                var url = $"{serviceUrl}/GetMonsters";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    var payload = JsonConvert.DeserializeObject<List<TokenDto>>(res);
                    Debug.Log("monsters loaded "+ payload?.Count);

                    return payload;
                }
                else
                {
                    throw new InvalidOperationException("Error server " + response.ReasonPhrase);
                }
            }
        }
    }
}
