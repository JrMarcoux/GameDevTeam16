using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginAABB : MonoBehaviour {

	private float heightAABB;
	private float depthAABB;
	private float widthAABB;
	private float halfHeightAABB;
	private float halfDepthAABB;
	private float halfWidthAABB;

	// Use this for initialization
	void Start () {
		Vector3 seize = gameObject.transform.localScale;
		heightAABB = seize.y;
		depthAABB = seize.z;
		widthAABB = seize.x;
		halfHeightAABB = seize.y/2;
		halfDepthAABB = seize.z/2;
		halfWidthAABB = seize.x/2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//regarde si un objet est en dehors de AABB
	public bool checkIfObjectIsOutOfAABB(GameObject objectToEvaluate, bool checkX, bool checkY, bool checkZ)
	{
		Vector3 seize = objectToEvaluate.transform.localScale;
		float halfHeight = seize.y;
		float halfDepth = seize.z;
		float halfWidth = seize.x;
		bool isOutside = false;
		Vector3 positionOfObject = objectToEvaluate.transform.position;
		float positionX = Mathf.Abs(positionOfObject.x);
		float positionY = Mathf.Abs(positionOfObject.y);
		float positionZ = Mathf.Abs(positionOfObject.z);
		if (checkX)
		{
			if (positionX + halfWidth >= halfWidthAABB)
			{
				isOutside = true;
			}
		}
		if (checkY)
		{
			if (positionY + halfHeight >= halfHeightAABB)
			{
				
				isOutside = true;
			}
		}
		if (checkZ)
		{
			if (positionZ + halfDepth >= halfDepthAABB)
			{
				isOutside = true;			
			}
		}
		return isOutside;
	}

    //regarde si un objet est à l'intérieur de AABB
    public bool checkIfObjectIsInAABB(GameObject objectToEvaluate, bool checkX, bool checkY, bool checkZ)
    {
        Vector3 seize = objectToEvaluate.transform.localScale;
        float halfHeight = seize.y/2;
        float halfDepth = seize.z/2;
        float halfWidth = seize.x/2;
        bool isInside = true;
        Vector3 positionOfObject = objectToEvaluate.transform.position;
        float positionX = Mathf.Abs(positionOfObject.x);
        float positionY = Mathf.Abs(positionOfObject.y);
        float positionZ = Mathf.Abs(positionOfObject.z);
        if (checkX)
        {
            if (positionX + halfWidth >= halfWidthAABB)
            {
                isInside = false;
            }
        }
        if (checkY)
        {
            if (positionY + halfHeight >= halfHeightAABB)
            {

                isInside = false;
            }
        }
        if (checkZ)
        {
            if (positionZ + halfDepth >= halfDepthAABB)
            {
                isInside = false;
            }
        }
        return isInside;
    }

}
