using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchParser.Tests
{
    [TestClass()]
    public class LuceneParserTests
    {
        [TestMethod()]
        public void ParseSearch_Simple_Test()
        {
            string search = "red green blue";
            string expected = "red OR green OR blue";

            var actual = LuceneParser.ParseSearch(search);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void ParseSearch_ExtraOR_Test()
        {
            string search = "red blue OR green";
            string expected = "red OR blue OR green";

            var actual = LuceneParser.ParseSearch(search);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ParseSearch_Exclusion_Test()
        {
            string search = "red -green blue";
            string expected = "red OR -green OR blue";

            var actual = LuceneParser.ParseSearch(search);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ParseSearch_Require_Test()
        {
            string search = "red green +blue";
            string expected = "red OR green +blue";

            var actual = LuceneParser.ParseSearch(search);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ParseSearch_Phrase_Test()
        {
            string search = "\"red blue\" green";
            string expected = "\"red blue\" OR green";

            var actual = LuceneParser.ParseSearch(search);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ParseSearch_TwoPhrases_Test()
        {
            string search = "\"red blue\" \"green blue\"";
            string expected = "\"red blue\" OR \"green blue\"";

            var actual = LuceneParser.ParseSearch(search);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ParseSearch_UnclosedQuote_Test()
        {
            string search = "\"red blue\" \"green blue";
            string expected = "\"red blue\" OR green OR blue";

            var actual = LuceneParser.ParseSearch(search);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ParseSearch_MisplacedQuote_Test()
        {
            string search = "\"re\"d b\"lue\" green";
            string expected = "\"red blue\" OR green";

            var actual = LuceneParser.ParseSearch(search);
            Assert.AreEqual(expected, actual);
        }
    }
}