using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath1Despawn : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(WaitForSeconds(1f));
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ObjPoolManager.Ins.Despawm(transform);
    }
}
