using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion m_rot = Quaternion.Euler(0, 0, 10);
        Quaternion quaternion = this.transform.rotation * m_rot;
        transform.rotation = quaternion;
    }
}
