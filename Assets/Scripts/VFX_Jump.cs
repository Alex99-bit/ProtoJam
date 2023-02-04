using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Jump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 0.54f);
    }
}
