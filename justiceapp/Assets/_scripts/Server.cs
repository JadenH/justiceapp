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
        public void FetchProfiles(string gameId, int round = 1, int[] swipes = null)
        {
            if (round <= 0)
            {
                throw new ArgumentOutOfRangeException("round");
            }

            var url =  "/round" + round + "/" + gameId;

            if (swipes != null)
            {
                url += "," + JsonConvert.SerializeObject(swipes);
            }

            StartCoroutine(GetRequest(Events.NewProfiles, url));
        }

        public void Finish(string gameId)
        {
            StartCoroutine(GetRequest(Events.Final, "/final/" + gameId, 3f));
        }

        private IEnumerator GetRequest(Events eventCode, string url, float delay = 0)
        {
            WWW www = new WWW("http://logandanielcox.me" + url);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(GetRequest(eventCode, url));
                Debug.LogWarning("Nothing from server..");
            }
            else
            {
                Dragon.Instance.Dispachter.DispatchDelay(eventCode, www.text, delay);
            }
        }
    }
}