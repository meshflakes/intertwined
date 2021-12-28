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

        protected void Start()
        {
            var targetTransform = transform.Find("Camera");
            _cameraSequence = new AreaCameraManager(targetTransform, animationDuration);
        }

        protected void Update()
        {
            if (NumInLocations[(int) Location.FullCraneArea] != 2)
            {
                // nobody in rn, make sure camera is not being modified
                if (_cameraSequence.GetCameraActive())
                {
                    _cameraSequence.EndCameraSequence();
                }
                
            } else if (NumInLocations[(int) Location.FinalBuilding] == 2)
            {
                // reached end, revert to regular camera

                if (_cameraSequence.GetCameraActive()) _cameraSequence.EndCameraSequence();
                
                if (!_startedEndTransition)
                {
                    Invoke(nameof(OutroComic1), 5);
                    _startedEndTransition = true;
                }
                
                
            }
            else
            {
                // set to special camera
                if (!_cameraSequence.GetCameraActive()) _cameraSequence.StartNewCameraSequence();
                
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
}