using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour
{
    private HPValue hpv;
    private float damage = 5;
    void Update()
    {
        hpv = GameObject.Find("HP").GetComponent<HPValue>();
        if (Input.GetKeyDown("z"))
        {
            hpv.SetTextValue(damage);
        }
    }
}
