using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using LineAnalyze.Domain.Models;
using NMeCab;

namespace LineAnalyze.Domain.Services
{
    /// <summary>
    /// 分析サービス
    /// </summary>
    public class AnalyzeService
    {
        public IDictionary<string, User> Users = new Dictionary<string, User>();

        /// <summary>
        /// 分析を実施します。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IEnumerable<Talk> ParseTalk(string text)
        {
            DateTime startDate = DateTime.MinValue;
            DateTime nowDate = DateTime.MinValue;
            var lines = text.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                string dateRegexp = @"^\d{4}\/\d{2}\/\d{2}";
                var match = Regex.Match(line, dateRegexp);
                if (match.Success)
                {
                    if (DateTime.TryParseExact(match.Value, "yyyy/MM/dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out var dateValue))
                    {
                        if (startDate == DateTime.MinValue)
                        {
                            startDate = dateValue;
                        }

                        nowDate = dateValue;
                        continue;
                    }
                }

                Talk talk = CreateTalk(startDate, nowDate, line);
                if (talk == null)
                {
                    continue;
                }

                yield return talk;
            }
        }

        public IEnumerable<Word> ParseText(MeCabTagger meCabTagger, string message)
        {
            var node = meCabTagger.ParseToNode(message);
            while (node != null)
            {
                if (node.CharType > 0)
                {
                    string word = node.Surface;
                    var data = new List<string>(node.Feature.Split(','));

                    yield return new Word
                    {
                        Surface = word,
                        Elements = data
                    };
                }

                node = node.Next;
            }
        }

        /// <summary>
        /// <seealso cref="Talk"/>を生成します。
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="nowDate"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private Talk CreateTalk(DateTime startDate, DateTime nowDate, string line)
        {
            var splits = line.Split(new[] {'\t'}, StringSplitOptions.RemoveEmptyEntries);
            if (splits.Length < 3)
            {
                return null;
            }

            var dateText = splits[0];
            var userName = splits[1];
            var message = splits[2];
            CreateUser(startDate, userName);
            DateTime talkTime = GetTalkTime(nowDate, dateText);
            TalkType talkType = TalkType.Message;

            if (message == "[写真]")
            {
                talkType = TalkType.Picture;
                message = string.Empty;
            }

            if (message == "[スタンプ]")
            {
                talkType = TalkType.Stamp;
                message = string.Empty;
            }

            var talk = new Talk
            {
                Message = message,
                Time = talkTime,
                User = Users[userName],
                TalkType = talkType
            };
            return talk;
        }

        private static DateTime GetTalkTime(DateTime nowDate, string dateText)
        {
            var dateSplits = dateText.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries);
            var talkTime = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, int.Parse(dateSplits[0]),
                int.Parse(dateSplits[1]), 0);
            return talkTime;
        }

        private void CreateUser(DateTime startDate, string userName)
        {
            if (!Users.ContainsKey(userName))
            {
                Users.Add(userName, new User
                {
                    Name = userName,
                    EntryTime = startDate
                });
            }
        }
    }
}