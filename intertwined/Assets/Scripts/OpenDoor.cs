using UnityEngine;

namespace DefaultNamespace
{
    public class OpenDoor : MonoBehaviour
    {
        public GameObject gate;
        
        // Update is called once per frame
        void Update()
        {
            if (transform.position.x < 10 & transform.position.z < -5)
            {
                gate.SetActive(false);
            }
        }
    }
}