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

        /// <summary>
        /// 无音频参数
        /// </summary>
        public string NoAudioArg { get; private set; }

        #endregion

        #region set audio options

        /// <summary>
        /// 设置音频编码器
        /// </summary>
        /// <param name="value">音频编码器类型</param>
        /// <returns></returns>
        public AudioOptions SetAudioCodecType(AudioCodecType value)
        {
            /*
            -acodec codec(input / output):
                Set the audio codec. This is an alias for -codec:a.
            */
            AudioCodecArg = $"-acodec {value}";
            return this;
        }

        /// <summary>
        /// 设置音频采样率
        /// </summary>
        /// <param name="value">采样率</param>
        /// <returns></returns>
        public AudioOptions SetSamplingFrequency(AudioSamplingFrequency value)
        {
            /*
             -ar[:stream_specifier] freq (input/output,per-stream):
                Set the audio sampling frequency. 
                For output streams it is set by default to the frequency of the corresponding input stream. 
                For input streams this option only makes sense for audio grabbing devices and raw demuxers and is mapped to the corresponding demuxer options.
             */
            SamplingFrequencyArg = $"-ar {(int)value}";
            return this;
        }

        /// <summary>
        /// 设置声道
        /// </summary>
        /// <param name="value">声道类型: 单声道,双声道</param>
        /// <returns></returns>
        public AudioOptions SetChannels(AudioChannelsType value)
        {
            /*
             -ac[:stream_specifier] channels (input/output,per-stream):
                Set the number of audio channels. \
                For output streams it is set by default to the number of input audio channels. 
                For input streams this option only makes sense for audio grabbing devices and raw demuxers and is mapped to the corresponding demuxer options.
             */
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
            /*
             -ab rate:
                Set bit rate of audio
             */
            BitRateArg = $"-ab {value}";
            return this;
        }

        /// <summary>
        /// 设置无音频
        /// </summary>
        /// <returns></returns>
        public AudioOptions SetNoAudio()
        {
            /*
             -an (input/output):
                As an input option, blocks all audio streams of a file from being filtered or being automatically selected or mapped for any output. See -discard option to disable streams individually.
                As an output option, disables audio recording i.e. automatic selection or mapping of any audio stream. For full manual control see the -map option.
             */
            NoAudioArg = "-an";
            return this;
        }

        #endregion
    }
}
