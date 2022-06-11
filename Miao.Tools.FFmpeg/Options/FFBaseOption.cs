using System;
using System.Collections.Generic;
using System.Reflection;

namespace Miao.Tools.FFmpeg.Options
{
    public abstract class FFBaseOption
    {
        /// <summary>
        /// 转为命令行参数
        /// </summary>
        /// <returns></returns>
        public virtual string ToCommandArgs()
        {
            var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var list = new List<string>();
            foreach (var property in properties)
            {
                var objValue = property.GetValue(this);
                if (objValue != null)
                {
                    list.Add(objValue.ToString());
                }
            }
            return string.Join(" ", list);
        }
    }
}
