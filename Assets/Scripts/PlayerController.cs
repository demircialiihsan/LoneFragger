using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float rotationSpeed = 10f;

    public Transform worldPointCursor;

    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            transform.Translate(movementVector * Time.deltaTime * moveSpeed, Space.World);

            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }

        if ((worldPointCursor.position - transform.position).sqrMagnitude > 3)
        {
            Quaternion rot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(worldPointCursor.position - transform.position), Time.deltaTime * rotationSpeed);
            transform.rotation = rot;
        }
    }
}
