using System;

namespace bfbnet
{
	public interface ITapLock
	{
		TapLockVars TapLockVars {get; set; }
	}

	public struct TapLockVars
	{
		public bool Locked;
	}

	public static class TapLockExtensions
	{
		private static DateTime _lastTappedTime = DateTime.Now;
		public static bool AcquireTapLock(this ITapLock obj)
		{
			try
			{
				var vars = obj.TapLockVars;
				return (!vars.Locked && (vars.Locked = true) && (obj.TapLockVars = vars).Locked) ||
					_lastTappedTime.AddSeconds(3) < DateTime.Now;
			}
			finally
			{
				_lastTappedTime = DateTime.Now;
			}
		}

		public static void ReleaseTapLock(this ITapLock obj)
		{
			var vars = obj.TapLockVars;
			vars.Locked = false;
			obj.TapLockVars = vars;
		}
	}
}

