using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LineAnalyze.Domain.Models;
using LineAnalyze.Domain.Services;
using NMeCab;

namespace LineAnalyze.Console
{
    class Program
    {
        /// <summary>
        /// プログラムのメインエントリです。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string text = File.ReadAllText(args[0]);
                var service = new AnalyzeService();
                var talks = service.ParseTalk(text);
                var meCabTagger = MeCabTagger.Create();
                var totalWords = new List<Word>();
                foreach (var talk in talks)
                {
                    var words = service.ParseText(meCabTagger, talk.Message);
                    totalWords.AddRange(words);
                }

                var enumerable = totalWords.GroupBy(w => w.Id).Select(x => new
                {
                    Id = x.Key,
                    Count = x.Count()
                })
                    .Where(x => x.Count > 1)
                    .OrderByDescending(x => x.Count);
                System.Console.WriteLine("単語,出現回数,単語情報...");
                foreach (var data in enumerable)
                {
                    var split = data.Id.Split(new[] { "," }, StringSplitOptions.None);
                    System.Console.WriteLine(split[0] + "," + data.Count + "," + data.Id);
                }

            }
            else
            {
                System.Console.WriteLine("引数に分析するファイルを指定してください。");
            }
        }
    }
}
