namespace Miao.Tools.FFmpeg.Options
{
    public class VideoSize
    {
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
