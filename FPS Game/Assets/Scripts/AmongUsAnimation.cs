using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmongUsAnimation : MonoBehaviour
{
    Vector3 prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        prevPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (prevPosition != transform.position)
		{
			GetComponent<Animator>().SetBool("isWalk",true);
			prevPosition = transform.position;
		}
		else{
			GetComponent<Animator>().SetBool("isWalk",false);
		}
        
    }
}
