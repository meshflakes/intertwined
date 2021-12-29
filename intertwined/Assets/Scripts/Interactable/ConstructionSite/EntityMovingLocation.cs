using Character;
using Extensions;
using UnityEngine;

namespace Interactable.ConstructionSite
{
    public class EntityMovingLocation : Interactable
    {
        [SerializeField] [Tooltip("How many layers up the common parent is")] 
        private int entityMoverLocation = 1;
        
        private IEntityMover _entityMover;
                
        protected void Start()
        {
            var currTransform = transform;

            for (var i = 0; i < entityMoverLocation; i++)
                currTransform = currTransform.parent;

            _entityMover = currTransform.gameObject.GetInterface<IEntityMover>();
                        
            if (_entityMover == null)
                _entityMover = currTransform.gameObject.GetInterfaceInChildren<IEntityMover>();
            
            if (_entityMover == null)
                Debug.LogError($"No {nameof(IEntityMover)} found rooted at ancestor {currTransform.name}, " +
                               $"n={entityMoverLocation} layers above {gameObject.name}");
        }

        protected override void ProximityInteraction(Character.Character interacter, bool enteredTrigger)
        {
            var movementScale = interacter.charType == CharType.Boy ? 3.5f : 1.95f;
            
            if (enteredTrigger)
            {
                _entityMover.AddEntity((interacter.transform, movementScale));
            }
            else
            {
                _entityMover.RemoveEntity((interacter.transform, movementScale));
            }
            
        }
    }
}