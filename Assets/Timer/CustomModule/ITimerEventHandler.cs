namespace Seongho.TimerSystem
{
    /// <summary>
    /// Ÿ�̸��� �̺�Ʈ�� �������ְ� ���� ��� ��ӹ޾� ����Ѵ�.
    /// </summary>
	public interface ITimerEventHandler
	{
		public void OnStartTimerEvent() { }
		public void OnRunningTimerEvent(float curTimer) { }
        public void OnStopTimerEvent() { }
        public void OnEndTimerEvent() { }
        public void OnResetTimerEvent() { }
        public void OnDeleteTimerEvent() { }
    }
}