using System;
using UnityEngine;

namespace Interactable.ConstructionSite
{
    public interface IEntityMover
    {
        public void AddEntity((Transform transform, float movementScale) entity);
        
        public void RemoveEntity((Transform transform, float movementScale) entity);
    }
}