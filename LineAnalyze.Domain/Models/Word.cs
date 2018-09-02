using System.Collections.Generic;

namespace LineAnalyze.Domain.Models
{
    /// <summary>
    /// 単語を扱うオブジェクトです。
    /// </summary>
    public class Word
    {
        /// <summary>
        /// 表層形
        /// </summary>
        public string Surface { get; set; }

        /// <summary>
        /// 単語の属性
        /// </summary>
        public List<string> Elements { get; set; }

        /// <summary>
        /// 品詞
        /// </summary>
        public string Pos
        {
            get { return Elements[0]; }
        }

        /// <summary>
        /// 品詞細分類
        /// </summary>
        public string Pos1
        {
            get { return Elements[1]; }
        }

        /// <summary>
        /// 単語の基本形
        /// </summary>
        public string Base
        {
            get { return Elements[6]; }
        }

        public string Id => Surface + "," + string.Join(",", Elements.ToArray());
    }
}