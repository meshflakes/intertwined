using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private GameObject _boy;
    private GameObject _dog;
    
    private bool _dogMadeIt = false;
    private bool _boyMadeIt = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _boy = GameObject.FindWithTag("Boy");
        _dog = GameObject.FindWithTag("Dog");
    }

    // Update is called once per frame
    void Update()
    {
        if (_boyMadeIt && _dogMadeIt)
        {
            Destroy(_boy);
            Destroy(_dog);

            GoToCredits();
        }
    }

    private void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dog"))
        {
            _dogMadeIt = true;
        }
        if (other.CompareTag("Boy"))
        {
            _boyMadeIt = true;
        }
        
    }
}
