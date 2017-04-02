using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Utility.Singleton;

namespace _scripts
{
    public class Server : MonoSingleton<Server>
    {
        const string URL = "http://logandanielcox.me/";


        public void FetchProfiles(string gameId, int round = 1, int[] swipes = null)
        {
            if (round <= 0)
            {
                throw new ArgumentOutOfRangeException("round");
            }

            var url = URL + "/round" + round + "/" + gameId;

            if (swipes != null)
            {
                url += "," + JsonConvert.SerializeObject(swipes);
            }

            StartCoroutine(GetProfiles(url));
        }

        private IEnumerator GetProfiles(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(GetProfiles(url));
                Debug.LogWarning("Nothing from server..");
            }
            else
            {
                Dragon.Instance.Dispachter.Dispatch(Events.NewProfiles, www.text);
            }
        }
    }
}