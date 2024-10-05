using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Seongho.TimerSystem
{
	public static class TimerSystem
	{
		private static GameObject _timerObj = null;
		private static TimerExecutor _timerExecutor = null;
		private static Dictionary<string, ITimer> _nameToTimerDic = null;

		static TimerSystem()
		{
			_nameToTimerDic = new Dictionary<string, ITimer>();

			CreateExecutor();
		}

        /// <summary>
        /// MonoBehaviour�� GameObject�� ����� ����ϱ� ���� TimerExecutor ������ ������Ʈ�� �ϳ� �������ش�.
        /// </summary>
        private static void CreateExecutor()
		{
			if (_timerObj != null)
			{
				Object.Destroy(_timerObj);
			}

			_timerObj = new GameObject("TimerExecutor");
			Object.DontDestroyOnLoad(_timerObj);
			_timerExecutor = _timerObj.AddComponent<TimerExecutor>();
		}

		/// <summary>
		/// Ÿ�̸Ӹ� �������ִ� �Լ�
		/// </summary>
		public static ITimer CreateTimer(string timerName, float timer)
		{
			if (_nameToTimerDic.ContainsKey(timerName))
			{
                Debug.LogError($"{timerName}�� �ߺ��Ǿ����ϴ�.");

				return null;

			} //end if

			ITimer iTimer = _timerExecutor.CreateTimer(timerName, timer);
			_nameToTimerDic.Add(timerName, iTimer);

			return iTimer;
		}

		/// <summary>
		/// Ÿ�̸Ӹ� �̸����� �������� �Լ�
		/// </summary>
		public static ITimer GetTimer(string timerName)
		{
			if (_nameToTimerDic.TryGetValue(timerName, out ITimer value))
			{
				return value;

			} //end if

			Debug.Log($"{timerName}�� ã�� �� �����ϴ�.");

			return null;
		}

		/// <summary>
		/// Ÿ�̸Ӹ� �����ִ� �Լ�
		/// </summary>
		/// <param name="timerName"></param>
		public static void DeleteTimer(string timerName)
		{
			_nameToTimerDic.Remove(timerName);
		}

		#region Control

		/// <summary>
		/// ��� Ÿ�̸Ӹ� �������ִ� �Լ�
		/// </summary>
		public static void StartAllTimer()
		{
			foreach (ITimer item in _nameToTimerDic.Values)
			{
				item.StartTimer();

			} // end foreach
		}

		/// <summary>
		/// ��� Ÿ�̸Ӹ� ������Ű�� �Լ� 
		/// </summary>
		public static void StopAllTimer()
		{
			foreach (ITimer item in _nameToTimerDic.Values)
			{
				item.StopTimer();

			} // end foreach
		}

        /// <summary>
        /// ��� Ÿ�̸Ӹ� �ٽ� �����ϴ� �Լ� 
        /// </summary>
        public static void ReStartAllTimer()
		{
			foreach (ITimer item in _nameToTimerDic.Values)
			{
				item.ReStartTimer();

			} // end foreach
		}

        /// <summary>
        /// ��� Ÿ�̸Ӹ� �����ϴ� �Լ� 
        /// </summary>
        public static void ResetAllTimer()
		{
			foreach (ITimer item in _nameToTimerDic.Values)
			{
				item.ResetTimer();

			} // end foreach
		}

        /// <summary>
        /// ��� Ÿ�̸Ӹ� ����� �Լ� 
        /// </summary>
        public static void DeleteAllTimer()
		{
			foreach (ITimer item in _nameToTimerDic.Values)
			{
				item.DeleteTimer();

			} // end foreach
		}

		#endregion

		/// <summary>
		/// �̺�Ʈ ������ ������ִ� ��ƿ ��� �Լ�
		/// </summary>
		public static void OnResterTimerEvent(ITimer timer, ITimerEventHandler timerEventHandler)
		{
			timer.OnStartTimerEvent    += timerEventHandler.OnStartTimerEvent;
			timer.OnRunningTimerEvent  += timerEventHandler.OnRunningTimerEvent;
			timer.OnStopTimerEvent     += timerEventHandler.OnStopTimerEvent;
			timer.OnEndTimerEvent      += timerEventHandler.OnEndTimerEvent;
			timer.OnResetTimerEvent    += timerEventHandler.OnResetTimerEvent;
			timer.OnDeleteTimerEvent   += timerEventHandler.OnDeleteTimerEvent;
		}

        /// <summary>
        /// �̺�Ʈ ���������� ������ִ� ��ƿ ��� �Լ�
        /// </summary>
        public static void RemoveResterTimerEvent(ITimer timer, ITimerEventHandler timerEventHandler)
		{
			timer.OnStartTimerEvent    -= timerEventHandler.OnStartTimerEvent;
			timer.OnRunningTimerEvent  -= timerEventHandler.OnRunningTimerEvent;
			timer.OnStopTimerEvent     -= timerEventHandler.OnStopTimerEvent;
			timer.OnEndTimerEvent      -= timerEventHandler.OnEndTimerEvent;
			timer.OnResetTimerEvent    -= timerEventHandler.OnResetTimerEvent;
			timer.OnDeleteTimerEvent   -= timerEventHandler.OnDeleteTimerEvent;
		}


	}

	public class TimerExecutor : MonoBehaviour
	{
		/// <summary>
		/// ���⼭ ���������� Ÿ�̸Ӹ� �������ش�.
		/// </summary>
		public ITimer CreateTimer(string timerName, float timer)
		{
			Timer iTimer = new GameObject(timerName).AddComponent<Timer>();
			iTimer.SetTimer(timer);

			iTimer.transform.parent = transform;

			return iTimer;
		}
	}
}

