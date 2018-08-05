﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                var enumerable = totalWords.GroupBy(w => w.RealName).Select(x => new
                    {
                        RealName = x.Key,
                        Count = x.Count()
                    })
                    .Where(x => x.Count > 1)
                    .OrderByDescending(x => x.Count);
                System.Console.WriteLine("単語,出現回数");
                foreach (var data in enumerable)
                {
                    System.Console.WriteLine(data.RealName + "," + data.Count);
                }
            }
            else
            {
                System.Console.WriteLine("引数に分析するファイルを指定してください。");
            }
        }
    }
}