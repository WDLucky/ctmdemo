using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.impl
{
    class Vectorer
    {
        public float X { set; get; }
        public float Y { set; get; }
        public float Z { set; get; }

        public Vectorer() { }
        public Vectorer(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// 得到空间两个点之间向量
        /// </summary>
        /// <param name="v1">第一个点的空间坐标位置</param>
        /// <param name="v2">第二个空间点的坐标位置</param>
        /// <returns>两个点构成的空间向量</returns>
        public Vectorer GetSubVector(Vectorer v1, Vectorer v2)
        {
            Vectorer subVector = new Vectorer();
            subVector.X = v2.X - v1.X;
            subVector.Y = v2.Y - v1.Y;
            subVector.Z = v2.Z - v1.Z;
            return subVector;
        }

        /// <summary>
        /// 计算一个平面的法向量
        /// </summary>
        /// <param name="v1">平面的一个向量</param>
        /// <param name="v2">平面的另一个向量</param>
        /// <returns>平面的法向量</returns>
        public Vectorer CrossVector(Vectorer v2)
        {
            Vectorer normalVector = new Vectorer();
            normalVector.X = Y * v2.Z - v2.Y * Z;
            normalVector.Y = v2.X * Z - X * v2.Z;
            normalVector.Z = X * v2.Y - v2.X * Y;
            return normalVector;
        }

        /// <summary>
        /// 单位化向量
        /// </summary>
        /// <param name="v">传进去的空间向量</param>
        /// <returns>单位化后的向量输出</returns>
        public Vectorer NormalSize()
        {
            Vectorer initVector = new Vectorer();
            float sum = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            initVector.X = X / sum;
            initVector.Y = Y / sum;
            initVector.Z = Z / sum;
            return initVector;

        }
    }
}
