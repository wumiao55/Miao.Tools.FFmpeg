namespace Miao.Tools.FFmpeg
{
    public class TransferResult
    {
        public TransferResult(bool result, string error)
        {
            Result = result;
            Error = error;
        }

        /// <summary>
        /// 结果
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get; set; }
    }
}
