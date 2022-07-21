using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Miao.Core.Utils;
using Miao.Tools.FFmpeg.Options;

namespace Miao.Tools.FFmpeg
{
    public class FFmpegUtil
    {
        /// <summary>
        /// 是否使用原始命令行参数
        /// </summary>
        private readonly bool _useRawCommandArgs;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="ffmpegPath"></param>
        /// <param name="options"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public FFmpegUtil(string ffmpegPath, FFmpegOptions options)
        {
            if (!File.Exists(ffmpegPath))
            {
                throw new FileNotFoundException("the ffmpeg file is not exist", ffmpegPath);
            }
            this.FFmpegPath = ffmpegPath;
            this.Options = options;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="ffmpegPath"></param>
        /// <param name="rawCommandArgs"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public FFmpegUtil(string ffmpegPath, string rawCommandArgs)
        {
            if (!File.Exists(ffmpegPath))
            {
                throw new FileNotFoundException("the ffmpeg file is not exist", ffmpegPath);
            }
            if (string.IsNullOrEmpty(rawCommandArgs))
            {
                throw new ArgumentException("命令行参数为空", nameof(rawCommandArgs));
            }
            this.FFmpegPath = ffmpegPath;
            this.RawCommandArgs = rawCommandArgs;
            this._useRawCommandArgs = true;
        }

        /// <summary>
        /// FFmpeg.exe程序路径
        /// </summary>
        public string FFmpegPath { get; private set; }

        /// <summary>
        /// FFmpeg设置
        /// </summary>
        public FFmpegOptions Options { get; private init; }

        /// <summary>
        /// 原始命令行参数
        /// </summary>
        public string RawCommandArgs { get; private init; }

        /// <summary>
        /// 输出数据接收处理程序
        /// </summary>
        private readonly List<DataReceivedEventHandler> _outDataReceivedHandlers = new List<DataReceivedEventHandler>();

        /// <summary>
        /// 错误数据接收处理程序
        /// </summary>
        private readonly List<DataReceivedEventHandler> _errorDataReceivedHandlers = new List<DataReceivedEventHandler>();

        /// <summary>
        /// 添加输出数据接收处理程序
        /// </summary>
        /// <param name="handler"></param>
        public void AddOutDataReceivedHandler(DataReceivedEventHandler handler)
        {
            _outDataReceivedHandlers.Add(handler);
        }

        /// <summary>
        /// 添加错误数据接收处理程序
        /// </summary>
        /// <param name="handler"></param>
        public void AddErrorDataReceivedHandler(DataReceivedEventHandler handler)
        {
            _errorDataReceivedHandlers.Add(handler);
        }

        /// <summary>
        /// 进行转换
        /// </summary>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        public async Task<TransferResult> ExecuteAsync(CancellationTokenSource cancellationTokenSource)
        {
            return await ExecuteAsync(cancellationTokenSource.Token);
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TransferResult> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            // ffmpeg.exe -i input.mp4 output.avi
            string commandArguments;
            if (_useRawCommandArgs)
            {
                commandArguments = RawCommandArgs;
            }
            else
            {
                commandArguments = Options.ToCommandArgs();
            }
            Console.WriteLine("ffmpeg arguments:" + commandArguments);
            using var processUtil = new ProcessUtil(FFmpegPath);
            processUtil.SetArguments(commandArguments);
            foreach (var item in _outDataReceivedHandlers)
            {
                processUtil.AddOutDataReceivedHandler(item);
            }
            foreach (var item in _errorDataReceivedHandlers)
            {
                processUtil.AddErrorDataReceivedHandler(item);
            }
            bool processResult = await processUtil.ExecuteAsync(cancellationToken);
            string error = processResult ? string.Empty : processUtil.LastError;
            return new TransferResult(processResult, error);
        }
    }
}
