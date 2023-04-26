﻿namespace Code.Extensions
{
    public interface ITimerUpdater
    {
        float TimeSinceLastSawTarget { get; set; }
        float TimeSinceArrivedAtWaypoint { get; set; }
        bool IsSuspecting { get; set; }
        void UpdateTimers();
    }
}