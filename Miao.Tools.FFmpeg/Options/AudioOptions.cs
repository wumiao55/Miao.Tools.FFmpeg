using System.Collections.Generic;
using System.Reflection;
using Miao.Tools.FFmpeg.Enums;

namespace Miao.Tools.FFmpeg.Options
{
    /// <summary>
    /// 音频选项
    /// </summary>
    public class AudioOptions : FFBaseOption
    {
        #region command arguments

        /// <summary>
        /// 音频编码器参数
        /// </summary>
        public string AudioCodecArg { get; private set; }

        /// <summary>
        /// 音频采样率参数
        /// </summary>
        public string SamplingFrequencyArg { get; private set; }

        /// <summary>
        /// 声道参数
        /// </summary>
        public string ChannelsArg { get; private set; }

        /// <summary>
        /// 音频码率/比特率参数
        /// </summary>
        public string BitRateArg { get; private set; }

        #endregion

        #region set audio options

        /// <summary>
        /// 设置音频编码器
        /// </summary>
        /// <param name="value">音频编码器类型</param>
        /// <returns></returns>
        public AudioOptions SetAudioCodecType(AudioCodecType value)
        {
            AudioCodecArg = $"-acodec {value}";
            return this;
        }

        /// <summary>
        /// 设置音频采样率
        /// </summary>
        /// <param name="value">采样率, 如: 44100</param>
        /// <returns></returns>
        public AudioOptions SetSamplingFrequency(int value)
        {
            SamplingFrequencyArg = $"-ar {value}";
            return this;
        }

        /// <summary>
        /// 设置声道
        /// </summary>
        /// <param name="value">声道类型: 单声道,双声道</param>
        /// <returns></returns>
        public AudioOptions SetChannels(AudioChannelsType value)
        {
            ChannelsArg = $"-ac {(int)value}";
            return this;
        }

        /// <summary>
        /// 设置音频码率/比特率
        /// </summary>
        /// <param name="value">码率/比特率, 如: 64k</param>
        /// <returns></returns>
        public AudioOptions SetBitRate(string value)
        {
            BitRateArg = $"-ab {value}";
            return this;
        }

        #endregion
    }
}
