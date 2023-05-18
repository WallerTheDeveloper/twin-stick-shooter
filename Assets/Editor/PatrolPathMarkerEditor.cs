using Code;
using Code.Entities.EnemyEntity.Patrol;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PatrolPath))]
    public class PatrolPathMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(PatrolPathMarker patrolPath, GizmoType gizmo)
        {
            CircleGizmo(patrolPath);
        }

        private static void CircleGizmo(PatrolPathMarker patrolPath)
        {
            Gizmos.color = Color.green;
            
            Vector3 position = patrolPath.transform.position;
            
            Gizmos.DrawSphere(position, 0.5f);
        }
    }
}