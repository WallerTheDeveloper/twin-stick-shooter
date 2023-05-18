using Code;
using Code.Infrastructure.GameStates;
using Code.StaticData;
using Code.StaticData.Enemies;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using Code.Entities.EnemyEntity.Patrol;

namespace Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.EnemySpawners = 
                    FindObjectsOfType<SpawnMarker>()
                        .Select(x => new EnemySpawnerData(
                            x.GetComponent<UniqueId>().Id,
                            x.EnemyTypeId, 
                            x.transform.position))
                        .ToList();
                
                levelData.LevelKey = SceneManager.GetActiveScene().name;
                
                levelData.PatrolPaths = 
                    FindObjectsOfType<PatrolPathMarker>()
                        .Select(x => new PatrolPathData(
                            x.GetComponent<UniqueId>().Id,
                            x.transform.position
                            ))
                        .ToList();
            }

            EditorUtility.SetDirty(target);
        }
    }
}