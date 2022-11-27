using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLoadComponent : MonoBehaviour
{
    [SerializeField] protected bool debug = false;

    private void Reset()
    {
        this.LoadComponents();
        this.ResetValue();
    }

    private void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void ResetValue() { }
    protected virtual void LoadComponents() { }

    protected virtual void DebugRaycast(Vector3 start, RaycastHit hit, Vector3 direction)
    {
        if (!this.debug) return;

        if(hit.transform == null)
        {
            Debug.DrawRay(start, direction, Color.red);
            //Debug.Log(transform.name + ": Hit nothing");
        }
        else {
            Debug.DrawRay(start, (hit.point-transform.position).normalized, Color.green);
            //Debug.Log(transform.name + ": Hit " + hit.transform.name);
        }
    }
}
