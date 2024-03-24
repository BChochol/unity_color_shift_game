using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelExit : MonoBehaviour{

    Collider _collider;
    
    private void Start()
    {
        _collider = GetComponent<Collider>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_collider.bounds.Contains(other.bounds.max) && _collider.bounds.Contains(other.bounds.min))
            {
                Debug.Log("Gracz jes");
            }
        }
    }
    
}
