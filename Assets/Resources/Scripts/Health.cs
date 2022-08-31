using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Animator[] healthItem;
    public Animator geo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Hurt()
    {
        healthItem[0].SetTrigger("Hurt");
    }
    public IEnumerator showHealthItem()
    {
        for (int i = 0; i < healthItem.Length; i++)
        {
            healthItem[i].SetTrigger("Respawn");
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.2f);
        geo.Play("Enter");
    }
    public void HideHealthItem()
    {
        geo.Play("Exit");
        for (int i = 0; i < healthItem.Length; i++)
        {
            healthItem[i].SetTrigger("Hide");
        }
    }
}
