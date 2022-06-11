using Miao.Tools.FFmpeg.Enums;

namespace Miao.Tools.FFmpeg.Options
{
    /// <summary>
    /// 视频选项
    /// </summary>
    public class VideoOptions : FFBaseOption
    {
        #region command arguments

        /// <summary>
        /// 视频编码器参数
        /// </summary>
        public string VideoCodecArg { get; private set; }

        /// <summary>
        /// 尺寸参数
        /// </summary>
        public string SizeArg { get; private set; }

        /// <summary>
        /// 视频帧率参数
        /// </summary>
        public string RateArg { get; private set; }

        /// <summary>
        /// 视频码率/比特率参数
        /// </summary>
        public string BitRateArg { get; private set; }

        #endregion

        #region set video options

        /// <summary>
        /// 设置视频编码器类型
        /// </summary>
        /// <param name="value">视频编码器类型</param>
        /// <returns></returns>
        public VideoOptions SetVideoCodecType(VideoCodecType value)
        {
            VideoCodecArg = $"-vcodec {value}";
            return this;
        }

        /// <summary>
        /// 设置尺寸
        /// </summary>
        /// <param name="value">视频尺寸</param>
        /// <returns></returns>
        public VideoOptions SetSize(VideoSize value)
        {
            SizeArg = $"-s {value.WidthPixel}x{value.HeightPixel}";
            return this;
        }

        /// <summary>
        /// 设置视频帧率
        /// </summary>
        /// <param name="value">视频帧率, 如: 25</param>
        /// <returns></returns>
        public VideoOptions SetRate(int value)
        {
            RateArg = $"-r {value}";
            return this;
        }

        /// <summary>
        /// 设置视频码率/比特率
        /// </summary>
        /// <param name="value">视频码率/比特率, 如: 64k</param>
        /// <returns></returns>
        public VideoOptions SetBitRate(string value)
        {
            BitRateArg = $"-vb {value}";
            return this;
        }

        #endregion
    }
}
