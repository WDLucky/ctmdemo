using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenCTM;

namespace OpenCTM
{
    class demoCtm
    {
        /// <summary>
        /// 根据给出的点的数组转换成空间坐标的点
        /// </summary>
        /// <param name="points">点的一维数租</param>
        /// <returns>点的空间坐标数组i是点的编号</returns>
        public static float[,] GetVector3(float[] points)
        {
            float[,] Vector = new float[points.Length/3,3];
            for(int i=0;i< points.Length / 3; i++)
            {
                Vector[i, 0] = points[i * 3 + 0];
                Vector[i, 1] = points[i * 3 + 1];
                Vector[i, 2] = points[i * 3 + 2];
            }
            return Vector;
        }

        /// <summary>
        /// 根据传进去的points和index计算出normal的一维数组
        /// </summary>
        /// <param name="points">传进去的点的一维坐标数组</param>
        /// <param name="indexs">传进去的点的一维索引数组</param>
        /// <returns></returns>
       public static float[] GetNormals(float[] points, int[] indexs)
        {
            //获得8个点空间坐标数组
            float[,] getVector = GetVector3(points);
            //暂未单位化的面的空间坐标向量
            float[,] normal_faces_vector= new float[indexs.Length / 3, 3];
            //单位化面的空间向量
            float[,] normal_faces_vector_init= new float[indexs.Length / 3, 3];
            //输出点的法向量
            float[,] normal_points_vector = new float[points.Length / 3, 3];
            //输出点的单位化法向量
            float[,] normal_points_vector_init = new float[points.Length / 3, 3];
            //输出符合mesh需要的一维数组法向量数值
            float[] normal = new float[points.Length];

            //初始化小三角形面的法向量
            for (int i = 0; i <indexs.Length/3;i++)
            {
                normal_faces_vector[i, 0] = (getVector[indexs[i * 3 + 1], 1] - getVector[indexs[i * 3 + 0], 1]) * (getVector[indexs[i * 3 + 0], 2] - getVector[indexs[i * 3 + 2], 2]) - (getVector[indexs[i * 3 + 0], 1] - getVector[indexs[i * 3 + 2], 1]) * (getVector[indexs[i * 3 + 1], 2] - getVector[indexs[i * 3 + 0], 2]);

                normal_faces_vector[i, 1] = (getVector[indexs[i * 3 + 0], 0] - getVector[indexs[i * 3 + 2], 0]) * (getVector[indexs[i * 3 + 1], 2] - getVector[indexs[i * 3 + 0], 2]) - (getVector[indexs[i * 3 + 1], 0] - getVector[indexs[i * 3 + 0], 0]) * (getVector[indexs[i * 3 + 1], 2] - getVector[indexs[i * 3 + 0], 2]);

                normal_faces_vector[i, 2] = (getVector[indexs[i * 3 + 1], 0] - getVector[indexs[i * 3 + 0], 0]) * (getVector[indexs[i * 3 + 0], 1] - getVector[indexs[i * 3 + 2], 1]) - (getVector[indexs[i * 3 + 0], 0] - getVector[indexs[i * 3 + 2], 0]) * (getVector[indexs[i * 3 + 1], 1] - getVector[indexs[i * 3 + 0], 1]);

                //完成每个小三角形面的法向量的单位化
                normal_faces_vector_init[i, 0] = (float)(normal_faces_vector[i, 0] / Math.Sqrt(normal_faces_vector[i, 0] * normal_faces_vector[i, 0] + normal_faces_vector[i, 1] * normal_faces_vector[i, 1] + normal_faces_vector[i, 2] * normal_faces_vector[i, 2]));//x的单位化
                normal_faces_vector_init[i, 1] = (float)(normal_faces_vector[i, 1] / Math.Sqrt(normal_faces_vector[i, 0] * normal_faces_vector[i, 0] + normal_faces_vector[i, 1] * normal_faces_vector[i, 1] + normal_faces_vector[i, 2] * normal_faces_vector[i, 2]));//y的单位化
                normal_faces_vector_init[i, 2] = (float)(normal_faces_vector[i, 2] / Math.Sqrt(normal_faces_vector[i, 0] * normal_faces_vector[i, 0] + normal_faces_vector[i, 1] * normal_faces_vector[i, 1] + normal_faces_vector[i, 2] * normal_faces_vector[i, 2]));//z的单位化
            }

            
            float[,] triangle = new float[indexs.Length/3,3];//用来接收index围成的三角形面的数组
            //接受三角形面的下标索引，放到数组中便于查询
            /*for (int i=0;i<indexs.Length/3;i++)
            {
                triangle[i, 0] = indexs[i * 3 + 0];
                triangle[i, 1] = indexs[i * 3 + 1];
                triangle[i, 2] = indexs[i * 3 + 2];
            }*/

            //计算某个顶点出现的次数
            int itemTimes = 0;
            for (int i=0;i<points.Length/3;i++) {
                 itemTimes = 0;//用来记录点出现的次数,每次循环时候清0

                for (int j = 0; j < indexs.Length; j++)
                {

                    if (i == indexs[j])
                    {
                        itemTimes += 1;
                        if (j%3!=0) {
                            int faceIndex = j / 3 + 1;
                            //填充顶点的法向量的和
                            normal_points_vector[i, 0] += normal_faces_vector_init[faceIndex, 0];
                            normal_points_vector[i, 1] += normal_faces_vector_init[faceIndex, 1];
                            normal_points_vector[i, 2] += normal_faces_vector_init[faceIndex, 2];
                        }
                        else
                        {
                            int faceIndex = j / 3;
                            //填充顶点的法向量的和
                            normal_points_vector[i, 0] += normal_faces_vector_init[faceIndex, 0];
                            normal_points_vector[i, 1] += normal_faces_vector_init[faceIndex, 1];
                            normal_points_vector[i, 2] += normal_faces_vector_init[faceIndex, 2];
                        }
                        
                       /* //填充顶点的法向量的和
                        normal_points_vector[i, 0] += normal_faces_vector_init[faceIndex, 0];
                        normal_points_vector[i, 1] += normal_faces_vector_init[faceIndex, 1];
                        normal_points_vector[i, 2] += normal_faces_vector_init[faceIndex, 2];*/
                        
                    }          
                }
                //求点法向量的平均值
                float x = normal_points_vector[i,0] / itemTimes;
                float y = normal_points_vector[i,1] / itemTimes;
                float z = normal_points_vector[i,2] / itemTimes;

                //求顶点法向量的单位化向量
                normal_points_vector_init[i, 0] = (float)(x / Math.Sqrt(x * x + y * y + z * z));
                normal_points_vector_init[i, 1] = (float)(y / Math.Sqrt(x * x + y * y + z * z));
                normal_points_vector_init[i, 2] = (float)(z / Math.Sqrt(x * x + y * y + z * z));
                
            }
            for(int i = 0; i < points.Length / 3; i++)
            {
                normal[i*3+0] = normal_points_vector_init[i , 0];
                normal[i*3+1] = normal_points_vector_init[i , 1];
                normal[i*3+2] = normal_points_vector_init[i , 2];
            }
            return normal;
        }




        static void Main(string[] args)
        {
            float[] spoints = { 0, 0, 0,   2, 0, 0,    2, 0, 2,   0, 0, 2,   0, 2, 2,   2, 2, 2,   2, 2, 0,   0, 2, 0 };//固定8个点
            int[] sindex = { 0, 1, 3, 2, 3, 1, 4, 3, 5, 2, 5, 3, 6, 7, 5, 4, 5, 7, 6, 1, 7, 0, 7, 1, 2, 1, 5, 6, 5, 1, 0, 3, 7, 4, 7, 3 };//12个三角形面共点的方式 
            float[] normal = GetNormals(spoints, sindex);
            Mesh mesh = new Mesh(spoints, normal, sindex, new AttributeData[0], new AttributeData[0]);
            //write
            MemoryStream memory = new MemoryStream();
            CtmFileWriter writer = new CtmFileWriter(memory,new RawEncoder());
            writer.encode(mesh, "test");
            System.IO.File.WriteAllBytes(@"F:\正六面体.ctm", memory.ToArray());
        }
    }
}
