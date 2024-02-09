using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class CameraControl : MonoBehaviour, IScrollHandler
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float max_x = 10.0f, min_x = -10.0f, max_y = 10.0f, min_y = -10.0f;
    [SerializeField] float max_zoom = 10.0f, min_zoom = 1.0f;
    [SerializeField] float zoom_speed = 1.0f;

    private Cinemachine.CinemachineVirtualCamera camera;
    private float scroll;

    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move this object with WASD:
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -1) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }

        scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Debug.Log("Scrolled: " + scroll);
            camera.m_Lens.OrthographicSize = Mathf.Clamp(camera.m_Lens.OrthographicSize - scroll * zoom_speed * Time.deltaTime, min_zoom, max_zoom);
        }

        //Clamp inside certain max and min values:
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min_x, max_x),
                                        transform.position.y,
                                        Mathf.Clamp(transform.position.z, min_y, max_y));
    }

    public void Scroll(BaseEventData data)
    {
        //Zoom in and out with the mouse wheel:
        PointerEventData pointer = (PointerEventData)data;
        float scroll = pointer.scrollDelta.y;
        camera.m_Lens.OrthographicSize = Mathf.Clamp(camera.m_Lens.OrthographicSize - scroll, min_zoom, max_zoom);
        Debug.Log("Scrolled: " + scroll);
    }

    public void OnScroll(PointerEventData data)
    {
        Scroll(data);
    }

}
