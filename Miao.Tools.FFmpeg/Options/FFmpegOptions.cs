using System;
using System.Collections.Generic;
using System.IO;

namespace Miao.Tools.FFmpeg.Options
{
    public class FFmpegOptions : FFBaseOption
    {
        /// <summary>
        /// 输入文件
        /// </summary>
        private readonly string _inputFile;

        /// <summary>
        /// 输出文件
        /// </summary>
        private readonly string _outputFile;

        /// <summary>
        /// 输入参数
        /// </summary>
        private string _inputArg;

        /// <summary>
        /// 输出参数
        /// </summary>
        private string _outputArg;

        /// <summary>
        /// 是否覆盖
        /// </summary>
        private bool _isCover;

        /// <summary>
        /// 视频选项
        /// </summary>
        private VideoOptions _videoOptions;

        /// <summary>
        /// 音频选项
        /// </summary>
        private AudioOptions _audioOptions;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="inputFile">输入文件</param>
        /// <param name="outputFile">输出文件</param>
        /// <exception cref="ArgumentException"></exception>
        public FFmpegOptions(string inputFile, string outputFile)
        {
            if (string.IsNullOrEmpty(inputFile))
            {
                throw new ArgumentException("输入项为空", nameof(inputFile));
            }
            if (string.IsNullOrEmpty(outputFile))
            {
                throw new ArgumentException("输出项为空", nameof(outputFile));
            }
            if (!File.Exists(inputFile))
            {
                throw new FileNotFoundException("输入文件不存在", Path.GetFullPath(inputFile));
            }
            _inputFile = inputFile;
            _outputFile = outputFile;
            _inputArg = $"-i \"{inputFile}\"";
            _outputArg = $"\"{outputFile}\"";
        }

        /// <summary>
        /// 是否覆盖
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public FFmpegOptions IsCover(bool value)
        {
            _isCover = value;
            return this;
        }

        /// <summary>
        /// 设置视频选项
        /// </summary>
        /// <param name="options"></param>
        public FFmpegOptions SetVideoOptions(VideoOptions options)
        {
            _videoOptions = options;
            return this;
        }

        /// <summary>
        /// 设置音频选项
        /// </summary>
        /// <param name="options"></param>
        public FFmpegOptions SetAudioOptions(AudioOptions options)
        {
            _audioOptions = options;
            return this;
        }

        /// <summary>
        /// 转为命令行参数
        /// </summary>
        /// <returns></returns>
        public override string ToCommandArgs()
        {
            string audioCommandArgs = _audioOptions?.ToCommandArgs();
            string videoCommandArgs = _videoOptions?.ToCommandArgs();
            var list = new List<string>();
            if (_isCover)
            {
                _inputArg = $"-y -i \"{_inputFile}\"";
            }
            list.Add(_inputArg);
            if (!string.IsNullOrEmpty(audioCommandArgs))
            {
                list.Add(audioCommandArgs);
            }
            if (!string.IsNullOrEmpty(videoCommandArgs))
            {
                list.Add(videoCommandArgs);
            }
            list.Add(_outputArg);
            return string.Join(" ", list);
        }

    }

}
