using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarService;

namespace UnitTests
{
    [TestClass]
    public class TestLogic
    {
        #region LFP
        [TestMethod]
        public void TestCorrectLFP()
        {
            string LFP = "Ковальский Владислав Витальевич";
            var actual = LFP.IsCorrectLFP();
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void TestCorrectLFPNull()
        {
            string LFP = "";
            var actual = LFP.IsCorrectLFP();
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void TestCorrectLFPWhiteSpace()
        {
            string LFP = "   Ковальский   Владислав   Витальевич  ";
            var actual = LFP.IsCorrectLFP();
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void TestCorrectLFPNoThreeWord()
        {
            string LFP = "Ковальский   Владислав";
            var actual = LFP.IsCorrectLFP();
            Assert.AreEqual(false, actual);
        }
        #endregion

        #region numberBid
        [TestMethod]
        public void TestCorrectNumberBid()
        {
            string numberBid = "HHHH4444";
            var actual = numberBid.IsNumberBid();
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void TestCorrectNumberBidNull()
        {
            string numberBid = "";
            var actual = numberBid.IsNumberBid();
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void TestCorrectNumberBidNoEightLenght()
        {
            string numberBid = "HHHH444";
            var actual = numberBid.IsNumberBid();
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void TestCorrectNumberBidNoCorrect()
        {
            string numberBid = "HH/H4444";
            var actual = numberBid.IsNumberBid();
            Assert.AreEqual(false, actual);
        }
        #endregion

        [TestMethod]
        public void TestCorrectGenerateNumberBid()
        {
            string numberBid = Methods.GenerateNumberBid();
            var actual = numberBid.IsNumberBid();
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void TestCorrectGetTypeWorks()
        {
            List<string> typeWorks = Methods.GetTypeWorks();
            int actual = typeWorks.Count;
            Assert.AreEqual(8, actual);
        }

        #region FindBid
        [TestMethod]
        public void TestCorrectFindBid()
        {
            string numberBid = "FRBT9436";
            BidDao bid = Methods.FindBid(numberBid);
            string actual = bid.NumberBid;
            Assert.AreEqual(numberBid, actual);
        }

        [TestMethod]
        public void TestCorrectFindBidNotFound()
        {
            string numberBid = "FRBS9436";
            BidDao bid;
            try
            {
                bid = Methods.FindBid(numberBid);
            }
            catch (InvalidOperationException)
            {
                bid = new BidDao()
                {
                    NumberBid = "test"
                };
            }
            string actual = bid.NumberBid;
            Assert.AreEqual("test", actual);
        }
        #endregion

        #region FindDeleayedBid
        [TestMethod]
        public void TestCorrectFindDeleayedBid()
        {
            string numberBid = "JOIZ1422";
            BidDao bid = Methods.FindDeleayedBid(numberBid);
            string actual = bid.NumberBid;
            Assert.AreEqual(numberBid, actual);
        }

        [TestMethod]
        public void TestCorrectFindDeleayedBidNotFound()
        {
            string numberBid = "FRBS9436";
            BidDao bid;
            try
            {
                bid = Methods.FindDeleayedBid(numberBid);
            }
            catch (InvalidOperationException)
            {
                bid = new BidDao()
                {
                    NumberBid = "test"
                };
            }
            string actual = bid.NumberBid;
            Assert.AreEqual("test", actual);
        }
        #endregion

        #region Search
        [TestMethod]
        public void TestCorrectSearch()
        {
            DateTime date = DateTime.Parse("2022-10-20 17:12:22.047");
            string numberBid = "YUOC5235";
            string LFP = "Печкин Сергей Михайлович";
            List<BidDao> bids = Methods.Search(date, numberBid, LFP);
            string actual = bids[0].NumberBid;
            Assert.AreEqual(numberBid, actual);
        }

        [TestMethod]
        public void TestCorrectSearchNotFound()
        {
            DateTime date = DateTime.Parse("2022-10-30 17:12:22.047");
            string numberBid = "YUOC5235";
            string LFP = "Печкин Сергей Михайлович";
            List<BidDao> bids = Methods.Search(date, numberBid, LFP);
            Assert.AreEqual(0, bids.Count);
        }
        #endregion
    }
}
