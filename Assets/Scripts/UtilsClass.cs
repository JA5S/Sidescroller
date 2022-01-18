using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class UtilsClass
    {
        //Variables
        public static float ScreenHeight = Camera.main.orthographicSize;

        //Methods
        public static float GetScreenWidth()
        {
            return Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height;
        }

        public static Quaternion EulerAnglesToQuaternion(Vector3 angles)
        {
            return Quaternion.Euler(angles);
        }

        public static void MoveRight(Transform transform, float speed)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        public static void MoveLeft(Transform transform, float speed)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

    }
}
