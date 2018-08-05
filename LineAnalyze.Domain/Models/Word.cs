using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Id => Name + "," + string.Join(",",Elements.ToArray());
    }
}
