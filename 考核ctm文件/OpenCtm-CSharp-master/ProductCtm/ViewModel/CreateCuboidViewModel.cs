using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductCtm.ViewModel
{
  
    class CreateCuboidViewModel
    {
        public int Length { set; get; }
        public int Wide { set; get; }
        public int Height { set; get; }
        public bool TrueOrFase { set; get; }


        //用于生成按钮的功能
        public ICommand ProductCommand { private set; get; }
    }
}
