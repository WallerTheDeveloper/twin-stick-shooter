namespace Code.Entities.EnemyEntity.Patrol
{
    public interface IPatrolBehaviour
    {
        // public float TimeSinceLastSawTarget { get; set; }
        // public float TimeSinceArrivedAtWaypoint { get; }
        void PatrolArea();
        // void UpdateTimers();
    }
}