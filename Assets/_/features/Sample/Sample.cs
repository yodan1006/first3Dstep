using System.Drawing;
using Math.Point;
using UnityEngine;

namespace Sample.Runtime
{
    public class Sample : MonoBehaviour
    {
        void Start()
        {
            Vector3 vec = new Vector3(0, 2, 3);
            Vector3 vec2 = new Vector3(1, 2, 3);
            

            Point3 point = new Point3(0, 2, 3);
            Point3 point2 = new Point3(1, 2, 3);
            
            Point3 resultadd = point + point2;
            Debug.Log(resultadd.x + "," + resultadd.y + "," + resultadd.z);
            
            Point3 resultsub = point2 - point;
            Debug.Log(resultsub.x + "," + resultsub.y + "," + resultsub.z);
            
            float resultLength = point2.Length();
            Debug.Log(resultLength);
            
            Point3 resultMul = point * point2;
            Debug.Log(resultMul.x + "," + resultMul.y + "," + resultMul.z);
            
            Point3 resultDiv = point / point2;
            Debug.Log(resultDiv.x + "," + resultDiv.y + "," + resultDiv.z);
            
            Point3 addVecPoint = point + vec;
            Debug.Log(addVecPoint.x + "," + addVecPoint.y + "," + addVecPoint.z);
        }
    }
}
