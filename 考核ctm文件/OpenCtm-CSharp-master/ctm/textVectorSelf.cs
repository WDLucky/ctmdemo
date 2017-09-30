using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenCTM;


namespace ctm
{
    class textVectorSelf
    {
        static void Main(string[] args)
        {
            //共点的正方体
            //float[] spoints = { 0, 0, 0, 2, 0, 0, 2, 0, 2, 0, 0, 2, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0, 2, 0 };
            //int[] sindex = {0,1,2, 2,3,0,
            //                2,5,4, 4,3,2,
            //                4,5,6, 6,7,4,
            //                0,7,6, 6,1,0,
            //                4,7,0, 0,3,4,
            //                6,5,2, 2,1,6};

            //共点四面体
            // float[] spoints = { 0, 0, 0, 2, 0, 0, 1, 0, (float)Math.Sqrt(3), 1, (float)(2 * Math.Sqrt(6) / 3), (float)Math.Sqrt(3) / 3 };
            //int[] sindex = { 1, 0, 2, 3, 0, 1, 1, 2, 3, 0, 3, 2 };

            //共线不共点四面体0 | 0,0,0,   1 | 2,0,0,   2 | 1,0,(float)Math.Sqrt(3),  3 | 1,(float)(2 * Math.Sqrt(6) / 3),(float)(Math.Sqrt(3) / 3),
            //float[] spoints = { 0,0,0,   2,0,0,   1,0,(float)Math.Sqrt(3),
            //   1,(float)(2*Math.Sqrt(6)/3),(float)(Math.Sqrt(3)/3), 1,0,(float)Math.Sqrt(3), 2,0,0,
            //   0,0,0, 1,(float)(2*Math.Sqrt(6)/3),(float)(Math.Sqrt(3)/3),  2,0,0,
            //   1,(float)(2*Math.Sqrt(6)/3),(float)(Math.Sqrt(3)/3),  0,0,0, 1,0,(float)Math.Sqrt(3),};
            //int[] sindex = { 1, 0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };


            //共线不共点的6面体 0 | 0,0,0,  1 | 2,0,0,  2 | 2,0,2, 3 | 0,0,2,  4 | 0,2,2,  5 | 2,2,2, 6 | 2,2,0, 7 | 0,2,0,
            float[] spoints = { 0,0,0,  2,0,0,  2,0,2, 0,0,2,
                                    0,0,2,  2,0,2,  2,2,2, 0,2,2,
                                    0,2,2,  2,2,2,  2,2,0, 0,2,0,
                                    0,2,0,  2,2,0,  2,0,0, 0,0,0,
                                    0,0,0,  0,0,2,  0,2,2, 0,2,0,
                                    2,0,2,  2,0,0,  2,2,0, 2,2,2};
            int[] sindex = {0,1,2, 2,3,0,
                            4,5,6, 6,7,4,
                            8,9,10, 10,11,8,
                            12,13,14, 14,15,12,
                            16,17,18, 18,19,16,
                            20,21,22, 22,23,20};

            //计算点的个数
            int pointNums = spoints.Length / 3;

            //计算索引所围成三角形个数
            int indexFacesNums = sindex.Length / 3;

            //把传入的点的一位数组索引变成Vectorer对象
            // Vectorer points = new Vectorer();

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
                points.x = spoints[i * 3 + 0];
                points.y = spoints[i * 3 + 1];
                points.z = spoints[i * 3 + 2];
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
                sn = sn.GetNormalVector(s0, s1);
                normalQueue[i] = sn.GetInitVector(sn);

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

                        pointnormal.x += normalface.x;
                        pointnormal.y += normalface.y;
                        pointnormal.z += normalface.z;
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
                pointnormal1.x = pointnormal1.x / itemTimes;
                pointnormal1.y = pointnormal1.y / itemTimes;
                pointnormal1.z = pointnormal1.z / itemTimes;
                pointNormal[i] = pointNormal[i].GetInitVector(pointnormal1);

            }

            //把计算出的点的法向量变成一个float数组对象
            for (int i = 0; i < pointNums; i++)
            {
                Vectorer pointnormal2 = new Vectorer();
                pointnormal2 = pointNormal[i];
                normal[i * 3 + 0] = pointnormal2.x;
                normal[i * 3 + 1] = pointnormal2.y;
                normal[i * 3 + 2] = pointnormal2.z;
            }

            Mesh mesh = new Mesh(spoints, normal, sindex, new AttributeData[0], new AttributeData[0]);
            //write
            MemoryStream memory = new MemoryStream();
            CtmFileWriter writer = new CtmFileWriter(memory, new RawEncoder());
            writer.encode(mesh, "test");
            //System.IO.File.WriteAllBytes(@"F:\共点正六面体.ctm", memory.ToArray());
            //System.IO.File.WriteAllBytes(@"F:\共点正四面体.ctm", memory.ToArray());
            System.IO.File.WriteAllBytes(@"F:\共线不共点正六面体.ctm", memory.ToArray());
            //System.IO.File.WriteAllBytes(@"F:\共线不共点正四面体.ctm", memory.ToArray());




        }
    }
}
