namespace A3.Event
{
    public interface IEventTriggerSystem<TData>
    {
        T GetEventComponent<T>() where T : class;
        void Init();
        void InvokeEvent(TData data);
        void RegisterEvent(IEventTriggerData<TData> eventTriggerData);
    }
}