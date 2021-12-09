using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class drag : MonoBehaviour, IDragHandler
{
    public GameObject pos, detector;
    Vector3 pos_awal;
    // Start is called before the first frame update
    void Start()
    {
        pos_awal = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
    }

    //public void OnMouseDrag()
    //{
    //    Vector3 pos_mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
    //   transform.position = new Vector3(pos_mouse.x, pos_mouse.y, transform.position.z);
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
