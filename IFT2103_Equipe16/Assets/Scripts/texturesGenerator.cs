using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texturesGenerator : MonoBehaviour {

    public int width = 256;
    public int height = 256;
    public float scale = 20f;
    public float offsetX = 0f;
    public float offsetY = 0f;

    public Texture2D textureMagma;
    public Texture2D textureRock;

    public Color magicColor;
    private Color baseColor;

    private void Start()
    {
        baseColor = new Color(0.5f,0,0.5f);
        textureRock = GeneratePerlinTexture(0, 0);
    }

    void Update () {
        if(offsetX >= width)
        {
            offsetX = 0;
        }
        else
        {
            offsetX += 0.1f;
        }
        offsetY = Mathf.PingPong(Time.time, 4.0f);
        textureMagma = GeneratePerlinTexture(offsetX, offsetY);  

        float emission = Mathf.PingPong(Time.time, 1.0f);
        magicColor = baseColor * Mathf.LinearToGammaSpace(emission);
    }

    Texture2D GeneratePerlinTexture(float offsetXPerl, float offsetYPerl)
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculatePerlinColor(x, y,offsetXPerl,offsetYPerl);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculatePerlinColor(int x, int y, float offsetXPerl, float offsetYPerl)
    {
        float xUnit = (float)x / width * scale + offsetXPerl;
        float yUnit = (float)y / height * scale + offsetYPerl;
        float sample = Mathf.PerlinNoise(xUnit, yUnit);
        return new Color(sample, sample, sample);
    }
}
