using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseFollower : MonoBehaviour {

    void Start()
    {
        Cursor.visible = false;
    }

    void Update () {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;

        Vector3 mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = mouseScreenToWorld;
    }
}
