using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInteractable : Interactable.Interactable
{
    public AnxietyCalc AnxietyCalc;
    private static double DIST_LIMIT = 2;

    public override void Interact(Character.Character interacter)
    {
        if (interacter.CompareTag("Boy") && AnxietyCalc.GetDistance() < DIST_LIMIT)
        {
            Debug.Log("touch");
            Debug.Log(AnxietyCalc.GetDistance());
            AnxietyCalc.LowerAnxiety();
        }
    }
}
