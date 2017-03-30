using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json.Linq;
using UnityEngine;

public partial class Dragon
{
    private DragonDispachter _dispachter;

    public DragonDispachter Dispachter
    {
        get { return _dispachter ?? (_dispachter = new DragonDispachter()); }
    }

    public class DragonDispachter
    {
        private readonly Dictionary<Events, List<Action<JObject>>> _subscribers;

        public DragonDispachter()
        {
            _subscribers = new Dictionary<Events, List<Action<JObject>>>();
            foreach (Events eventCode in Enum.GetValues(typeof(Events)))
            {
                _subscribers.Add(eventCode, new List<Action<JObject>>());
            }
        }

        public void Dispatch(Events eventCode, JObject payload)
        {
            foreach (var dragonStore in _subscribers[eventCode])
            {
                dragonStore.Invoke(payload);
            }
        }

        public void Dispatch(Events eventCode, string payload)
        {
            foreach (var dragonStore in _subscribers[eventCode])
            {
                dragonStore.Invoke(JObject.Parse(payload));
            }
        }

        public void Register(IDragonStore store, Events eventCode, Action<JObject> action)
        {
            _subscribers[eventCode].Add(action);
            store.OnDestroyed += () =>
            {
                _subscribers[eventCode].Remove(action);
            };
        }

        public void DispatchDelay(Events eventCode, string payload, float delay)
        {
            Instance.StartCoroutine(DelayedDispatch(eventCode, payload, delay));
        }

        private IEnumerator DelayedDispatch(Events eventCode, string payload, float delay)
        {
            yield return new WaitForSeconds(delay);
            foreach (var dragonStore in _subscribers[eventCode])
            {
                dragonStore.Invoke(JObject.Parse(payload));
            }
        }
    }

}