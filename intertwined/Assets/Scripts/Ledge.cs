using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Ledge : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boy"))
        {
            var playerController = other.gameObject.GetComponent<TwoPlayerController>();
            playerController.player1Interactable = this;
        }
    }

    public override void Interact()
    {
        print("hello");
    }
}
