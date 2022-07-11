using System.Collections.Generic;
using UnityEngine.Events;

using HOGUS.Scripts.Enums;

namespace HOGUS.Scripts.DP
{
    public class EventBus
    {
        private static readonly IDictionary<TestEventEnum, UnityEvent> Events
            = new Dictionary<TestEventEnum, UnityEvent>();

        public static void Subscribe(TestEventEnum eventType, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(eventType, thisEvent);
            }
        }

        public static void Unsubscribe(TestEventEnum eventType, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void Publish(TestEventEnum eventType)
        {
            UnityEvent thisEvent;
            if(Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}