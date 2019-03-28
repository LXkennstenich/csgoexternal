using System;

namespace CSGOExternal.Classes
{

    /// <summary>
    /// Class that holds information about a 3d-coordinate and offers some basic operations
    /// </summary>
    public struct Vector3
    {
        #region VARIABLES
        public float X;
        public float Y;
        public float Z;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Returns a new Vector3 at (0,0,0)
        /// </summary>
        public static Vector3 Zero
        {
            get { return new Vector3(0, 0, 0); }
        }
        /// <summary>
        /// Returns a new Vector3 at (1,0,0)
        /// </summary>
        public static Vector3 UnitX
        {
            get { return new Vector3(1, 0, 0); }
        }
        /// <summary>
        /// Returns a new Vector3 at (0,1,0)
        /// </summary>
        public static Vector3 UnitY
        {
            get { return new Vector3(0, 1, 0); }
        }
        /// <summary>
        /// Returns a new Vector3 at (0,0,1)
        /// </summary>
        public static Vector3 UnitZ
        {
            get { return new Vector3(0, 0, 1); }
        }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new Vector3 using the given values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        /// <summary>
        /// Initializes a new Vector3 by copying the values of the given Vector3
        /// </summary>
        /// <param name="vec"></param>
        public Vector3(Vector3 vec) : this(vec.X, vec.Y, vec.Z) { }
        /// <summary>
        /// Initializes a new Vector3 using the given float-array
        /// </summary>
        /// <param name="values"></param>
        public Vector3(float[] values) : this(values[0], values[1], values[2]) { }
        #endregion

        #region METHODS
        /// <summary>
        /// Returns the length of this Vector3
        /// </summary>
        /// <returns></returns>
        public float Length()
        {
            return (float)System.Math.Abs(System.Math.Sqrt(System.Math.Pow(X, 2) + System.Math.Pow(Y, 2) + System.Math.Pow(Z, 2)));
        }
        /// <summary>
        /// Returns the distance from this Vector3 to the given Vector3
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public float DistanceTo(Vector3 other)
        {
            return (this - other).Length();
        }

        public override bool Equals(object obj)
        {
            Vector3 vec = (Vector3)obj;
            return this.GetHashCode() == vec.GetHashCode();
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[X={0}, Y={1}, Z={2}]", this.X.ToString(), this.Y.ToString(), this.Z.ToString());
        }
        #endregion

        #region OPERATORS
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static Vector3 operator *(Vector3 v1, float scalar)
        {
            return new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
        }
        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }
        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !(v1 == v2);
        }
        public float this[int i]
        {
            get {
                switch (i)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    case 2:
                        return this.Z;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (i)
                {
                    case 0:
                        this.X = value;
                        break;
                    case 1:
                        this.Y = value;
                        break;
                    case 2:
                        this.Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Class that holds information about a 2d-coordinate and offers some basic operations
    /// </summary>
    public struct Vector2
    {
        #region VARIABLES
        public float X;
        public float Y;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Returns a new Vector2 at (0,0)
        /// </summary>
        public static Vector2 Zero
        {
            get { return new Vector2(0, 0); }
        }
        /// <summary>
        /// Returns a new Vector3 at (1,0)
        /// </summary>
        public static Vector2 UnitX
        {
            get { return new Vector2(1, 0); }
        }
        /// <summary>
        /// Returns a new Vector2 at (0,1)
        /// </summary>
        public static Vector2 UnitY
        {
            get { return new Vector2(0, 1); }
        }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new Vector2 using the given values
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        /// <summary>
        /// Initializes a new Vector2 by copying the values of the given Vector2
        /// </summary>
        /// <param name="vec"></param>
        public Vector2(Vector2 vec) : this(vec.X, vec.Y) { }
        /// <summary>
        /// Initializes a new Vector2 using the given float-array
        /// </summary>
        /// <param name="values"></param>
        public Vector2(float[] values) : this(values[0], values[1]) { }
        #endregion

        #region METHODS
        /// <summary>
        /// Returns the length of this Vector2
        /// </summary>
        /// <returns></returns>
        public float Length()
        {
            return (float)System.Math.Sqrt(System.Math.Pow(X, 2) + System.Math.Pow(Y, 2));
        }
        /// <summary>
        /// Returns the distance from this Vector2 to the given Vector2
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public float DistanceTo(Vector2 other)
        {
            return (this + other).Length();
        }

        public override bool Equals(object obj)
        {
            Vector2 vec = (Vector2)obj;
            return this.GetHashCode() == vec.GetHashCode();
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[X={0}, Y={1}]", this.X.ToString(), this.Y.ToString());
        }
        #endregion

        #region OPERATORS
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static Vector2 operator *(Vector2 v1, float scalar)
        {
            return new Vector2(v1.X * scalar, v1.Y * scalar);
        }
        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y;
        }
        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1 == v2);
        }
        public float this[int i]
        {
            get {
                switch (i)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (i)
                {
                    case 0:
                        this.X = value;
                        break;
                    case 1:
                        this.Y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// A matrix.
    /// </summary>
    public class Matrix
    {
        #region VARIABLES
        private float[] data;
        private int rows, columns;
        #endregion

        #region CONSTRUCTOR
        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.data = new float[rows * columns];
        }
        #endregion

        #region METHODS
        public void Read(byte[] data)
        {
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < columns; x++)
                    this[y, x] = BitConverter.ToSingle(data, sizeof(float) * ((y * columns) + x));
        }
        public byte[] ToByteArray()
        {
            int sof = sizeof(float);
            byte[] data = new byte[this.data.Length * sof];
            for (int i = 0; i < this.data.Length; i++)
                Array.Copy(BitConverter.GetBytes(this.data[i]), 0, data, i * sof, sof);
            return data;
        }
        #endregion

        #region OPERANDS
        public float this[int i]
        {
            get { return data[i]; }
            set { data[i] = value; }
        }
        public float this[int row, int column]
        {
            get { return data[row * columns + column]; }
            set { data[row * columns + column] = value; }
        }
        #endregion
    }

    /// <summary>
    /// A utility-class that offers several mathematical algorithms.
    /// </summary>
    public static class MathUtils
    {
        #region VARIABLES
        private static float DEG_2_RAD = (float)(Math.PI / 180f);
        private static float RAD_2_DEG = (float)(180f / Math.PI);
        #endregion

        #region METHODS
        /// <summary>
        /// Translates an array of 3d-coordinates to screen-coodinates
        /// </summary>
        /// <param name="viewMatrix">The viewmatrix used to perform translation</param>
        /// <param name="screenSize">The size of the screen which is translated to</param>
        /// <param name="points">Array of 3d-coordinates</param>
        /// <returns>Array of translated screen-coodinates</returns>
        public static Vector2[] WorldToScreen(this Matrix viewMatrix, Vector2 screenSize, params Vector3[] points)
        {
            Vector2[] worlds = new Vector2[points.Length];
            for (int i = 0; i < worlds.Length; i++)
                worlds[i] = viewMatrix.WorldToScreen(screenSize, points[i]);
            return worlds;
        }
        /// <summary>
        /// Translates a 3d-coordinate to a screen-coodinate
        /// </summary>
        /// <param name="viewMatrix">The viewmatrix used to perform translation</param>
        /// <param name="screenSize">The size of the screen which is translated to</param>
        /// <param name="point3D">3d-coordinate of the point to translate</param>
        /// <returns>Translated screen-coodinate</returns>
        public static Vector2 WorldToScreen(this Matrix viewMatrix, Vector2 screenSize, Vector3 point3D)
        {
            Vector2 returnVector = Vector2.Zero;
            float w = viewMatrix[3, 0] * point3D.X + viewMatrix[3, 1] * point3D.Y + viewMatrix[3, 2] * point3D.Z + viewMatrix[3, 3];
            if (w >= 0.01f)
            {
                float inverseX = 1f / w;
                returnVector.X =
                    (screenSize.X / 2f) +
                    (0.5f * (
                    (viewMatrix[0, 0] * point3D.X + viewMatrix[0, 1] * point3D.Y + viewMatrix[0, 2] * point3D.Z + viewMatrix[0, 3])
                    * inverseX)
                    * screenSize.X + 0.5f);
                returnVector.Y =
                    (screenSize.Y / 2f) -
                    (0.5f * (
                    (viewMatrix[1, 0] * point3D.X + viewMatrix[1, 1] * point3D.Y + viewMatrix[1, 2] * point3D.Z + viewMatrix[1, 3])
                    * inverseX)
                    * screenSize.Y + 0.5f);
            }
            return returnVector;
        }
        /// <summary>
        /// Applies (adds) an offset to an array of 3d-coordinates
        /// </summary>
        /// <param name="offset">Offset to apply</param>
        /// <param name="points">Array if 3d-coordinates</param>
        /// <returns>Array of manipulated 3d-coordinates</returns>
        public static Vector3[] OffsetVectors(this Vector3 offset, params Vector3[] points)
        {
            for (int i = 0; i < points.Length; i++)
                points[i] += offset;
            return points;
        }
        /// <summary>
        /// Copies an array of vectors to a new array containing identical, new Vector3s (deep-copy)
        /// </summary>
        /// <param name="source">Source-array to copy from</param>
        /// <returns>New array containing identical yet new Vector3s</returns>
        public static Vector3[] CopyVectors(this Vector3[] source)
        {
            Vector3[] ret = new Vector3[source.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = new Vector3(source[i]);
            return ret;
        }
        /// <summary>
        /// Rotates a given point around another point
        /// </summary>
        /// <param name="pointToRotate">Point to rotate</param>
        /// <param name="centerPoint">Point to rotate around</param>
        /// <param name="angleInDegrees">Angle of rotation in degrees</param>
        /// <returns>Rotated point</returns>
        public static Vector2 RotatePoint(this Vector2 pointToRotate, Vector2 centerPoint, float angleInDegrees)
        {
            float angleInRadians = (float)(angleInDegrees * (Math.PI / 180f));
            float cosTheta = (float)Math.Cos(angleInRadians);
            float sinTheta = (float)Math.Sin(angleInRadians);
            return new Vector2
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }
        /// <summary>
        /// Clamps a given angle
        /// </summary>
        /// <param name="qaAng">Angle to clamp</param>
        /// <returns>Clamped angle</returns>
        public static Vector3 ClampAngle(this Vector3 qaAng)
        {

            //if (qaAng.X > 89.0f && qaAng.X <= 180.0f)
            //    qaAng.X = 89.0f;

            //while (qaAng.X > 180.0f)
            //    qaAng.X = qaAng.X - 360.0f;

            //if (qaAng.X < -89.0f)
            //    qaAng.X = -89.0f;

            //while (qaAng.Y > 180.0f)
            //    qaAng.Y = qaAng.Y - 360.0f;

            //while (qaAng.Y < -180.0f)
            //    qaAng.Y = qaAng.Y + 360.0f;

            //return qaAng;
            if (qaAng[0] > 89.0f)
                qaAng[0] = 89.0f;

            if (qaAng[0] < -89.0f)
                qaAng[0] = -89.0f;

            while (qaAng[1] > 180)
                qaAng[1] -= 360;

            while (qaAng[1] < -180)
                qaAng[1] += 360;

            qaAng.Z = 0;

            return qaAng;
        }
        /// <summary>
        /// Calculates an angle that aims from the given source-Vector3 to the given destination-Vector3
        /// </summary>
        /// <param name="src">3d-coordinate of where to aim from</param>
        /// <param name="dst">3d-coordinate of where to aim to</param>
        /// <returns></returns>
        public static Vector3 CalcAngle(this Vector3 src, Vector3 dst)
        {
            Vector3 ret = new Vector3();
            Vector3 vDelta = src - dst;
            float fHyp = (float)Math.Sqrt((vDelta.X * vDelta.X) + (vDelta.Y * vDelta.Y));

            ret.X = RadiansToDegrees((float)Math.Atan(vDelta.Z / fHyp));
            ret.Y = RadiansToDegrees((float)Math.Atan(vDelta.Y / vDelta.X));

            if (vDelta.X >= 0.0f)
                ret.Y += 180.0f;
            return ret;
        }
        /// <summary>
        /// Smooths an angle from src to dest
        /// </summary>
        /// <param name="src">Original angle</param>
        /// <param name="dest">Destination angle</param>
        /// <param name="smoothAmount">Value between 0 and 1 to apply as smooting where 0 is no modification and 1 is no smoothing</param>
        /// <returns></returns>
        public static Vector3 SmoothAngle(this Vector3 src, Vector3 dest, float smoothAmount)
        {
            return src + (dest - src) * smoothAmount;
        }
        /// <summary>
        /// Converts the given angle in degrees to radians
        /// </summary>
        /// <param name="deg">Angle in degrees</param>
        /// <returns>Angle in radians</returns>
        public static float DegreesToRadians(float deg) { return (float)(deg * DEG_2_RAD); }
        /// <summary>
        /// Converts the given angle in radians to degrees
        /// </summary>
        /// <param name="rad">Angle in radians</param>
        /// <returns>Angle in degrees</returns>
        public static float RadiansToDegrees(float rad) { return (float)(rad * RAD_2_DEG); }
        /// <summary>
        /// Returns whether the given point is within a circle of the given radius around the given center
        /// </summary>
        /// <param name="point">Point to test</param>
        /// <param name="circleCenter">Center of circle</param>
        /// <param name="radius">Radius of circle</param>
        /// <returns></returns>
        public static bool PointInCircle(this Vector2 point, Vector2 circleCenter, float radius)
        {
            return (point - circleCenter).Length() < radius;
        }
        #endregion
    }
}
