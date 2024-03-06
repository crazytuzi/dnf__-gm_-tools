using HandyControl.Controls;
using Prism.Commands;
using Prism.Mvvm;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

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

        #endregion

        #region 

        ICommand? _finishCommand;

        public ICommand FinishCommand => _finishCommand ??= new DelegateCommand(DoFinishCommand);

        void DoFinishCommand()
        {
            var di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var exePath = di.Parent.FullName;
            var path = Path.Combine(exePath, " AY.DNF.GMTool.exe");
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

                var fullPath = Path.Combine(filePath, fileName);

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
            }
            catch (Exception ex)
            {
                Growl.Error(ex.Message);
            }
        }

        #endregion

        #region 更新相关

        void UnzipFile(string filePath)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;

            try
            {

                using var archive = ZipFile.OpenRead(filePath);
                var total = archive.Entries.Count;
                for (var i = 0; i < total; i++)
                {
                    try
                    {
                        var entry = archive.Entries[i];
                        var dir = Path.GetDirectoryName(entry.FullName);
                        if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        if (!string.IsNullOrWhiteSpace(entry.Name) && string.IsNullOrWhiteSpace(dir))
                            entry.ExtractToFile(entry.Name, true);
                        else if (!string.IsNullOrWhiteSpace(entry.Name) && !string.IsNullOrWhiteSpace(dir))
                            entry.ExtractToFile(entry.FullName, true);

                        DispatchInvoke(() => ZipProgress = (float)((float)(i + 1) / total!) * 100);
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Growl.Error("更新异常：" + ex.Message);
            }
        }

        void UpdateFile()
        {

        }

        #endregion

        void DispatchInvoke(Action act)
        {
            Application.Current.Dispatcher.Invoke(act);
        }
    }
}
