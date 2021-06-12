using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    [SerializeField]
    GameObject _plane = null;
    BoxCollider _boxCollider = null;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_plane)
        {
            return;
        }
        if (_plane.transform.position.x < _boxCollider.bounds.min.x || _plane.transform.position.x > _boxCollider.bounds.max.x || _plane.transform.position.y < _boxCollider.bounds.min.y || _plane.transform.position.y > _boxCollider.bounds.max.y)
        {
            Debug.Log("exit");
            _plane.SetActive(false);
            _plane.transform.position = new Vector3(240.9f, 134, 0);
            _plane.SetActive(true);
        }
    }
}
