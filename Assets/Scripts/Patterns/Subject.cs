using System.Collections.Generic;
using UnityEngine;

namespace HOGUS.Scripts.DP
{
    public abstract class Subject : MonoBehaviour
    {
        private List<Observer> observers = new();

        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (Observer observer in observers)
            {
                if (observer)
                    observer.Notify(this);
            }
        }
    }
}