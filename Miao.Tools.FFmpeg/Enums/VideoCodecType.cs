namespace Miao.Tools.FFmpeg.Enums
{
    public enum VideoCodecType
    {
        /// <summary>
        /// 复制原视频编码
        /// </summary>
        copy = -1,

        /// <summary>
        /// libx264
        /// </summary>
        libx264 = 0,

        /// <summary>
        /// h264
        /// </summary>
        h264 = 1,

        /// <summary>
        /// mpeg4
        /// </summary>
        mpeg4 = 2,
    }
}
