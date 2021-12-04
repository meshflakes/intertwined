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
            promptImage.rectTransform.sizeDelta = new Vector2(5, 5);
            promptObject.transform.position = target.position;
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
                //Scuffed rotating the image towards the camera
                promptObject.transform.eulerAngles = new Vector3(
                    UnityEngine.Camera.main.transform.eulerAngles.x,
                    UnityEngine.Camera.main.transform.parent.gameObject.transform.eulerAngles.y,
                    promptObject.transform.eulerAngles.x);
                
                promptObject.transform.position = target.position;
            }
        }

        void show()
        {
            display = true;
            promptObject.SetActive(true);
        }

        void hide()
        {
            display = false;
            promptObject.SetActive(false);
        }

    }
}
