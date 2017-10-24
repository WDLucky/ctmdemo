using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CubeCore;
using OpenCTM;
using System.IO;
using System.ComponentModel.Composition;

namespace Cube.impl
{
    [Export("Cuboid", typeof(ICreateCube))]
    public class CuboidCreater :BaseCreater, ICreateCube
    {
        public void GetCubeData(EdgeLength length, bool TrueOrFalse)
        {
            float flengeh = length.Length;
            float fheight = length.Height;
            float fwide = length.Width;

            if (TrueOrFalse == true)
            {
                float[] spoints = { 0, 0, 0,
                                    flengeh, 0, 0,
                                    flengeh, 0, fwide,
                                    0, 0, fwide,
                                    0, fheight, fwide,
                                    flengeh, fheight, fwide,
                                    flengeh, fheight, 0,
                                    0, fheight, 0 };
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
                System.IO.File.WriteAllBytes(@"F:\共点长方体生成.ctm", memory.ToArray());
            }
            else
            {
                float[] spoints = { 0,0,0,            flengeh,0,0,            flengeh,0,fwide,         0,0,fwide,
                                    0,0,fwide,        flengeh,0,fwide,        flengeh,fheight,fwide,   0,fheight,fwide,
                                    0,fheight,fwide,  flengeh,fheight,fwide,  flengeh,fheight,0,       0,fheight,0,
                                    0,fheight,0,      flengeh,fheight,0,      flengeh,0,0,             0,0,0,
                                    0,0,0,            0,0,fwide,              0,fheight,fwide,         0,fheight,0,
                                    flengeh,0,fwide,  flengeh,0,0,            flengeh,fheight,0,       flengeh,fheight,fwide};

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
                System.IO.File.WriteAllBytes(@"F:\不共点长方体生成.ctm", memory.ToArray());
            }
        }

    }
}

