using Seongho.TimerSystem;
using UnityEngine;
using UnityEngine.UI;

public class TestTimerUI : MonoBehaviour, ITimerEventHandler
{
    private ITimer timer = null;

    public Text TimerText = null;

    private void Awake()
    {
        //�׽�Ʈ��� �̸����� Ÿ�̸Ӹ� �������ְ� Ÿ�̸Ӹ� 10�ʷ� �������ش�.
        TimerSystem.CreateTimer("�׽�Ʈ", 10);
    }

    private void Start()
    {
        //�̸����� Ÿ�̸Ӹ� �����´�.
        timer = TimerSystem.GetTimer("�׽�Ʈ");

        //ITimerEventHandler�� ��ӹ��� �� Ÿ�̸ӿ� �̺�Ʈ�� �������ش�. (�����ؾ� �̺�Ʈ �Լ��� ��� ����)
        TimerSystem.OnResterTimerEvent(timer, this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            timer.StartTimer();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            timer.StopTimer();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            timer.ReStartTimer();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            timer.AddTime(10);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            timer.ResetTimer();
        }
    }

    /// <summary>
    /// Ÿ�̸Ӱ� ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    void ITimerEventHandler.OnStartTimerEvent()
    {
        TimerText.text = "Ÿ�̸� ����";
    }

    /// <summary>
    /// Ÿ�̸Ӱ� ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    void ITimerEventHandler.OnStopTimerEvent()
    {
        TimerText.text = "Ÿ�̸� ����";
    }

    /// <summary>
    /// Ÿ�̸Ӱ� ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    void ITimerEventHandler.OnEndTimerEvent()
    {
        TimerText.text = "Ÿ�̸� ��";
    }

    /// <summary>
    /// Ÿ�̸Ӹ� ������ �� ȣ��Ǵ� �Լ�
    /// </summary>
    void ITimerEventHandler.OnResetTimerEvent()
    {
        TimerText.text = $"{timer.GetMaxTime()} ��";
    }

    /// <summary>
    /// Ÿ�̸Ӱ� �۵� ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    void ITimerEventHandler.OnRunningTimerEvent(float curTimer)
    {
        int seconds = Mathf.FloorToInt(curTimer); // �Ҽ��� ����
        TimerText.text = $"{seconds} ��";
    }
}
