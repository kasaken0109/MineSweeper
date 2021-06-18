using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マウスポインターの動きに追従するAirplaneの動きを制御する
/// </summary>
public class AirPlanecontroller : MonoBehaviour
{
    /// <summary>マウスの入力位置/// </summary>
    private Vector3 position;
    /// <summary>プレイヤーの初期位置/// </summary>
    private Vector3 m_origin;
    /// <summary>変換後の座標/// </summary>
    private Vector3 screenToWorldPointPosition;
    Quaternion rot;
    Quaternion quaternion;
    Quaternion m_rot;
    Quaternion m_quotanion;

    private void Start()
    {
        //初期位置を保存
        m_origin = this.transform.position;
    }
    void Update()
    {
        //マウスのスクリーン座標をプレイヤーの座標に変換
        position = Input.mousePosition;
        quaternion = Quaternion.identity;
        m_quotanion = Quaternion.identity;
        position.z = 10f;
        screenToWorldPointPosition = new Vector3(Camera.main.ScreenToWorldPoint(position).x, Camera.main.ScreenToWorldPoint(position).y, 0);
        if(Time.timeScale >= 0.5) gameObject.transform.position = screenToWorldPointPosition;

        //旋回可能範囲なら移動距離に応じて旋回する
        if ((315f < this.transform.rotation.eulerAngles.z && 360f > this.transform.rotation.eulerAngles.z) || (this.transform.rotation.eulerAngles.z >= 0f && this.transform.rotation.eulerAngles.z < 45f))
        {
            rot = Quaternion.AngleAxis((screenToWorldPointPosition.x - m_origin.x)/ -5, Vector3.forward);
            quaternion = this.transform.rotation * rot;
            this.transform.rotation = quaternion;
        }
        else
        {
            if (this.transform.rotation.eulerAngles.z > 300f && this.transform.rotation.eulerAngles.z < 315f)
            {
                m_rot = Quaternion.Euler(0, 0, 3f);
            }
            else
            {
                m_rot = Quaternion.Euler(0, 0, -3f);
            }
            m_quotanion = m_rot * this.transform.localRotation;
            transform.rotation = m_quotanion;
        }
        m_origin = this.transform.position;
    }
}
