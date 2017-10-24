using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductCtm.ViewModel
{
    [Export]
    class CreateCubeViewModel
    {
        private int side;
        public bool TrueOrFase { set; get; }

        public int Side
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

        
        //用于生成按钮的功能
        public ICommand ProductCommand { private set; get; }
    }
}
