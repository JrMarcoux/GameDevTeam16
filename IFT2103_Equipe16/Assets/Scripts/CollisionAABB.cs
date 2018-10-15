using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAABB : MonoBehaviour
{

    private float xMinThis;
    private float yMinThis;
    private float zMinThis;
    private float xMaxThis;
    private float yMaxThis;
    private float zMaxThis;

    // Use this for initialization
    void Start()
    {
        Vector3 positionThis = gameObject.transform.position;
        Vector3 scaleThis = gameObject.GetComponent<Renderer>().bounds.size;

        xMinThis = positionThis.x - (scaleThis.x / 2);
        yMinThis = positionThis.y - (scaleThis.y / 2);
        zMinThis = positionThis.z - (scaleThis.z / 2);
        xMaxThis = positionThis.x + (scaleThis.x / 2);
        yMaxThis = positionThis.y + (scaleThis.y / 2);
        zMaxThis = positionThis.z + (scaleThis.z / 2);

}

    //regarde si un objet est à l'intérieur de AABB
    public bool CheckIfObjectIsInAABB(GameObject objectToEvaluate, bool is2DOnly=false)
    {
        Vector3 position = objectToEvaluate.transform.position;
        Vector3 scale = objectToEvaluate.GetComponent<Renderer>().bounds.size;

        float xMin = position.x - (scale.x / 2);
        float yMin = position.y - (scale.y / 2);
        float zMin = position.z - (scale.z / 2);
        float xMax = position.x + (scale.x / 2);
        float yMax = position.y + (scale.y / 2);
        float zMax = position.z + (scale.z / 2);



        if (is2DOnly)
        {
            if (xMaxThis < xMin || xMax < xMinThis || yMaxThis < yMin || yMax < yMinThis)
            {
                if (this.tag == "AABB")
                {
                    Debug.Log("L'objet " + objectToEvaluate.name + " est sorti de la zone de jeu.");
                }
                return false;
            }
            if (this.tag == "Enemy" || this.tag == "Player")
            {
                Debug.Log("Collision effectue entre " + this.name + " et " + objectToEvaluate.name + ".");
            }
            return true;
        }
        else
        {
            if (xMaxThis < xMin || xMax < xMinThis || yMaxThis < yMin || yMax < yMinThis || zMaxThis < zMin || zMax < zMinThis)
            {
                if (this.tag == "AABB")
                {
                    Debug.Log("L'objet "+objectToEvaluate.name+" est sorti de la zone de jeu.");
                }
                return false;
            }
            if(this.tag == "Enemy"|| this.tag == "Player")
            {
                Debug.Log("Collision effectue entre " + this.name + " et " + objectToEvaluate.name + ".");
            }
            return true;
        }
    }

}
