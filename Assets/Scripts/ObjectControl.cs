using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    public GameObject Quest;
    

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))  
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Quest.SetActive(true);
                
                Quest.GetComponent<DialogueTrigger>().hasSpoken= false;


            }
        }
    }

}