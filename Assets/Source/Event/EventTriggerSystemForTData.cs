using System;
using System.Collections.Generic;
using UnityEngine;

namespace A3.Event
{
    public abstract class EventTriggerSystem<TData> : MonoBehaviour, IEventTriggerSystem<TData>
    {
        private Dictionary<Type, object> _component;

        private List<IEventTriggerData<TData>> _eventDatas { get; } = new List<IEventTriggerData<TData>>();

        public T GetEventComponent<T>() where T : class
        {
            if (!_component.ContainsKey(typeof(T))) return null;
            return (T) _component[typeof(T)];
        }

        public abstract void Init();

        public virtual void Init(params object[] components)
        {
            foreach (object cmpn in components)
                RegisterComponent(cmpn);
            Init();
        }

        private void RegisterComponent(object cmpn)
        {
            if (cmpn == null) return;
            if (_component == null) _component = new Dictionary<Type, object>();
            if (_component.ContainsKey(cmpn.GetType())) return;
            _component.Add(cmpn.GetType(), cmpn);
        }

        public void InvokeEvent(TData data)
        {
            IEventTriggerData<TData> foundEventTrigger = _eventDatas.Find(eventData => eventData.IsSuitable(data));
            if (foundEventTrigger == null) return;
            if (!foundEventTrigger.IsReady) foundEventTrigger.Init(this);
            foundEventTrigger.Trigger();
        }


        public void RegisterEvent(IEventTriggerData<TData> eventTriggerData)
        {
            _eventDatas.Add(eventTriggerData);
        }
    }
}