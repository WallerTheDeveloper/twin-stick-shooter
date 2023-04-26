using System.Timers;
using UnityEngine;

namespace Code.Extensions
{
    public class TimerUpdater : ITimerUpdater
    {
        public float TimeSinceLastSawTarget { get; set; }
        public float TimeSinceArrivedAtWaypoint { get; set; }
        public bool IsSuspecting { get; set; }

        public void UpdateTimers()
        {
            TimeSinceLastSawTarget += Time.deltaTime;
            TimeSinceArrivedAtWaypoint += Time.deltaTime;
        }
    }
}