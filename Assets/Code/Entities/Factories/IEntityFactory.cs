using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Entities.Factories
{
    public interface IEntityFactory
    {
        public IEntity GetEntity(string entityPath, Vector3 position);
    }
}