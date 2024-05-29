using UnityEngine;

public class _BC_PC_move : MonoBehaviour
{

    [Header("Camera")]
    public Camera BC_PC_cam;
    public float fl_sensitivity = 1.0f;
    public float fl_view_angle_limit = 60;

    private float fl_x_rotation = 0.0f;
    private float fl_y_rotation = 0.0f;
    private Vector3 v3_direction = Vector3.zero;
    private CharacterController BC_PC_cc = null;

    [Header("CameraBob")]
    public bool bl_can_camera_bob = true;

    [Header("Movement")]
    public float fl_speed = 0;
    public float fl_jump_force = 8.0F;
    public float fl_current_jump_count = 0;
    public float fl_jump_count = 4;
    public float fl_gravity = 20.0F;
    public float fl_rotation_rate = 180;
    public float fl_dash_speed = 10;
    private Vector3 v3_move_direction = Vector3.zero;
    public bool bl_can_double_jump = false;
    public int in_cc_height = 2;

    [Header("Field Of View")]
    private float initialFOV; 
    private float runFOV;

    [Header("Head Bobbing")]
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;
    private float headBobbingTimer = 0.0f;

    // Get player stats
    public pc_stats stats;

    void Start()
    {
        BC_PC_cc = GetComponent<CharacterController>();
        stats = GetComponent<pc_stats>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        fl_current_jump_count = fl_jump_count;
        UpdateFov();

    }
    void UpdateFov()
    {
        initialFOV = BC_PC_cam.fieldOfView;
        runFOV = initialFOV + 10;
    }

    void Update()
    {
        camera_look();
        PC_Move();
        HeadBobbing();
    }

    private void camera_look()
    {
        // Gets inputs
        float fl_mouse_x = Input.GetAxis("Mouse X");
        float fl_mouse_y = Input.GetAxis("Mouse Y");

        fl_y_rotation += fl_mouse_x * fl_sensitivity * Time.deltaTime;
        fl_x_rotation -= fl_mouse_y * fl_sensitivity * Time.deltaTime;

        fl_x_rotation -= fl_mouse_y;

        // Clamps rotation
        fl_x_rotation = Mathf.Clamp(fl_x_rotation, -45, 45);

        BC_PC_cam.transform.localRotation = Quaternion.Euler(fl_x_rotation, 0, 0);

        // Takes inputs and transforms the camera
        transform.Rotate(Vector3.up * fl_mouse_x);
        BC_PC_cam.transform.Rotate(fl_x_rotation, 0, 0);

        }
    private void PC_Move()
    {  

        if (BC_PC_cc.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) && !stats.GetRegenStamina() && stats.canSprint)
            {
                fl_speed = stats.GetRunSpeed();
                BC_PC_cam.fieldOfView = runFOV;
                stats.SetStamina(stats.GetStamina() - 0.3f);
                stats.isSprinting = true;
            }

            else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
            {
                fl_speed = stats.GetCrouchSpeed();
                BC_PC_cc.height = in_cc_height / 4;
                stats.isSprinting = false;
            }
            else
            {
                fl_speed = stats.GetWalkSpeed();
                BC_PC_cc.height = in_cc_height;
                stats.SetStamina(stats.GetStamina() + 1);
                BC_PC_cam.fieldOfView = initialFOV;
                stats.isSprinting = false;
            }
            v3_move_direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            v3_move_direction = fl_speed * transform.TransformDirection(v3_move_direction);

            fl_current_jump_count = fl_jump_count;

            if (Input.GetButton("Jump"))
            {
                stats.SetStamina(-10);
                v3_move_direction.y = fl_jump_force;
            }
        }
        v3_move_direction.y -= fl_gravity * Time.deltaTime;
        BC_PC_cc.Move(v3_move_direction * Time.deltaTime);
    }

    void HeadBobbing()
    {
        if (bl_can_camera_bob && IsMoving())
        {
            float waveslice = Mathf.Sin(headBobbingTimer);
            headBobbingTimer += bobbingSpeed;

            if (headBobbingTimer > Mathf.PI * 2)
            {
                headBobbingTimer = headBobbingTimer - (Mathf.PI * 2);
            }

            if (waveslice != 0)
            {
                float translateChange = waveslice * bobbingAmount;

                // Create a temporary Vector3 to modify the y component
                Vector3 newPosition = BC_PC_cam.transform.localPosition;
                newPosition.y = Mathf.Abs(translateChange);

                // Assign the modified vector back to player_camera.transform.localPosition
                BC_PC_cam.transform.localPosition = newPosition;
            }
            else BC_PC_cam.transform.localPosition = new Vector3(BC_PC_cam.transform.localPosition.x, 0, BC_PC_cam.transform.localPosition.z);
        }

        else
        {
            // Reset head bobbing when not moving
            headBobbingTimer = 0.0f;
            BC_PC_cam.transform.localPosition = new Vector3(BC_PC_cam.transform.localPosition.x, 0, BC_PC_cam.transform.localPosition.z);
        }
    }
    bool IsMoving()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        return Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
    }

}


