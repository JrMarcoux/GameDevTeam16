﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicScript : MonoBehaviour {

    private Renderer m_renderer;
    private texturesGenerator texturesGenerator;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_renderer.material.EnableKeyword("_EMISSION");
        texturesGenerator = GameObject.FindGameObjectWithTag("Game manager").GetComponent<texturesGenerator>();
    }

    void Update()
    {
        m_renderer.material.SetColor("_EmissionColor", texturesGenerator.magicColor);
    }
}
