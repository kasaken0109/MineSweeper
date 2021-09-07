using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirControllerKey : MonoBehaviour
{
    /// <summary>マウスの入力位置/// </summary>
    private Vector3 position;
    /// <summary>プレイヤーの初期位置/// </summary>
    private Vector3 m_origin;
    /// <summary>変換後の座標/// </summary>
    Quaternion rot;
    Quaternion quaternion;
    Quaternion m_rot;
    Quaternion m_quotanion;

    private void Start()
    {

    }
    void Update()
    {
        
        //マウスのスクリーン座標をプレイヤーの座標に変換
        quaternion = Quaternion.identity;
        m_quotanion = Quaternion.identity;
        position.z = 10f;

        if (Input.GetKey(KeyCode.Q))
        {
            rot = Quaternion.AngleAxis(1, Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rot = Quaternion.AngleAxis(-1, Vector3.forward);
        }
        else
        {
            rot = Quaternion.AngleAxis(0, Vector3.forward);
        }
        quaternion = this.transform.rotation * rot;
        this.transform.rotation = quaternion;
    }
}
