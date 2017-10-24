using CubeCore;
using OpenCTM;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube.impl
{

    [Export("Cube", typeof(ICreateCube))]
    public class CubeCreater : BaseCreater,ICreateCube

    {
        public void GetCubeData(EdgeLength length, bool TrueOrFalse)
        {
            //点坐标数组
            float fside = length.Side;

            if (TrueOrFalse == true)
            {
                float[] spoints = { 0, 0, 0, fside, 0, 0, fside, 0, fside, 0, 0, fside, 0, fside, fside, fside, fside, fside, fside, fside, 0, 0, fside, 0 };
                int[] sindex = {0,1,2, 2,3,0,
                                2,5,4, 4,3,2,
                                4,5,6, 6,7,4,
                                0,7,6, 6,1,0,
                                4,7,0, 0,3,4,
                                6,5,2, 2,1,6};
                float[] normal = CountNormal(spoints, sindex);

                Mesh mesh = new Mesh(spoints, normal, sindex, new AttributeData[0], new AttributeData[0]);
                //write
                MemoryStream memory = new MemoryStream();
                CtmFileWriter writer = new CtmFileWriter(memory, new RawEncoder());
                writer.encode(mesh, "test");
                System.IO.File.WriteAllBytes(@"F:\共点正方体生成.ctm", memory.ToArray());

            }
            else
            {
                float[] spoints = { 0,0,0,  fside,0,0,  fside,0,fside, 0,0,fside,
                                    0,0,fside,  fside,0,fside,  fside,fside,fside, 0,fside,fside,
                                    0,fside,fside,  fside,fside,fside,  fside,fside,0, 0,fside,0,
                                    0,fside,0,  fside,fside,0,  fside,0,0, 0,0,0,
                                    0,0,0,  0,0,fside,  0,fside,fside, 0,fside,0,
                                    fside,0,fside,  fside,0,0,  fside,fside,0, fside,fside,fside};

                int[] sindex = {0,1,2,      2,3,0,
                                4,5,6,      6,7,4,
                                8,9,10,     10,11,8,
                                12,13,14,   14,15,12,
                                16,17,18,   18,19,16,
                                20,21,22,   22,23,20};

                float[] normal = CountNormal(spoints, sindex);


                Mesh mesh = new Mesh(spoints, normal, sindex, new AttributeData[0], new AttributeData[0]);
                //write
                MemoryStream memory = new MemoryStream();
                CtmFileWriter writer = new CtmFileWriter(memory, new RawEncoder());
                writer.encode(mesh, "test");

                System.IO.File.WriteAllBytes(@"F:\不共点正方体生成.ctm", memory.ToArray());

            }
        }

    }
}

