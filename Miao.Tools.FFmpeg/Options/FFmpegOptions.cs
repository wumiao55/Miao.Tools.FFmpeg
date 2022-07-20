using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Miao.Tools.FFmpeg.Options
{
    public class FFmpegOptions : FFBaseOption
    {
        /// <summary>
        /// 输入文件集合
        /// </summary>
        private readonly List<string> _inputFiles;

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
        /// <param name="inputFiles">多个输入文件</param>
        /// <param name="outputFile">输出文件</param>
        public FFmpegOptions(List<string> inputFiles, string outputFile)
        {
            foreach (var inputFile in inputFiles)
            {
                if (!File.Exists(inputFile))
                {
                    throw new FileNotFoundException("输入文件不存在", Path.GetFullPath(inputFile));
                }
            }
            if (string.IsNullOrEmpty(outputFile))
            {
                throw new ArgumentException("输出项为空", nameof(outputFile));
            }
            _inputFiles = inputFiles;
            _outputFile = outputFile;
            _inputArg = GenerateInputArg(_inputFiles.ToArray());
            _outputArg = $"\"{outputFile}\"";
        }

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
            if (!File.Exists(inputFile))
            {
                throw new FileNotFoundException("输入文件不存在", Path.GetFullPath(inputFile));
            }
            if (string.IsNullOrEmpty(outputFile))
            {
                throw new ArgumentException("输出项为空", nameof(outputFile));
            }
            _inputFiles = new List<string>() { inputFile };
            _outputFile = outputFile;
            _inputArg = GenerateInputArg(_inputFiles.ToArray());
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
                _inputArg = $"-y {_inputArg}";
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

        /// <summary>
        /// 生成输入参数
        /// </summary>
        /// <param name="inputFiles"></param>
        /// <returns></returns>
        private static string GenerateInputArg(params string[] inputFiles)
        {
            // -i input1.mp4 -i input2.png
            var inputItems = inputFiles.Select(input => $"-i \"{input}\"");
            return string.Join(" ", inputItems);
        }

    }

}
