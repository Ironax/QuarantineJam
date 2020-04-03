using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashObject : MonoBehaviour
{
    [SerializeField]
    private Texture2D m_texture;
    [SerializeField]
    private Material m_material;

    private Color color;
    private int textureWidth = 500;
    private int textureHeight = 500;
    private bool isEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        SetSplash();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetSplash()
    {
        Color c_color = Color.black;
        m_texture = new Texture2D(textureWidth, textureHeight);
        for (int x = 0; x < textureWidth; ++x)
            for (int y = 0; x < textureHeight; ++y)
                m_texture.SetPixel(x, y, c_color);

        m_material.SetTexture("_DrawingTex", m_texture);
    }

    public void PaintOn(Vector2 textureCoord, Texture2D splashTexture)
    {
        if (isEnabled)
        {
            int x = (int)(textureCoord.x * textureWidth) - (splashTexture.width / 2);
            int y = (int)(textureCoord.y * textureHeight) - (splashTexture.height / 2);
            //for (int i = 0; i < splashTexture.width; ++i)
            //    for (int j = 0; j  0)
            //    {
            //        Color result = Color.Lerp(existingColor, targetColor, alpha);   // resulting color is an addition of splash texture to the texture based on alpha
            //        result.a = existingColor.a + alpha;                             // but resulting alpha is a sum of alphas (adding transparent color should not make base color more transparent)
            //        m_texture.SetPixel(newX, newY, result);
            //    }
        }

        m_texture.Apply();
    }
}

