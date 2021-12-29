using UnityEngine;

namespace Interactable.ConstructionSite.Crane
{
    public class CraneCameraLocations : Interactable
    {
        public Location location;
        private CraneCameraManager _cameraManager;
        
        protected void Start()
        {
            _cameraManager = GetComponentInParent<CraneCameraManager>();
        }
        
        protected override void ProximityInteraction(Character.Character interacter, bool enteredTrigger)
        {
            if (enteredTrigger) _cameraManager.NumInLocations[(int) location]++;
            else _cameraManager.NumInLocations[(int) location]--;
        }
    }

    public enum Location
    {
        ConstructionBuilding,
        FinalBuilding,
        FullCraneArea
    }
}