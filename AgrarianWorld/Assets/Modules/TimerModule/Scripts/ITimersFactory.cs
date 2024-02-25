namespace TimerModule {
    public interface ITimersFactory {
        public ITimer Create(TimerType timerType);
    }
}