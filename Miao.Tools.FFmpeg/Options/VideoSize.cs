namespace Miao.Tools.FFmpeg.Options
{
    public class VideoSize
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="widthPixel"></param>
        /// <param name="heightPixel"></param>
        public VideoSize(int widthPixel, int heightPixel)
        {
            WidthPixel = widthPixel;
            HeightPixel = heightPixel;
        }

        /// <summary>
        /// 宽度, 单位: 像素
        /// </summary>
        public int WidthPixel { get; set; }

        /// <summary>
        /// 高度, 单位: 像素
        /// </summary>
        public int HeightPixel { get; set; }
    }
}
