using System;
using System.Collections.Generic;

namespace LineAnalyze.Domain.Models
{
    public enum TalkType
    {
        None = -1,
        /// <summary>
        /// メッセージ
        /// </summary>
        Message,
        /// <summary>
        /// スタンプ
        /// </summary>
        Stamp,
        /// <summary>
        /// 写真
        /// </summary>
        Picture
    }
    public class Talk
    {
        /// <summary>
        /// 発言日時
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// トーク種類
        /// </summary>
        public TalkType TalkType { get; set; } = TalkType.Message;

        /// <summary>
        /// 発言内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ユーザー
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 単語情報
        /// </summary>
        public IEnumerable<Word> Words { get; set; }
    }
}
