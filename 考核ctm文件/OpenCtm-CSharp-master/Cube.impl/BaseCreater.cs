using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.impl
{
   public class BaseCreater
    {
        /// <summary>
        /// 根据传入的点和下标计算出normal
        /// </summary>
        /// <param name="spoints"></param>
        /// <param name="sindex"></param>
        /// <returns></returns>
        protected float[] CountNormal(float[] spoints, int[] sindex)
        {

            //计算点的个数
            int pointNums = spoints.Length / 3;

            //计算索引所围成三角形个数
            int indexFacesNums = sindex.Length / 3;

            //存放点下标索引的集合
            Vectorer[] pointQueue = new Vectorer[pointNums];

            //存贮根据索引构成的最小三角形面的单位法向量
            Vectorer[] normalQueue = new Vectorer[indexFacesNums];

            //存贮顶点的法向量
            Vectorer[] pointNormal = new Vectorer[pointNums];

            float[] normal = new float[spoints.Length];


            //把一维数组变成向量对象，并且存到点向量的集合中
            for (int i = 0; i < pointNums; i++)
            {
                Vectorer points = new Vectorer();
                points.X = spoints[i * 3 + 0];
                points.Y = spoints[i * 3 + 1];
                points.Z = spoints[i * 3 + 2];
                pointQueue[i] = points;
            }


            //计算索引构成三角形面的法向量，并且单位化向量然后存到面向量的集合中                
            for (int i = 0; i < indexFacesNums; i++)
            {
                int point0 = 0;
                int point1 = 0;
                int point2 = 0;

                Vectorer s0 = new Vectorer();
                Vectorer s1 = new Vectorer();
                Vectorer sn = new Vectorer();
                point0 = sindex[i * 3];
                point1 = sindex[i * 3 + 1];
                point2 = sindex[i * 3 + 2];

                s0 = s0.GetSubVector(pointQueue[point0], pointQueue[point1]);
                s1 = s1.GetSubVector(pointQueue[point1], pointQueue[point2]);
                sn = s0.CrossVector(s1);
                normalQueue[i] = sn.NormalSize();

            }

            //计算共点的顶点的法向量
            int itemTimes = 0;

            for (int i = 0; i < pointNums; i++)
            {

                itemTimes = 0;//用来记录点出现的次数,每次循环时候清0
                              //查询三角形下标索引出现的次数和所在三角形面的索引
                for (int j = 0; j < sindex.Length; j++)
                {
                    int a = sindex[j];
                    if (i == sindex[j])
                    {
                        itemTimes += 1;
                        int faceIndex = j / 3;
                        //实例化对象接受数组集合中的对象
                        Vectorer normalface = new Vectorer();
                        normalface = normalQueue[faceIndex];
                        Vectorer pointnormal = new Vectorer();

                        pointnormal.X += normalface.X;
                        pointnormal.Y += normalface.Y;
                        pointnormal.Z += normalface.Z;
                        pointNormal[i] = pointnormal;
                    }
                    else
                    {
                        itemTimes += 0;
                    }
                }


                //开始计算点的法向量
                Vectorer pointnormal1 = new Vectorer();
                pointnormal1 = pointNormal[i];
                pointnormal1.X = pointnormal1.X / itemTimes;
                pointnormal1.Y = pointnormal1.Y / itemTimes;
                pointnormal1.Z = pointnormal1.Z / itemTimes;
                pointNormal[i] = pointNormal[i].NormalSize();

            }

            //把计算出的点的法向量变成一个float数组对象
            for (int i = 0; i < pointNums; i++)
            {
                Vectorer pointnormal2 = new Vectorer();
                pointnormal2 = pointNormal[i];
                normal[i * 3 + 0] = pointnormal2.X;
                normal[i * 3 + 1] = pointnormal2.Y;
                normal[i * 3 + 2] = pointnormal2.Z;
            }

            return normal;
        }
    }
}
