namespace A3.Event
{
    public interface IEventTriggerData<TData>
    {
        bool IsSuitable(TData data);
        void Trigger();
        bool IsReady { get; }
        void Init(IEventTriggerSystem<TData> triggerSystem);
    }
}