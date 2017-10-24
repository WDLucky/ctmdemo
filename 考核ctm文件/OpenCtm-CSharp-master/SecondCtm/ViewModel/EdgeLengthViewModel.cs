using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondCtm
{
    
    /// <summary>
    /// 设置六面体的长宽高和棱长的属性
    /// </summary>
    [Export]
    public class EdgeLengthViewModel: NotificationObject
    {
        //长方体的长
        private uint length;

        //长方体的宽
        private uint width;

        //长方体的高
        private uint height;

        //正方体的边长
        private uint side;

        public uint Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
                RaisePropertyChanged("Length");

            }
        }

        public uint Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
                RaisePropertyChanged("Width");
            }
        }

        public uint Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
                RaisePropertyChanged("Height");
            }
        }

        public uint Side
        {
            get
            {
                return side;
            }

            set
            {
                side = value;
                RaisePropertyChanged("Side");
            }
        }

        public EdgeLengthViewModel() { }

        /// <summary>
        /// 构建长方体数据
        /// </summary>
        /// <param name="l">长</param>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        public EdgeLengthViewModel(uint l, uint w, uint h)
        {
            this.Length = l;
            this.Width = w;
            this.Height= h;
        }

        /// <summary>
        /// 构造函数构建正方体边长
        /// </summary>
        /// <param name="s">棱长</param>
        public EdgeLengthViewModel(uint s)
        {
            this.Side = s;
        }




    }
}
