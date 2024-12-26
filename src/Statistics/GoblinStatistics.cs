using System;

namespace GoblinShared
{
	[Serializable]
	public class GoblinStatistics
	{
		public long birthTime = 0;
		public int timesSacrificed = 0;
		public int timesEarnedLoot = 0;
		public int timesFarted = 0;
		public bool ownsAHome = false;
		public int homeId =-1;
	}
}
