using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Prompts : MonoBehaviour
{
    public class Prompt
    {
        private bool display = true;
        private GameObject promptObject;
        private Image promptImage;
        //private GameObject canvas;
        private Transform target;
        public Prompt(Sprite sprite, Transform target, bool display, GameObject canvas)
        {
            this.target = target;
            promptObject = new GameObject();
            promptImage = promptObject.AddComponent<Image>();
            promptImage.sprite = sprite;
            promptImage.GetComponent<RectTransform>().SetParent(canvas.transform);
            promptImage.rectTransform.sizeDelta = new Vector2(2, 2);
            promptObject.transform.position = new Vector3(target.position.x, target.position.y+2, target.position.z);
            this.display = display;
            promptObject.SetActive(display);


        }

        // Start is called before the first frame update
        void Start()
        {
            //canvas = GameObject.Find("WorldCanvas");
        }

        // Update is called once per frame
        public void Update()
        {
            if (display)
            {
                //TODO: How to rotate the image to face the camera?
                /*
                promptObject.transform.eulerAngles = new Vector3(
                    UnityEngine.Camera.main.transform.eulerAngles.x,
                    UnityEngine.Camera.main.transform.parent.gameObject.transform.eulerAngles.y,
                    promptObject.transform.eulerAngles.x);
                */
                promptObject.transform.position = new Vector3(target.position.x, target.position.y+2, target.position.z);
            }
        }

        public void Show()
        {
            display = true;
            promptObject.SetActive(true);
        }

        public void Hide()
        {
            display = false;
            promptObject.SetActive(false);
        }

    }
}
