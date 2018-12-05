using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuButtonScript : MonoBehaviour {

    public GameObject pointer;

    public void changePointerPosition()
    {
        pointer.transform.position = new Vector3(pointer.transform.position.x, transform.position.y, pointer.transform.position.z);
    }
}
