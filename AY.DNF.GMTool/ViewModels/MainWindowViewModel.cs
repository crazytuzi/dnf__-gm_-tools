using AY.DNF.GMTool.Common;
using AY.DNF.GMTool.Common.Lib;
using AY.DNF.GMTool.Common.Npk;
using AY.DNF.GMTool.Db;
using AY.DNF.GMTool.Db.Models;
using AY.DNF.GMTool.Db.Services;
using AY.DNF.GMTool.Enums;
using AY.DNF.GMTool.Helpers;
using AY.DNF.GMTool.Models;
using HandyControl.Controls;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TiaoTiaoCode.NLogger;

namespace AY.DNF.GMTool.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        readonly IModuleManager _moduleMng;
        readonly IRegionManager _regionMng;

        readonly Task _timeTask;
        readonly CancellationTokenSource _timeTaskCancelTokenSource;

        readonly Task _pingTask;
        readonly CancellationTokenSource _pingTaskCancelTokenSource;

        string? _downloadUrl;

        System.Drawing.Color _redStateColor = System.Drawing.Color.Red;
        System.Drawing.Color _greenStateColor = System.Drawing.Color.LimeGreen;
        System.Drawing.Color _redEffectColor = System.Drawing.Color.PaleVioletRed;
        System.Drawing.Color _greenEffectColor = System.Drawing.Color.LightGreen;

        #region 属性

        private string _title = "DNF GM Tool";
        /// <summary>
        /// 工具栏标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #region 版本相关

        private string _version = "1.1.20240307";
        /// <summary>
        /// 显示的自定义版本号
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { SetProperty(ref _version, value); }
        }

        private string? _lastReleaseVersion;
        /// <summary>
        /// 最后发布版本
        /// </summary>
        public string? LastReleaseVersion
        {
            get { return _lastReleaseVersion; }
            set { SetProperty(ref _lastReleaseVersion, value); }
        }

        string? _lastVersionBody = string.Empty;
        public string? LastVersionBody
        {
            get { return _lastVersionBody; }
            set { SetProperty(ref _lastVersionBody, value); }
        }

        private Visibility _isShowDownload = Visibility.Hidden;

        public Visibility IsShowDownload
        {
            get { return _isShowDownload; }
            set { SetProperty(ref _isShowDownload, value); }
        }

        #endregion

        private DateTime _sysTime;
        /// <summary>
        /// 系统时间
        /// </summary>
        public DateTime SysTime
        {
            get { return _sysTime; }
            set { SetProperty(ref _sysTime, value); }
        }


        private bool _connectedForEnabled = false;
        /// <summary>
        /// 是否成功连接数据库启用功能
        /// </summary>
        public bool ConnectedForEnabled
        {
            get { return _connectedForEnabled; }
            set { SetProperty(ref _connectedForEnabled, value); }
        }

        private bool _openEnabled = true;
        /// <summary>
        /// 连接按钮可用性
        /// </summary>
        public bool OpenEnabled
        {
            get { return _openEnabled; }
            set { SetProperty(ref _openEnabled, value); }
        }

        #region 数据库连接相关

        private string _server = "";
        /// <summary>
        /// Server
        /// </summary>
        public string Server
        {
            get { return _server; }
            set { SetProperty(ref _server, value); }
        }

        private string _userName = "";
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private int _port = 3306;
        /// <summary>
        /// 连接端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { SetProperty(ref _port, value); }
        }

        private string _pwd = "";
        /// <summary>
        /// 数据库密码
        /// </summary>
        public string Pwd
        {
            get { return _pwd; }
            set { SetProperty(ref _pwd, value); }
        }

        #endregion

        private string _searchAccount = "";
        /// <summary>
        /// 查询账号
        /// </summary>
        public string SearchAccount
        {
            get { return _searchAccount; }
            set { SetProperty(ref _searchAccount, value); }
        }

        private LoginBindableModel? _loginInfo;
        /// <summary>
        /// 登录信息
        /// </summary>
        public LoginBindableModel? LoginInfo
        {
            get { return _loginInfo; }
            set { SetProperty(ref _loginInfo, value); }
        }

        private CurMemberInfoModel? _curMemberInfo;
        /// <summary>
        /// 当前选中角色信息
        /// </summary>
        public CurMemberInfoModel? CurMemberInfo
        {
            get { return _curMemberInfo; }
            set { SetProperty(ref _curMemberInfo, value); }
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        public ObservableCollection<SimpleMemberInfoModel> MemberInfos { get; set; } = new ObservableCollection<SimpleMemberInfoModel>();

        private string? _exeFilePath;
        /// <summary>
        /// 游戏启动器程序路径
        /// </summary>
        public string? ExeFilePath
        {
            get { return _exeFilePath; }
            set
            {
                SetProperty(ref _exeFilePath, value);
                WriteCfg();
            }
        }

        #region 服务器状态

        private SolidColorBrush _stateColor = new SolidColorBrush(Color.FromArgb(System.Drawing.Color.LightGray.A, System.Drawing.Color.LightGray.R, System.Drawing.Color.LightGray.G, System.Drawing.Color.LightGray.B));
        /// <summary>
        /// 状态灯颜色
        /// </summary>
        public SolidColorBrush StateColor
        {
            get { return _stateColor; }
            set { SetProperty(ref _stateColor, value); }
        }

        private Color _effectColor;
        /// <summary>
        /// 状态灯效果颜色
        /// </summary>
        public Color EffectColor
        {
            get { return _effectColor; }
            set { SetProperty(ref _effectColor, value); }
        }

        private string? _pingStateStr = "正在连接...";
        /// <summary>
        /// 服务器状态
        /// </summary>
        public string? PingStateStr
        {
            get { return _pingStateStr; }
            set { SetProperty(ref _pingStateStr, value); }
        }

        #endregion

        #endregion

        #region 命令

        ICommand? _connectCommand;
        ICommand? _searchAccountCommand;
        ICommand? _disconnectCommand;
        ICommand? _gmToolCommand;

        /// <summary>
        /// 连接命令
        /// </summary>

        public ICommand ConnectCommand => _connectCommand ??= new DelegateCommand(DoConnectCommand);

        /// <summary>
        /// 查询命令
        /// </summary>
        public ICommand SearchAccountCommand => _searchAccountCommand ??= new DelegateCommand(DoSearchAccountCommand);

        /// <summary>
        /// 断开连接命令
        /// </summary>
        public ICommand DisconnectCommand => _disconnectCommand ??= new DelegateCommand(DoDisconnectCommand);

        /// <summary>
        /// GM工具菜单
        /// </summary>
        public ICommand GMToolCommand => _gmToolCommand ??= new DelegateCommand<string>(DoGMToolCommand);

        ICommand? _rowClickCommand;
        /// <summary>
        /// 角色列表点击事件
        /// </summary>
        public ICommand RowClickCommand => _rowClickCommand ??= new DelegateCommand<SimpleMemberInfoModel?>(DoRowClickCommand);

        ICommand? _changePwdCommand;
        /// <summary>
        /// 切换密码事件
        /// </summary>
        public ICommand ChangePwdCommand => _changePwdCommand ??= new DelegateCommand(DoChangePwdCommand);

        ICommand? _gameStartCommand;
        /// <summary>
        /// 使用游戏启动器启动游戏
        /// </summary>
        public ICommand GameStartCommand => _gameStartCommand ??= new DelegateCommand(DoGameStartCommand);

        #region 版本&下载

        ICommand? _showLastVersionInfoCommand;
        /// <summary>
        /// 显示最后更新版本信息
        /// </summary>
        public ICommand ShowLastVersionInfoCommand => _showLastVersionInfoCommand ??= new DelegateCommand(DoShowLastVersionInfoCommand);

        ICommand? _gotoDownloadCommand;
        /// <summary>
        /// 下载
        /// </summary>
        public ICommand GotoDownloadCommand => _gotoDownloadCommand ??= new DelegateCommand(DoGotoDownloadCommand);

        #endregion

        #endregion

        ICommand? _appCloseCommand;
        /// <summary>
        /// 应用程序关闭
        /// </summary>
        public ICommand AppCloseCommand => _appCloseCommand ??= new DelegateCommand(() =>
        {
            _timeTaskCancelTokenSource.Cancel();
            _pingTaskCancelTokenSource.Cancel();

            DbFrameworkScope.TaiwanCain2nd?.Close();
            DbFrameworkScope.TaiwanCain?.Close();
            DbFrameworkScope.DTaiwan?.Close();
            DbFrameworkScope.TaiwanBilling?.Close();

            Application.Current.Shutdown();
        });

        public MainWindowViewModel(IModuleManager moduleManager, IRegionManager regionManager)
        {
            _moduleMng = moduleManager;
            _regionMng = regionManager;

            LoadCfg();

            #region 系统时间

            _timeTaskCancelTokenSource = new CancellationTokenSource();
            var ct = _timeTaskCancelTokenSource.Token;

            _timeTask = new Task(() =>
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    while (true)
                    {
                        if (ct.IsCancellationRequested) return;

                        SysTime = DateTime.Now;
                        Task.Delay(1000);
                    }
                });
            }, ct);
            _timeTask.Start();

            #endregion

            #region Ping服务器状态

            _pingTaskCancelTokenSource = new CancellationTokenSource();
            var pct = _pingTaskCancelTokenSource.Token;

            _pingTask = new Task(() =>
            {
                while (true)
                {
                    if (ct.IsCancellationRequested) return;

                    if (!string.IsNullOrWhiteSpace(Server))
                        PingServer();

                    // 第分钟ping一次
                    Task.Delay(60 * 1000);
                }
            });
            _pingTask.Start();

            #endregion

            Task.Run(() =>
            {
                Task.Delay(5000);
                Growl.Info("首次使用，请先进行Script.pvf导入，以便生成基础数据\r\n若版本更换也需重新导入Script.pvf");

                GetLastVersion();
            });
        }

        /// <summary>
        /// 最新版本信息
        /// </summary>
        void GetLastVersion()
        {
            _lastVersionBody = string.Empty;
            _downloadUrl = string.Empty;

            var verInfo = VersionHelper.Get();

            if (!verInfo.HasValue)
            {
                Growl.Error("获取版本信息失败");
                return;
            }

            LastReleaseVersion = verInfo.Value.tag_name;

            var hasUpdate = VersionHelper.HasUpdate(Version, verInfo.Value.tag_name);
            if (hasUpdate)
                IsShowDownload = Visibility.Visible;
            else
                IsShowDownload = Visibility.Hidden;

            _downloadUrl = verInfo.Value.ReleaseAssets.Value.browser_download_url;
            _lastVersionBody = verInfo.Value.body;
        }

        /// <summary>
        /// Ping服务器，检测服务器是否在线
        /// </summary>
        async void PingServer()
        {
            var p = new Ping();
            var pp = await p.SendPingAsync(Server);

            if (pp.Status == IPStatus.Success && PingStateStr != "服务端已上线")
                Application.Current.Dispatcher.Invoke(() =>
                {
                    StateColor = new SolidColorBrush(Color.FromArgb(_greenStateColor.A, _greenStateColor.R, _greenStateColor.G, _greenStateColor.B));
                    EffectColor = Color.FromArgb(_greenEffectColor.A, _greenEffectColor.R, _greenEffectColor.G, _greenEffectColor.B);
                    PingStateStr = "服务端已上线";
                });

            else if (pp.Status != IPStatus.Success)
                Application.Current.Dispatcher.Invoke(() =>
                {
                    StateColor = new SolidColorBrush(Color.FromArgb(_redStateColor.A, _redStateColor.R, _redStateColor.G, _redStateColor.B));
                    EffectColor = Color.FromArgb(_redEffectColor.A, _redEffectColor.R, _redEffectColor.G, _redEffectColor.B);
                    PingStateStr = "服务端未上线";
                    if (ConnectedForEnabled)
                        DoDisconnectCommand();
                });
        }

        #region 命令实现

        /// <summary>
        /// 数据库连接
        /// </summary>
        void DoConnectCommand()
        {
            bool b;
            try
            {
                b = DbFrameworkScope.Init(Server, UserName, Pwd, Port);
            }
            catch (Exception ex)
            {
                Growl.Error(ex.Message);
                return;
            }
            if (!b)
            {
                OpenEnabled = true;
                ConnectedForEnabled = false;
                Growl.Error("数据库连接失败");
                return;
            }

            Growl.Success("连接成功");
            OpenEnabled = false;
            ConnectedForEnabled = true;

            WriteCfg();

            PingServer();
        }

        /// <summary>
        /// 账号查询
        /// </summary>
        async void DoSearchAccountCommand()
        {
            MemberInfos.Clear();

            try
            {
                var loginInfo = await new LoginService().Login(SearchAccount);

                if (loginInfo == null)
                {
                    Growl.Error("未查询到信息");
                    return;
                }

                WriteCfg();

                LoginInfo = new LoginBindableModel
                {
                    AccountName = loginInfo.AccountName,
                    DCoin = loginInfo.DCoin,
                    DPoint = loginInfo.DPoint,
                    Status = loginInfo.Status,
                    Uid = loginInfo.Uid,
                    VIP = loginInfo.VIP
                };

                MemberInfos.AddRange(loginInfo.MemberInfos);
            }
            catch (Exception ex)
            {
                TiaoTiaoNLogger.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        void DoDisconnectCommand()
        {
            DbFrameworkScope.Disconnect();

            OpenEnabled = true;
            ConnectedForEnabled = false;
            _timeTaskCancelTokenSource.Cancel();
        }

        /// <summary>
        /// GM工具菜单
        /// </summary>
        void DoGMToolCommand(string menu)
        {
            var moudelName = $"{menu}Module";
            if (!_moduleMng.ModuleExists(moudelName) || _moduleMng.GetModuleState(moudelName) != ModuleState.Initialized)
                _moduleMng.LoadModule(moudelName);


            var module = _moduleMng.Modules.FirstOrDefault(t => t.ModuleName == moudelName);
            if (module == null) return;

            _regionMng.RequestNavigate("ContentRegion", module.ModuleName);
        }

        /// <summary>
        /// 角色列表点击事件
        /// </summary>
        async void DoRowClickCommand(SimpleMemberInfoModel? memberInfo)
        {
            if (memberInfo == null) return;

            var detailInfo = await new MemberService().GetDetailMemberInfo(memberInfo.CharacNo);
            if (detailInfo == null)
            {
                CurMemberInfo = null;
                return;
            }

            CurMemberInfo = new CurMemberInfoModel
            {
                CharacNo = detailInfo.CharacNo.ToString(),
                CharacName = detailInfo.CharacName!.ToString(),
                ExpertJob = ((ExpertJobType)Enum.Parse(typeof(ExpertJobType), detailInfo.ExpertJob.ToString())).ToString(),
                LastPlayTime = detailInfo.LastPlayTime,
                Level = detailInfo.Level,
                Money = detailInfo.Money,
                VIP = detailInfo.VIP
            };


            var jobArr = detailInfo.Job!.Split("_", StringSplitOptions.RemoveEmptyEntries);
            var jobs = await new GMToolService().GetJobs(int.Parse(jobArr[0]));
            var baseJobId = int.Parse(jobArr[0]);
            var growJobId = int.Parse(jobArr[1]);
            var job = jobs.First(t => t.JobId == baseJobId);
            // 基础职业
            CurMemberInfo.Job = job.JobName;

            // 觉醒次数
            var jxCount = growJobId / 16;
            CurMemberInfo.JxCount = jxCount;

            var jx = growJobId % 16;
            // 新职业，单机版的可能都是一转职业的最后一个顺位的职业
            var jxJob = job.GrowJobs!.FirstOrDefault(t => t.JobId == jx);
            if (jxJob == null)// 兼容性
            {
                CurMemberInfo.JxNames = "未觉醒";
                Growl.Error("未能找到相关职业信息，请联系管理员\r\n可能与游戏版本或数据有关");
                var str = job.GrowJobs == null ? "觉醒职业信息为空" : JsonConvert.SerializeObject(job.GrowJobs);
                TiaoTiaoNLogger.LogDebug($"【职业信息问题】\r\n职业数据\r\n{str}\r\n查询数据{jx}");
            }
            else
            {
                if (jxCount == 0)
                    CurMemberInfo.JxNames = "未觉醒";
                else
                {
                    if (jxJob.GrowJobs == null || jxJob.GrowJobs.Count <= 0)
                        CurMemberInfo.JxNames = jxJob.JobName;
                    else
                    {
                        var jxNames = jxJob.GrowJobs.Where(t => t.JobId <= jxCount).ToList();
                        CurMemberInfo.JxNames = jxNames.Aggregate(string.Empty, (x, y) => x += $"{y.JobName} -> ").TrimEnd('-', '>', ' ');
                    }
                }
            }
        }

        /// <summary>
        /// 切换密码
        /// </summary>
        void DoChangePwdCommand()
        {
            if (string.IsNullOrWhiteSpace(Pwd))
                Pwd = "123456";
            else if (Pwd == "123456")
                Pwd = "uu5!^%jg";
            else
                Pwd = "123456";
        }

        /// <summary>
        /// 启动游戏
        /// </summary>
        void DoGameStartCommand()
        {
            if (string.IsNullOrWhiteSpace(ExeFilePath))
            {
                Growl.Warning("请选择启动程序");
                return;
            }
            if (!File.Exists(ExeFilePath))
            {
                Growl.Warning("启动程序不存在");
                return;
            }

            var psi = new ProcessStartInfo(ExeFilePath);
            psi.WorkingDirectory = Path.GetDirectoryName(ExeFilePath);

            var ps = new Process();
            ps.StartInfo = psi;

            ps.Start();
        }

        /// <summary>
        /// 显示最后版本信息
        /// </summary>
        void DoShowLastVersionInfoCommand()
        {
            HandyControl.Controls.MessageBox.Show(_lastVersionBody, _lastReleaseVersion);
        }

        void DoGotoDownloadCommand()
        {
            if (string.IsNullOrWhiteSpace(_downloadUrl))
            {
                Growl.Error("未获取到最新程序下载地址，请手动下载");
                return;
            }

            var updaterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoUpdater", "AY.DNF.GMTool.AutoUpdater.exe");
            if (!File.Exists(updaterPath))
            {
                Growl.Error("自动更新器不存在，请重新下载完整版");
                return;
            }

            try
            {
                var pi = new ProcessStartInfo(updaterPath, _downloadUrl) { UseShellExecute = true };
                Process.Start(pi);
            }
            catch (Exception ex)
            {
                TiaoTiaoNLogger.LogError("自动更新异常：" + ex.Message);
            }

            Process.GetCurrentProcess().Kill();
        }

        #endregion

        #region 连接配置

        /// <summary>
        /// 读取连接相关信息
        /// </summary>
        void LoadCfg()
        {
            var path = "settings.dat";
            if (!File.Exists(path)) return;

            var str = File.ReadAllText(path);
            var cfg = JsonConvert.DeserializeObject<ConnectInfoModel>(str);
            if (cfg == null) return;

            Server = cfg.IP;
            Port = cfg.Port;
            UserName = cfg.UserName;
            Pwd = cfg.Pwd;
            SearchAccount = cfg.Account;
            ExeFilePath = cfg.ExeFilePath;
        }

        /// <summary>
        /// 连接相关信息写入本地文件
        /// </summary>
        void WriteCfg()
        {
            var path = "settings.dat";

            var cfg = new ConnectInfoModel
            {
                IP = Server,
                Port = Port,
                UserName = UserName,
                Pwd = Pwd,
                Account = SearchAccount,
                ExeFilePath = ExeFilePath!
            };
            var str = JsonConvert.SerializeObject(cfg);

            File.WriteAllText(path, str);
        }

        #endregion    

        ICommand? _testCommand;

        public ICommand TestCommand => _testCommand ??= new DelegateCommand(DoTestCommand);

        void DoTestCommand()
        {
            var npkFilePath = @"D:\99Game\游蝶新客户端95黑金5\ImagePacks2\sprite_item.NPK";

            //var tmpA = ChineseConverter.Convert("加持的重生之魂手鐲 : [改裝 : G-3捕食者]", ChineseConversionDirection.SimplifiedToTraditional);

            var npkFile = new NpkFile(npkFilePath);
            var sprite = npkFile.NpkFiles[0].Images[94];
            var imgData = sprite.ImageBytes;
            var data = SharpZipHelper.SharpZipLibDecompress(imgData);
            var path = @"D:\test.png";
            var type = (ColorBits)Enum.Parse(typeof(ColorBits), sprite.ColorBytes[0].ToString());

            var bitmap = Bitmaps.FromArray(data, new System.Drawing.Size((int)sprite.Width, (int)sprite.Height), type);
            bitmap.Save(@"D:\test.png");
        }
    }
}

