using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockScript : MonoBehaviour {

    private Renderer m_renderer;
    private texturesGenerator texturesGenerator;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_renderer.material.EnableKeyword("_NORMALMAP");
        texturesGenerator = GameObject.FindGameObjectWithTag("Game manager").GetComponent<texturesGenerator>();
        m_renderer.material.SetTexture("_BumpMap", texturesGenerator.textureRock);
    }
}
