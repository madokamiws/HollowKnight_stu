using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public SpriteRenderer render;
    public Color normalColor;
    public Color flashColor;

    public int duration;

    public bool isInvincibility;
    private static Invincibility _instance;
    public static Invincibility Get
    {
        get
        {
            if (_instance == null)
            {
                // instance = this ;
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public IEnumerator SetInvincibility()
    {
        isInvincibility = true;
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(0.1f);
            render.color = flashColor;
            yield return new WaitForSeconds(0.1f);
            render.color = normalColor;

        }
        isInvincibility = false;
    }
}
