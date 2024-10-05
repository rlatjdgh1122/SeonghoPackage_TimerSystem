using System;
using System.Collections;
using UnityEngine;

namespace Seongho.TimerSystem
{
    public class Timer : MonoBehaviour, ITimer
    {
        public event Action OnStartTimerEvent = null;
        public event Action<float> OnRunningTimerEvent = null;
        public event Action OnStopTimerEvent = null;
        public event Action OnEndTimerEvent = null;
        public event Action OnResetTimerEvent = null;
        public event Action OnDeleteTimerEvent = null;

        private TimerState _prevState = TimerState.None;
        private TimerState _timerState = TimerState.None;

        public TimerState TimerState
        {
            get => _timerState;

            private set
            {
                _prevState = _timerState;
                _timerState = value;
            }
        }


        private float _curTime = 0f;
        private float _stopTime = 0f;
        private float _maxTime = 0f;

        private Coroutine _timerCoroutine = null;

        #region Action

        public void StartTimer()
        {
            TimerState = TimerState.Running;
            OnStartTimerEvent?.Invoke();

            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);

            } //end if

            //���� ���°� �������� ��� ������ �ð����� Ÿ�̸Ӹ� �簳
            if (_prevState == TimerState.Stopped)
            {
                _timerCoroutine = StartCoroutine(Corou_Timer(_stopTime));

            } //end if

            //�װ� �ƴ϶�� �׳� ó������ ����
            else
            {
                //1�� �����ִ� ������ ���� 10���� ��� _curTime�� �� �������� ���� �� 9.x�ʰ� �Ǳ� ����
                _timerCoroutine = StartCoroutine(Corou_Timer(_curTime + 1));

            } //end else

        }

        public void StopTimer()
        {
            _stopTime = _curTime;

            TimerState = TimerState.Stopped;
            OnStopTimerEvent?.Invoke();

            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;

            } //end if

        }

        public void ReStartTimer()
        {
            ResetTimer();
            StartTimer();
        }

        public void ResetTimer()
        {
            _curTime = _maxTime;

            TimerState = TimerState.Reset;
            OnResetTimerEvent?.Invoke();

            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;

            } //end if
        }

        public void DeleteTimer()
        {
            OnDeleteTimerEvent?.Invoke();

            TimerSystem.DeleteTimer(name);
            Destroy(gameObject);
        }

        #endregion

        #region Value

        public void AddTime(float time)
        {
            _curTime += time;
        }

        public void RemoveTime(float time)
        {
            _curTime -= time;
        }

        #endregion

        public float GetCurrentTime()
        {
            return _curTime;
        }

        public float GetMaxTime()
        {
            return _maxTime;
        }

        public void SetTimer(float timer)
        {
            _maxTime = _curTime = timer;
        }

        /// <summary>
        /// ������ Ÿ�̸Ӹ� �۵���Ű�� �Լ�. �ڷ�ƾ�� �̿��Ͽ� Ÿ�̸� ��� ����
        /// </summary>
        private IEnumerator Corou_Timer(float timer)
        {
            _curTime = timer;

            while (_curTime > 1f)
            {
                yield return null;
                _curTime -= Time.deltaTime;
                OnRunningTimerEvent?.Invoke(_curTime);
            }

            _curTime = 0;
            OnRunningTimerEvent?.Invoke(0); //Ȯ���ϰ� 0�ʷ� ����

            TimerState = TimerState.Ended;
            OnEndTimerEvent?.Invoke();
        }
    }
}
