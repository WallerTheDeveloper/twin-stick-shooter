using UnityEngine;

namespace Code.Entities.EnemyEntity.Patrol
{
    [RequireComponent(typeof(SphereCollider))]
    public class PatrolPath : MonoBehaviour, IFindableObject
    {
        [field: SerializeField] public FindableObjectId FindableObjectId { get; set; }

        const float waypointGizmoRadius = 0.3f;

        bool AssignedToEntity { get; }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
        
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

    }
}