using System.Collections.Generic;

namespace LineAnalyze.Domain.Models
{
    /// <summary>
    /// 単語を扱うオブジェクトです。
    /// </summary>
    public class Word
    {
        /// <summary>
        /// 単語名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 単語の属性
        /// </summary>
        public List<string> Elements { get; set; }

        public string RealName
        {
            get { return Elements[6]; }
        }

        public string Id => Name + "," + string.Join(",", Elements.ToArray());
    }
}