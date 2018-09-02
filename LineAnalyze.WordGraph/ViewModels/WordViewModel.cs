using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineAnalyze.WordGraph.ViewModels
{
    /// <summary>
    /// 単語ViewModel
    /// </summary>
    public class WordViewModel
    {
        /// <summary>
        /// 基本形
        /// </summary>
        public string Base { get; set; }
        /// <summary>
        /// 頻出回数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 表示文字列
        /// </summary>
        public string Text
        {
            get { return $"{Base}\t{Count}"; }
        }
    }
}
