using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class ObjectFinder : MonoBehaviour
    {
        [SerializeField] private float _searchRadius = 5f;
        [SerializeField] private FindableObjectId _findableObjectId;        
        public T FindClosestObjectOfType<T>() where T : Component, IFindableObject
        {
            T[] objectsInRadius = FindClosestObjectsOfType<T>();
            T closestObject = null;
            float closestDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            
            foreach (T obj in objectsInRadius)
            {
                float distance = Vector3.Distance(obj.transform.position, currentPosition);
                if (distance < closestDistance)
                {
                    closestObject = obj;
                    closestDistance = distance;
                }
            }
            return closestObject;
        }
        private T[] FindClosestObjectsOfType<T>() where T : Component, IFindableObject
        {
            T[] objects = FindObjectsOfType<T>();

            List<T> objectsOfType = new List<T>();

            foreach (T obj in objects)
            {
                float distance = Vector3.Distance(obj.transform.position, transform.position);
                if (distance <= _searchRadius)
                {
                    objectsOfType.Add(obj);
                }
            }

            return objectsOfType.ToArray();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
        }
    }
}