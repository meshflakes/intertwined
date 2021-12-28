using System;
using System.Collections.Generic;
using Camera;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactable.ConstructionSite.Crane
{
    
    public class CraneCameraManager : MonoBehaviour
    {
        [Header("Camera Sequence")]
        [Tooltip("Time taken for camera to move from original position to end position and vice versa")]
        public float animationDuration = 2;
        
        private AreaCameraManager _cameraSequence;
        [NonSerialized] public readonly List<int> NumInLocations = new List<int> {0, 0, 0};

        private bool _startedEndTransition = false;
        public GameObject outro1;
        public GameObject outro2;
        public GameObject outro3;

        private CraneCameraState _currentCamera = CraneCameraState.Inactive;

        protected void Start()
        {
            var targetTransform = transform.Find("Camera");
            _cameraSequence = new AreaCameraManager(targetTransform, animationDuration);
        }

        protected void Update()
        {
            switch (_currentCamera)
            {
                case CraneCameraState.Inactive:
                    if (NumInLocations[(int) Location.FullCraneArea] == 2)
                    {
                        _cameraSequence.StartNewCameraSequence();
                        _currentCamera = CraneCameraState.Active;
                    }

                    break;
                
                case CraneCameraState.Active:
                    if (NumInLocations[(int) Location.FinalBuilding] == 2)
                    {
                        // reached end, revert to regular camera
                        if (_cameraSequence.GetCameraActive())
                        {
                            _cameraSequence.EndCameraSequence();
                            _currentCamera = CraneCameraState.Inactive;
                        }
                
                        if (!_startedEndTransition)
                        {
                            Invoke(nameof(OutroComic1), 5);
                            _startedEndTransition = true;
                        }
                    }
                    else if (NumInLocations[(int) Location.FullCraneArea] != 2 &&
                             NumInLocations[(int) Location.ConstructionBuilding] != 2)
                    {
                        // nobody in rn, make sure camera is not being modified
                        if (_cameraSequence.GetCameraActive())
                        {
                            _cameraSequence.EndCameraSequence();
                            _currentCamera = CraneCameraState.Inactive;
                        }
                    }
                    break;
                
                default:
                    Debug.LogError("Default state in CraneCameraManager not implemented");
                    break;
            }
        }

        private void OutroComic1()
        {
            outro1.SetActive(true);
            Invoke(nameof(OutroComic2), 3);
        }
        
        private void OutroComic2()
        {
            outro1.SetActive(false);
            outro2.SetActive(true);
            Invoke(nameof(OutroComic3), 3);
        }
        
        private void OutroComic3()
        {
            outro2.SetActive(false);
            outro3.SetActive(true);
            Invoke(nameof(GoToMainMenu), 3);
        }

        private void GoToMainMenu()
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }

    internal enum CraneCameraState
    {
        Active,
        Inactive
    }
}