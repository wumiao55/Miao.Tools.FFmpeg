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

        /// <summary>
        /// 视频位置参数
        /// </summary>
        public string SeekPositionArg { get; private set; }

        /// <summary>
        /// 视频结束位置参数
        /// </summary>
        public string ToPositionArg { get; private set; }

        /// <summary>
        /// 过滤器参数
        /// </summary>
        public string FiltergraphArg { get; private set; }

        /// <summary>
        /// 复杂过滤器参数
        /// </summary>
        public string ComplexFilterArg { get; private set; }

        /// <summary>
        /// 无视频参数
        /// </summary>
        public string NoVideoArg { get; private set; }

        #endregion

        #region set video options

        /// <summary>
        /// 设置视频编码器类型
        /// </summary>
        /// <param name="value">视频编码器类型</param>
        /// <returns></returns>
        public VideoOptions SetVideoCodecType(VideoCodecType value)
        {
            /*
             -vcodec codec (output):
                Set the video codec. This is an alias for -codec:v
             */
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
            /*
             -s[:stream_specifier] size (input/output,per-stream):
                Set frame size.
                As an input option, this is a shortcut for the video_size private option, recognized by some demuxers for which the frame size is either not stored in the file or is configurable – e.g. raw video or video grabbers.
                As an output option, this inserts the scale video filter to the end of the corresponding filtergraph. Please use the scale filter directly to insert it at the beginning or some other place.
                The format is ‘wxh’ (default - same as source).
             */
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
            /*
             -r[:stream_specifier] fps (input/output,per-stream):
                Set frame rate (Hz value, fraction or abbreviation).
                As an input option, ignore any timestamps stored in the file and instead generate timestamps assuming constant frame rate fps. This is not the same as the -framerate option used for some input formats like image2 or v4l2 (it used to be the same in older versions of FFmpeg). If in doubt use -framerate instead of the input option -r.
                As an output option, duplicate or drop input frames to achieve constant output frame rate fps.
             */
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
            /*
             -vb rate:
                Set bit rate of video
             */
            BitRateArg = $"-vb {value}";
            return this;
        }

        /// <summary>
        /// 设置视频位置
        /// </summary>
        /// <param name="value">位置值,单位:秒</param>
        /// <returns></returns>
        public VideoOptions SetSeekPosition(double value)
        {
            /*
             -ss position (input/output):
                When used as an input option (before -i), seeks in this input file to position. Note that in most formats it is not possible to seek exactly, so ffmpeg will seek to the closest seek point before position. When transcoding and -accurate_seek is enabled (the default), this extra segment between the seek point and position will be decoded and discarded. When doing stream copy or when -noaccurate_seek is used, it will be preserved.
                When used as an output option (before an output url), decodes but discards input until the timestamps reach position.
                position must be a time duration specification, see (ffmpeg-utils)the Time duration section in the ffmpeg-utils(1) manual.
             */
            SeekPositionArg = $"-ss {value}";
            return this;
        }

        /// <summary>
        /// 设置视频结束位置
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public VideoOptions SetToPosition(double value)
        {
            /*
             -ss position (input/output):
                When used as an input option (before -i), seeks in this input file to position. Note that in most formats it is not possible to seek exactly, so ffmpeg will seek to the closest seek point before position. When transcoding and -accurate_seek is enabled (the default), this extra segment between the seek point and position will be decoded and discarded. When doing stream copy or when -noaccurate_seek is used, it will be preserved.
                When used as an output option (before an output url), decodes but discards input until the timestamps reach position.
                position must be a time duration specification, see (ffmpeg-utils)the Time duration section in the ffmpeg-utils(1) manual.
             */
            ToPositionArg = $"-to {value}";
            return this;
        }

        /// <summary>
        /// 设置过滤器
        /// </summary>
        /// <param name="value">过滤器值</param>
        /// <returns></returns>
        public VideoOptions SetFiltergraph(string value)
        {
            /*
             -vf filtergraph (output):
                Create the filtergraph specified by filtergraph and use it to filter the stream.
                This is an alias for -filter:v, see the -filter option.
             */
            FiltergraphArg = $"-vf \"{value}\"";
            return this;
        }

        /// <summary>
        /// 设置复杂过滤器
        /// </summary>
        /// <param name="value">复杂过滤器值</param>
        /// <returns></returns>
        public VideoOptions SetComplexFilter(string value)
        {
            /*
             -filter_complex filtergraph (global):
                Define a complex filtergraph, i.e. one with arbitrary number of inputs and/or outputs. For simple graphs – those with one input and one output of the same type – see the -filter options. 
                filtergraph is a description of the filtergraph, as described in the “Filtergraph syntax” section of the ffmpeg-filters manual
             */
            ComplexFilterArg = $"-filter_complex \"{value}\"";
            return this;
        }

        /// <summary>
        /// 设置无视频
        /// </summary>
        /// <returns></returns>
        public VideoOptions SetNoVideo()
        {
            /*
            -vn (input/output):
                As an input option, blocks all video streams of a file from being filtered or being automatically selected or mapped for any output. See -discard option to disable streams individually.
                As an output option, disables video recording i.e. automatic selection or mapping of any video stream. For full manual control see the -map option. 
             */
            NoVideoArg = "-vn";
            return this;
        }

        #endregion
    }
}
