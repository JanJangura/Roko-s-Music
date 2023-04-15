using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyGO", .5f);
    }

    public void DestroyGO()
    {
        Destroy(this.gameObject);
    }
}
