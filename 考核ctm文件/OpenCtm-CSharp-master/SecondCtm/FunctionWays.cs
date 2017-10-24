using CubeCore;
using Microsoft.Practices.Prism;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCtm
{
    public class TableViewModel
    {
        //接受创建图形的三维数据
        public EdgeLengthViewModel EdgeLengthData { get; set; }
        //标签名
        public string TableName { get; set; }
        //标签id标记
        public int TableId { set; get; }
        //标记标签页的显示与否
        public string Flag1 { set; get; }
        public string Flag2 { set; get; }



        public TableViewModel(int id,string groupName,EdgeLengthViewModel edge,string f1,string f2)
        {

            EdgeLengthData = new EdgeLengthViewModel();
            this.Flag1 = f1;
            this.Flag2 = f2;
            this.TableId = id;
            this.EdgeLengthData = edge;
            this.TableName = groupName;
        }


    }
}
