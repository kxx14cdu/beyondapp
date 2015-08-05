using System;

namespace bfbnet
{
	public interface IConnection
	{
		void CheckConnection (bool result);
		void CheckSiteIsReachable (bool result);
	}
}

