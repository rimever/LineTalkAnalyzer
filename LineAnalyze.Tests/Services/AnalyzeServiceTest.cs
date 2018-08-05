using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LineAnalyze.Domain.Models;
using LineAnalyze.Domain.Services;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace LineAnalyze.Tests.Services
{
    [TestFixture]
    public class AnalyzeServiceTest
    {
        /// <summary>
        /// <seealso cref="AnalyzeService.ParseTalk"/>をテストします。
        /// </summary>
        [Test]
        public void Execute()
        {
            string text =
                @"2013/01/01(火)
00:07	aaa	明けましておめでとうございます。
00:08	aaa	[写真]

2013/01/02(水)
00:19	bbb	[スタンプ]";

            AnalyzeService service = new AnalyzeService();
            var list = service.ParseTalk(text).ToList();
            var first = list.Skip(0).FirstOrDefault();
            var second = list.Skip(1).FirstOrDefault();
            var third = list.Skip(2).FirstOrDefault();

            Assert.AreEqual(first.Message,"明けましておめでとうございます。");
            Assert.AreEqual(first.User.Name, "aaa");
            Assert.AreEqual(first.TalkType, TalkType.Message);
            Assert.AreEqual(first.Time, new DateTime(2013, 1, 1, 0, 7, 0));

            Assert.AreEqual(second.Message, string.Empty);
            Assert.AreEqual(second.User.Name, "aaa");
            Assert.AreEqual(second.TalkType, TalkType.Picture);
            Assert.AreEqual(second.Time, new DateTime(2013, 1, 1, 0, 8, 0));

            Assert.AreEqual(third.Message, string.Empty);
            Assert.AreEqual(third.User.Name, "bbb");
            Assert.AreEqual(third.TalkType, TalkType.Stamp);
            Assert.AreEqual(third.Time, new DateTime(2013, 1, 2, 0, 19, 0));
        }
    }
}
