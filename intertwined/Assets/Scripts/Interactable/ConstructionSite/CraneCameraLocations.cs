using UnityEngine;

namespace Interactable.ConstructionSite
{
    public class CraneCameraLocations : MonoBehaviour
    {
        public Location location;
        private CraneCameraManager _cameraManager;
        
        protected void Start()
        {
            _cameraManager = GetComponentInParent<CraneCameraManager>();
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (CollidingObjectCanInteract(other)) _cameraManager.NumInLocations[(int) location]++;
        }

        protected void OnTriggerExit(Collider other)
        {
            if (CollidingObjectCanInteract(other)) _cameraManager.NumInLocations[(int) location]--;
        }
        
        private bool CollidingObjectCanInteract(Component other)
        {
            return (other.CompareTag("Boy") || other.CompareTag("BoySubObjects"))
                   || (other.CompareTag("Dog") || other.CompareTag("DogSubObjects"));
        }
    }

    public enum Location
    {
        ConstructionBuilding,
        FinalBuilding,
        FullCraneArea
    }
}