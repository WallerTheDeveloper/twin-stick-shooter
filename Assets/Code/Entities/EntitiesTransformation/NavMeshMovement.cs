using UnityEngine;
using UnityEngine.AI;

namespace Code.Entities.EntitiesTransformation
{
    public class NavMeshMovement : IMovement
    {
        private readonly NavMeshAgent _navMeshAgent;

        public float MovementSpeed { get; set; }

        public NavMeshMovement(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
        }

        public void Move(Vector3 destination, float speed)
        {
            _navMeshAgent.speed = speed;
            _navMeshAgent.SetDestination(destination);
        }
    }
}