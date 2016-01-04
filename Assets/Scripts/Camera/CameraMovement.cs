using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform target;
    public PlayerAttack playerAttack;

    public float followSpeed;
    public float shootFollowSpeed;

    public float cameraSensitivityV;
	public float cameraSensitivityH;
    public float shootCameraSensitivityV;
    public float shootCameraSensitivityH;

    public float offsetMag;
    public float shootOffsetMag;

    Vector3 offset;
	float rotationH;
	float rotationV;

	void Awake () {
		offset = transform.position - target.position;
	}

    void Update()
    {
        if (!playerAttack.shootMode)
        {
            CameraNormal();
        }
        else
        {
            CameraShoot();
        }

    }

    void CameraNormal()
    {
        rotationH = Input.GetAxis("Mouse X") * cameraSensitivityH;
        rotationV = Input.GetAxis("Mouse Y") * cameraSensitivityV;

        offset = Quaternion.AngleAxis(rotationH, Vector3.up) * offset; //rotate offset

        Vector3 rotationOffset = Vector3.Cross(offset, Vector3.up).normalized;

        if ((offset.y < 2f && rotationV > 0f) || (offset.y > 0f && rotationV < 0f))
            offset = Quaternion.AngleAxis(rotationV, rotationOffset) * offset; //rotate offset

        Vector3 targetCamPos = target.position + (offset.normalized * offsetMag);


        transform.position = Vector3.Lerp(transform.position, targetCamPos, followSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    void CameraShoot()
    {
        rotationH = Input.GetAxis("Mouse X") * shootCameraSensitivityH;
        rotationV = Input.GetAxis("Mouse Y") * shootCameraSensitivityV;

        offset = Quaternion.AngleAxis(rotationH, Vector3.up) * offset; //rotate offset Horizontally

        Vector3 rotationOffset = Vector3.Cross(offset, Vector3.up).normalized;

        if ((offset.y < 2f && rotationV > 0f) || (offset.y > -1f && rotationV < 0f))
            offset = Quaternion.AngleAxis(rotationV, rotationOffset) * offset; //rotate offset Vertically

        Vector3 targetCamPos = target.position + (offset.normalized * shootOffsetMag);
        Vector3 targetOffset = targetCamPos + (rotationOffset * 0.5f) + (transform.up * 0.5f);

        transform.position = Vector3.Lerp(transform.position, targetCamPos, shootFollowSpeed * Time.deltaTime);
        transform.LookAt(target);
        transform.position = Vector3.Lerp(transform.position, targetOffset, shootFollowSpeed * Time.deltaTime);
    }
}

