using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Prompts
{
    public class Prompt
    {
        private GameObject promptObject;

        private Image promptImage;

        //private GameObject canvas;
        private Transform target;

        private static float offset = 0.9f;
        private static float size = 1.5f;

        private Transform _mainCameraTransform;

        public Prompt(Sprite sprite, Transform target, Transform canvas)
        {
            this.target = target;
            promptObject = new GameObject();
            promptImage = promptObject.AddComponent<Image>();
            promptImage.sprite = sprite;
            promptImage.GetComponent<RectTransform>().SetParent(canvas);
            promptImage.rectTransform.sizeDelta = new Vector2(size, size);
            promptObject.transform.position = new Vector3(target.position.x + offset, target.position.y +offset, target.position.z);
            promptObject.SetActive(true);

            _mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
            
        }

        public Sprite GetSprite()
        {
            return promptImage.sprite;
        }

        public void DestroyPrompt()
        {
            GameObject.Destroy(promptObject);
        }
        
        
        public void UpdatePromptPosition()
        {

            promptObject.transform.eulerAngles = new Vector3(
                _mainCameraTransform.eulerAngles.x,
                _mainCameraTransform.eulerAngles.y,
                0);

            promptObject.transform.position =
                new Vector3(target.position.x+offset, target.position.y + offset, target.position.z);
        }

        public void Show()
        {
            promptObject.SetActive(true);
        }

        public void Hide()
        {
            promptObject.SetActive(false);
        }

    }

}