using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour
{
    public Light discoLight = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        discoLight.color = Random.ColorHSV();
    }
}
