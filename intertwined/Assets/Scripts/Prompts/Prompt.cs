using UnityEngine;
using UnityEngine.UI;

namespace Prompts
{
    public class Prompt 
    {
        private readonly GameObject _promptObject;
        private readonly Image _promptImage;
        private readonly Transform _target;

        private readonly Vector3 _offset = new Vector3(0.9f, 0.9f, 0);
        private const float Size = 1.5f;

        private readonly Transform _mainCameraTransform;

        public Prompt(Sprite sprite, Transform target, Transform canvas)
        {
            _target = target;
            _promptObject = new GameObject();
            
            _promptImage = _promptObject.AddComponent<Image>();
            _promptImage.sprite = sprite;
            _promptImage.GetComponent<RectTransform>().SetParent(canvas);
            _promptImage.rectTransform.sizeDelta = new Vector2(Size, Size);
            
            _mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
            
            var dynamicOffset = _mainCameraTransform.TransformDirection(_offset);
            _promptObject.transform.position = target.position + dynamicOffset;
            _promptObject.SetActive(true);

        }

        public Sprite GetSprite()
        {
            return _promptImage.sprite;
        }

        public void DestroyPrompt()
        {
            Object.Destroy(_promptObject);
        }
        
        public void UpdatePromptPosition()
        {
            var cameraEulerAngles = _mainCameraTransform.eulerAngles;

            _promptObject.transform.eulerAngles = new Vector3(cameraEulerAngles.x, cameraEulerAngles.y, 0);
            
            var dynamicOffset = _mainCameraTransform.TransformDirection(_offset);
            _promptObject.transform.position = _target.position + dynamicOffset;
        }
    }

}