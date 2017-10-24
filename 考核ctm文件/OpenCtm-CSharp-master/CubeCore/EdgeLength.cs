using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeCore
{
    public class EdgeLength
    {
        private uint length;//长方体的长
        public uint Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }
        private uint height;//长方体的高
        public uint Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }
        private uint side;//正方体的边长
        public uint Side
        {
            get
            {
                return side;
            }

            set
            {
                side = value;
            }
        }
        private uint width;//长方体的宽
        public uint Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public EdgeLength() { }



        public EdgeLength(uint s)
        {
            this.Side = s;
        }


        public EdgeLength(uint length, uint width, uint height)
        {
            this.Length = length;
            this.Height = height;
            this.Width = width;

        }



    }
}
