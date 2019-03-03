using UnityEngine;

public class WorldPointCursor : MonoBehaviour
{
    private Transform player;
    private Camera cam;

    private float height = 0;

    private void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        SetHeight();
    }

    void SetHeight()
    {
        height = Vector3.Project((player.position - cam.transform.position), Vector3.down).magnitude;
    }

    void Update()
    {
        Vector3 cursorPosition = Input.mousePosition;

        cursorPosition.z = GetDistanceToCursor();

        transform.position = cam.ScreenToWorldPoint(cursorPosition);
    }

    private float GetDistanceToCursor()
    {
        float angle = 60 * cam.ScreenToViewportPoint(Input.mousePosition).y;
        return height / Mathf.Cos(Mathf.Deg2Rad * angle);
    }
}
