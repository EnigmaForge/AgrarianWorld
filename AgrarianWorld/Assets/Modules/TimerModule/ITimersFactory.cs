namespace TimerModule {
    public interface ITimersFactory {
        public ITimer Create(TimerConfiguration configuration);
    }
}