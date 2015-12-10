using System.Collections;
using UnityEngine;

public class ReflectBehaviour : MonoBehaviour {

    [SerializeField]
    Vector4 m_plane;
    [SerializeField]
    GameObject m_sphere;
    [SerializeField]
    GameObject m_reflectSphere;

    [Header("旋转速度(度单位)")]
    [SerializeField]
    float m_rotateSpeed = 1.0f;

    Matrix4x4 m_reflectMatrix;
    Quaternion m_rotateQuaternion;

    void Start() {
        // build matrix data
        BuildRotateMatrix();
        BuildRefectMatrix();

        // do reflect at start
        Reflect();
    }
	
	void Update() {
        var position = m_sphere.transform.position;
        var newPosition = m_rotateQuaternion * position;
        m_sphere.transform.position = newPosition;

        Reflect();
	}

    void BuildRotateMatrix() {
        m_rotateQuaternion = Quaternion.AngleAxis(m_rotateSpeed, new Vector3(0, 0, 1));
    }

    void BuildRefectMatrix() {
        m_reflectMatrix = Matrix4x4.identity;
        m_reflectMatrix.m00 = 1 - 2 * m_plane.x * m_plane.x;
        m_reflectMatrix.m01 = -2 * m_plane.x * m_plane.y;
        m_reflectMatrix.m02 = -2 * m_plane.x * m_plane.z;
        m_reflectMatrix.m03 = -2 * m_plane.x * m_plane.w;
        m_reflectMatrix.m10 = -2 * m_plane.x * m_plane.y;
        m_reflectMatrix.m11 = 1 - 2 * m_plane.y * m_plane.y;
        m_reflectMatrix.m12 = -2 * m_plane.y * m_plane.z;
        m_reflectMatrix.m13 = -2 * m_plane.y * m_plane.w;
        m_reflectMatrix.m20 = -2 * m_plane.x * m_plane.z;
        m_reflectMatrix.m21 = -2 * m_plane.y * m_plane.z;
        m_reflectMatrix.m22 = 1 - 2 * m_plane.z * m_plane.z;
        m_reflectMatrix.m23 = -2 * m_plane.z * m_plane.w;
        m_reflectMatrix.m30 = 0;
        m_reflectMatrix.m31 = 0;
        m_reflectMatrix.m32 = 0;
        m_reflectMatrix.m33 = 1;
    }

    void Reflect() {
        var position = m_sphere.transform.position;
        var reflectPosition = m_reflectMatrix.MultiplyPoint(position);
        m_reflectSphere.transform.position = reflectPosition;
    }

}
