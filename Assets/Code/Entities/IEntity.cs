using UnityEngine;

namespace Code.Entities
{
    public interface IEntity
    {
        Transform EntityTransform { get; }
        void Initialize();
    }
}