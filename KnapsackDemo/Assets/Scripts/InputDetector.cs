using UnityEngine;
using System.Collections;

public class InputDetector : MonoBehaviour
{
	void Update () {
        if (Input.GetKeyDown("x"))
        {
            int index = Random.Range(0, 8);
            KnapsackManager.Instance.StoreItem(index);
        }
	}
}
