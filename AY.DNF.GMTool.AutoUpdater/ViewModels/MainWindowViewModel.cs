using HandyControl.Controls;
using Prism.Commands;
using Prism.Mvvm;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using TiaoTiaoCode.NLogger;

namespace AY.DNF.GMTool.AutoUpdater.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        #region 

        private float _downloadProgress;

        public float DownloadProgress
        {
            get { return _downloadProgress; }
            set { SetProperty(ref _downloadProgress, value); }
        }

        private float _zipProgress;

        public float ZipProgress
        {
            get { return _zipProgress; }
            set { SetProperty(ref _zipProgress, value); }
        }

        private bool _finishEnabled = false;

        public bool FinishEnabled
        {
            get { return _finishEnabled; }
            set { SetProperty(ref _finishEnabled, value); }
        }

        #endregion

        #region 

        ICommand? _finishCommand;

        public ICommand FinishCommand => _finishCommand ??= new DelegateCommand(DoFinishCommand);

        void DoFinishCommand()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var path = Path.Combine(dir.Parent.FullName, "AY.DNF.GMTool.exe");
            var pi = new ProcessStartInfo(path) { UseShellExecute = true };

            var p = new Process();
            p.StartInfo = pi;

            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                Growl.Error("启动AYGMTool异常：" + ex.Message);
            }

            Process.GetCurrentProcess().Kill();
        }

        #endregion

        public void Start(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                Growl.Warning("未获取到下载连接");
                return;
            }
            DownloadZip(url);
        }

        #region 下载相关

        async void DownloadZip(string url)
        {
            try
            {
                var fileName = url.Split("/", StringSplitOptions.RemoveEmptyEntries).Last();
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "download");
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                using var client = new HttpClient();
                var headerRes = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                var realUrl = headerRes.RequestMessage.RequestUri.OriginalString;

                var stream = await client.GetStreamAsync(realUrl);
                var totalLen = headerRes.Content.Headers.ContentLength;

                TiaoTiaoNLogger.LogDebug($"包大小：{totalLen}");

                var fullPath = Path.Combine(filePath, fileName);

                if (File.Exists(fullPath))
                {
                    var fi = new FileInfo(fullPath);
                    if (fi.Length == totalLen)
                    {
                        // 转解压
                        UnzipFile(fullPath);

                        FinishEnabled = true;

                        return;
                    }
                    else
                        File.Delete(fullPath);
                }

                using var fs = File.Create(fullPath);

                var buffer = new byte[1024];
                var readLength = 0;
                int length;
                while ((length = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    readLength += length;

                    DispatchInvoke(() => DownloadProgress = (float)((float)readLength / totalLen!) * 100);

                    // 写入到文件
                    fs.Write(buffer, 0, length);
                }

                // 转解压
                UnzipFile(fullPath);

                FinishEnabled = true;
            }
            catch (Exception ex)
            {
                Growl.Error($"下载异常：{ex.Message}");
                TiaoTiaoNLogger.LogError("下载异常", ex);
            }
        }

        #endregion

        #region 更新相关
        // 判断文件是否打开
        [DllImport("kernel32.dll")]
        static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        const int OF_READWRITE = 2;
        const int OF_SHARE_DENY_NONE = 0x40;
        readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        void UnzipFile(string filePath)
        {
            var di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var path = di.Parent.FullName;

            try
            {
                using var archive = ZipFile.OpenRead(filePath);
                var total = archive.Entries.Count;
                for (var i = 0; i < total; i++)
                {
                    ZipArchiveEntry? entry = null;
                    try
                    {
                        entry = archive.Entries[i];
                        var dir = Path.GetDirectoryName(entry.FullName);

                        if (!string.IsNullOrWhiteSpace(dir))
                        {
                            dir = Path.Combine(path, dir);
                            if (!Directory.Exists(dir))
                                Directory.CreateDirectory(dir);
                        }

                        if (!string.IsNullOrWhiteSpace(entry.Name) && string.IsNullOrWhiteSpace(dir))
                            entry.ExtractToFile(Path.Combine(path, entry.Name), true);
                        else if (!string.IsNullOrWhiteSpace(entry.Name) && !string.IsNullOrWhiteSpace(dir))
                            entry.ExtractToFile(Path.Combine(path, dir, entry.Name), true);

                        DispatchInvoke(() => ZipProgress = (float)((float)(i + 1) / total!) * 100);
                    }
                    catch (Exception ex)
                    {
                        TiaoTiaoNLogger.LogError($"文件解压异常【{entry?.FullName}】", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                TiaoTiaoNLogger.LogError($"更新异常【{ex.Message}】", ex);
            }
        }

        #endregion

        void DispatchInvoke(Action act)
        {
            Application.Current.Dispatcher.Invoke(act);
        }
    }
}
