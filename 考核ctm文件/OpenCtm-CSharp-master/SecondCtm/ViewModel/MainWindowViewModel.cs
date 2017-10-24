
using CubeCore;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;

namespace SecondCtm.ViewModel
{

    /// <summary>
    /// 程序运行需要把，ipml的dll文件考到secondCtm中
    /// </summary>


    [Export]
    public class MainWindowViewModel : NotificationObject
    {

        //选中当前的tab页面  
        private TableViewModel selectGroupItem;
        public TableViewModel SelectGroupItem
        {
            get
            {
                return selectGroupItem;
            }

            set
            {
                selectGroupItem = value;
                RaisePropertyChanged("SelectGroupItem");
            }
        }

        //标签页面的内容的填充  
        private ObservableCollection<TableViewModel> _taleViewModels = new ObservableCollection<TableViewModel>();
        public ObservableCollection<TableViewModel> TableViewModels
        {
            get { return _taleViewModels; }
            set
            {
                _taleViewModels = value;
                RaisePropertyChanged("TableViewModels");

            }
        }

        //当前选中的生成方式  
        private bool concurrent;
        public bool Concurrent
        {
            get
            {
                return concurrent;
            }

            set
            {
                concurrent = value;
                RaisePropertyChanged("Concurrent");
            }
        }

        /// <summary>
        /// 初始化绑定数据
        /// </summary>
        public MainWindowViewModel()
        {
            //创建一个正方形生成标签页面对象
            var edge = new EdgeLengthViewModel(1);
            TableViewModel cube = new TableViewModel(0, "正方形生成", edge, "Visiable", "Hidden");
            TableViewModels.Add(cube);

            //创建一个长方形生成标签页面对象
            var threedege = new EdgeLengthViewModel(1, 1, 1);
            TableViewModel cuboid = new TableViewModel(1, "长方形生成", threedege, "Hidden", "Visiable");
            TableViewModels.Add(cuboid);

            //默认选中正方形的生成标签页面
            SelectGroupItem = TableViewModels[0];

            //默认选中共点的生成方式
            Concurrent = true;

            //点击生成按钮，根据选择生成相应图形
            RegisterCommand();


        }

        /// <summary>
        /// 创建监听生成方式按钮的事件对象
        /// </summary>
        public ICommand CreateCommand { get; private set; }


        private void RegisterCommand()
        {
            CreateCommand = new DelegateCommand(Create);
        }

        private void Create()
        {
            if (SelectGroupItem == TableViewModels[0])
            {
                //接收用户输入的值,并且避免用户输入错误的值（默认1的正确值），造成不能生成文件的问题
                uint sideData = SelectGroupItem.EdgeLengthData.Side==0?1: SelectGroupItem.EdgeLengthData.Side;
                EdgeLength eside = new EdgeLength(sideData);
                var cubeCraeter = MyBootstrapper.Container.GetExportedValue<ICreateCube>("Cube");
                cubeCraeter.GetCubeData(eside, Concurrent);

            }
            if (SelectGroupItem == TableViewModels[1])
            {
                uint flength= SelectGroupItem.EdgeLengthData.Length==0 ? 1 : SelectGroupItem.EdgeLengthData.Length;
                uint fheight = SelectGroupItem.EdgeLengthData.Height == 0 ? 1 : SelectGroupItem.EdgeLengthData.Height;
                uint fwidth = SelectGroupItem.EdgeLengthData.Width == 0 ? 1 : SelectGroupItem.EdgeLengthData.Width;
                //用来接收长方体对象三维数据,并且避免用户输入错误信息（默认1的正确值），造成不能生成文件的问题
                EdgeLength ethreeData = new EdgeLength(flength,fwidth,fheight);
                var cubeCraeter = MyBootstrapper.Container.GetExportedValue<ICreateCube>("Cuboid");
                cubeCraeter.GetCubeData(ethreeData, Concurrent);
            }
        }
    }
}
