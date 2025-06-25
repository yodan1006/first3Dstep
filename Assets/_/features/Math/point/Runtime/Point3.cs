using UnityEngine;

namespace Math.Point
{
    public class Point3 
    {
        public float x;
        public float y;
        public float z;


        public Point3()
        {
            x = 0f;
            y = 0f;
            z = 0f;
        }

        public Point3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public static Point3 operator +(Point3 p1, Vector3 p2)
        {
            return new Point3(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
        }
        
        public static Point3 operator +(Point3 a, Point3 b)
        {
            return new Point3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Point3 operator -(Point3 a, Point3 b) =>
            new Point3(a.x - b.x, a.y - b.y, a.z - b.z);
        
        public static Point3 operator *(Point3 a, Point3 b) =>
            new Point3(a.x * b.x, a.y * b.y, a.z * b.z);
        
        public static Point3 operator /(Point3 a, Point3 b) =>
            new Point3(a.x / b.x, a.y / b.y, a.z / b.z);
        
        public static Point3 operator *(Point3 a, float b) =>
            new Point3(a.x * b, a.y * b, a.z * b); //Point3 * 2
        
        public static Point3 operator /(Point3 a, float b) =>
            new Point3(a.x / b, a.y / b, a.z / b); //Point3 / 2

        public float Length() => Mathf.Sqrt(x * x + y * y + z * z);
        
        public float LengthSquared() => x*x + y*y + z*z;

        public Point3 Normalized()
        {
            float leght = Length();
            if (leght == 0) return new Point3(0, 0, 0);
            return new Point3(x/leght, y/leght, z/leght);
        }
        

        // public Point3 AddPoint(Point3 point)
        // {
        //     Point3 result = new Point3();
        //     result.x = x + point.x;
        //     result.y = y + point.y;
        //     result.z = z + point.z;
        //     return result;
        // }
        //
        // public Point3 SubtractPoint(Point3 point)
        // {
        //     Point3 result = new Point3();
        //     result.x = x - point.x;
        //     result.y = y - point.y;
        //     result.z = z - point.z;
        //     return result;
        // }
    }
}
