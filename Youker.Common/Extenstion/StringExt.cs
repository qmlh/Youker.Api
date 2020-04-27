using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Common.Extenstion
{
    public static class StringExt
    {
        /// <summary>
        /// 根据DB不同，拼接生成对应库的存储过程
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AssignDb(this string str, string dbName)
        {
            return string.Format("{1}.dbo.{0}", str, dbName);
        }
    }
}
