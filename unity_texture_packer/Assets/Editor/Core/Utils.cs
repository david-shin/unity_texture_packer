using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

    public static int TextureWidth = 2048;
    public static int TextureHeight = 2048;

    public static float[] GetChannelData(Texture2D tex, TextureChannel ch)
    {
        Color[] colors = tex.GetPixels();
        float[] result = new float[colors.Length];

        switch (ch)
        {
            case TextureChannel.r:
                for (int i = 0; i < result.Length; i++)
                    result[i] = colors[i].r;
                break;
            case TextureChannel.g:
                for (int i = 0; i < result.Length; i++)
                    result[i] = colors[i].g;
                break;
            case TextureChannel.b:
                for (int i = 0; i < result.Length; i++)
                    result[i] = colors[i].b;
                break;
            case TextureChannel.a:
                for (int i = 0; i < result.Length; i++)
                    result[i] = colors[i].a;
                break;
            default:
                break;
        }

        return result;
    }

    public static Texture2D ReadTexture(Texture2D texture)
    {
        // Create a temporary RenderTexture of the same size as the texture
        RenderTexture tmp = RenderTexture.GetTemporary(
                            texture.width,
                            texture.height,
                            0,
                            RenderTextureFormat.Default,
                            RenderTextureReadWrite.Linear);

        // Blit the pixels on texture to the RenderTexture
        Graphics.Blit(texture, tmp);
        // Backup the currently set RenderTexture
        RenderTexture previous = RenderTexture.active;
        // Set the current RenderTexture to the temporary one we created
        RenderTexture.active = tmp;
        // Create a new readable Texture2D to copy the pixels to it
        Texture2D myTexture2D = new Texture2D(texture.width, texture.height);
        // Copy the pixels from the RenderTexture to the new Texture
        myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        myTexture2D.Apply();
        // Reset the active RenderTexture
        RenderTexture.active = previous;
        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tmp);

        return myTexture2D;
    }
}

public enum TextureChannel
{
    r, g, b, a
};

