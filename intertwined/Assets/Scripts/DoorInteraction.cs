// using UnityEngine;
//
// public class DoorInteraction : Interactable
// {
//     private bool _opened = false;
//     private Transform _transform;
//
//     void Start()
//     {
//         _transform = gameObject.GetComponent<Transform>();
//     }
//     
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("entered");
//             IsInteractionFocus = true;
//         }
//     }
//
//     public void Interact()
//     {
//         if (!_opened)
//         {
//             _opened = true;
//             _transform.position += transform.forward;
//         }
//     }
// }