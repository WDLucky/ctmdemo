using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeCore
{
    /// <summary>
    /// 创建正方体的数据接口
    /// </summary>
    public  interface ICreateCube
    {
        void GetCubeData(EdgeLength length, bool TrueOrFalse);
    }
}
