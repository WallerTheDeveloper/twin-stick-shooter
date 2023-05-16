using Code;
using Code.Entities.Factories;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class SpawnerMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
        {
            CircleGizmo(spawner);
        }

        private static void CircleGizmo(SpawnMarker spawner)
        {
            Gizmos.color = Color.red;
            
            Vector3 position = spawner.transform.position;
            
            Gizmos.DrawSphere(position, 0.5f);
        }
    }
}