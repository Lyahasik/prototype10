using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DiskProperties))]
[RequireComponent(typeof(IDiskRotation))]
[RequireComponent(typeof(IRotationStart))]
[RequireComponent(typeof(IRotationStop))]
public class DiskLife : MonoBehaviour
{
    private IRotationStart _iRotationStart;
    
    void Start()
    {
        _iRotationStart = (IRotationStart) GetComponent(typeof(IRotationStart));

        StartCoroutine(StartRotation());
    }

    private IEnumerator StartRotation()
    {
        yield return new WaitForSeconds(1.0f);
        
        _iRotationStart.Switch();
    }
    
    void Update()
    {
        
    }
}
