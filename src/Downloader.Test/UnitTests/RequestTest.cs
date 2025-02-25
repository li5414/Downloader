﻿using Downloader.DummyHttpServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Test.UnitTests
{
    [TestClass]
    public class RequestTest
    {
        private const string EnglishText = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const string PersianText = "۰۱۲۳۴۵۶۷۸۹ابپتثجچحخدذرزژسشصضطظعغفقکگلمنوهیًٌٍَُِّْؤئيإأآة»«:؛كٰ‌ٔء؟";
        private static readonly Encoding Latin1Encoding = Encoding.GetEncoding("iso-8859-1");

        [TestMethod]
        public void GetFileNameWhenNoUrlTest()
        {
            // arrange
            var url = "  ";

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual("", actualFilename);
        }

        [TestMethod]
        public void GetFileNameWhenBadUrlTest()
        {
            // arrange
            var url = "http://www.a.com/a/b/c/d/e/";

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual("", actualFilename);
        }

        [TestMethod]
        public void GetFileNameWhenBadUrlWithFilenameTest()
        {
            // arrange
            var filename = "test";
            var url = "http://www.a.com/a/b/c/" + filename;

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public void GetFileNameWithJustFilenameTest()
        {
            // arrange
            var filename = "test.xml";
            var url = filename;

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public void GetFileNameWithJustFilenameWithoutExtensionTest()
        {
            // arrange
            var filename = "test";
            var url = filename;

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public void GetFileNameWithShortUrlTest()
        {
            // arrange
            var filename = "test.xml";
            var url = "/" + filename;

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public void GetFileNameWithShortUrlAndQueryParamTest()
        {
            // arrange
            var filename = "test.xml";
            var url = $"/{filename}?q=1";

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public void GetFileNameWithShortUrlAndQueryParamsTest()
        {
            // arrange
            var filename = "test.xml";
            var url = $"/{filename}?q=1&x=100.0&y=testName";

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public void GetFileNameWithJustFilenameAndQueryParamsTest()
        {
            // arrange
            var filename = "test.xml";
            var url = $"{filename}?q=1&x=100.0&y=testName";

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public void GetFileNameWithUrlAndQueryParamsTest()
        {
            // arrange
            var filename = "test.xml";
            var url = $"http://www.a.com/{filename}?q=1&x=1&filename=test";

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public void GetFileNameWithUrlAndQueryParamsComplexTest()
        {
            // arrange
            var filename = "Thor.Love.and.Thunder.2022.720p.WEBRip.800MB.x264-GalaxyRG[TGx].zip";
            var url = $"https://rs17.seedr.cc/get_zip_ngen_free/149605004/{filename}?st=XGSqYEtPiKmJcU-2PNNxjg&e=1663157407";

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public void GetFileNameWithUrlAndQueryParamsAndFragmentIdentifierTest()
        {
            // arrange
            var filename = "test.xml";
            var url = $"http://www.a.com/{filename}?q=1&x=3#aidjsf";

            // act
            var actualFilename = new Request(url).GetFileNameFromUrl();

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWhenNoUrlFileNameTest()
        {
            // arrange
            var url = DummyFileHelper.GetFileWithContentDispositionUrl(DummyFileHelper.SampleFile1KbName, DummyFileHelper.FileSize1Kb);

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.AreEqual(DummyFileHelper.SampleFile1KbName, actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWhenNoUrlTest()
        {
            // arrange
            var url = "  ";

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWhenBadUrlTest()
        {
            // arrange
            var url = "http://www.a.com/a/b/c/d/e/";

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWhenBadUrlWithFilenameTest()
        {
            // arrange
            var filename = "test";
            var url = "http://www.a.com/a/b/c/" + filename;

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWithJustFilenameTest()
        {
            // arrange
            var filename = "test.xml";
            var url = filename;

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWithJustFilenameWithoutExtensionTest()
        {
            // arrange
            var filename = "test";
            var url = filename;

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWithShortUrlTest()
        {
            // arrange
            var filename = "test.xml";
            var url = "/" + filename;

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWithShortUrlAndQueryParamTest()
        {
            // arrange
            var filename = "test.xml";
            var url = $"/{filename}?q=1";

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWithShortUrlAndQueryParamsTest()
        {
            // arrange
            var filename = "test.xml";
            var url = $"/{filename}?q=1&x=100.0&y=testName";

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWithJustFilenameAndQueryParamsTest()
        {
            // arrange
            var filename = "test.xml";
            var url = $"{filename}?q=1&x=100.0&y=testName";

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWithUrlAndQueryParamsTest()
        {
            // arrange
            var filename = "test.xml";
            var url = $"http://www.a.com/{filename}?q=1&x=1&filename=test";

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWithUrlAndQueryParamsAndFragmentIdentifierTest()
        {
            // arrange
            var filename = "test.xml";
            var url = $"http://www.a.com/{filename}?q=1&x=3#aidjsf";

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetUrlDispositionWithLongUrlTest()
        {
            // arrange
            var filename = "excel_sample.xls";
            var url = $"https://raw.githubusercontent.com/bezzad/Downloader/master/src/Downloader.Test/Assets/{filename}?test=1";

            // act
            var actualFilename = await new Request(url).GetUrlDispositionFilenameAsync().ConfigureAwait(false);

            // assert
            Assert.IsNull(actualFilename);
        }

        [TestMethod]
        public async Task GetFileNameOnRedirectUrlTest()
        {
            // arrange
            var filename = "test.zip";
            var url = DummyFileHelper.GetFileWithNameOnRedirectUrl(filename, DummyFileHelper.FileSize1Kb);

            // act
            var actualFilename = await new Request(url).GetFileName().ConfigureAwait(false);

            // assert
            Assert.AreEqual(filename, actualFilename);
        }

        [TestMethod]
        public void GetRedirectUrlByLocationTest()
        {
            // arrange
            var filename = "test.zip";
            var url = DummyFileHelper.GetFileWithNameOnRedirectUrl(filename, DummyFileHelper.FileSize1Kb);
            var redirectUrl = DummyFileHelper.GetFileWithNameUrl(filename, DummyFileHelper.FileSize1Kb);
            var request = new Request(url);

            // act
            var resp = WebRequest.Create(url).GetResponse();
            resp.Headers.Add("Location", redirectUrl);
            var actualRedirectUrl = request.GetRedirectUrl(resp);

            // assert
            Assert.AreNotEqual(url, redirectUrl);
            Assert.AreNotEqual(request.Address, redirectUrl);
            Assert.AreEqual(redirectUrl, actualRedirectUrl.AbsoluteUri);
        }

        [TestMethod]
        public void GetRedirectUrlWithoutLocationTest()
        {
            // arrange
            var filename = "test.zip";
            var url = DummyFileHelper.GetFileWithNameOnRedirectUrl(filename, DummyFileHelper.FileSize1Kb);
            var redirectUrl = DummyFileHelper.GetFileWithNameUrl(filename, DummyFileHelper.FileSize1Kb);
            var request = new Request(url);

            // act
            var resp = WebRequest.Create(url).GetResponse();
            var actualRedirectUrl = request.GetRedirectUrl(resp);

            // assert
            Assert.AreNotEqual(url, redirectUrl);
            Assert.AreNotEqual(request.Address, redirectUrl);
            Assert.AreEqual(redirectUrl, actualRedirectUrl.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetFileSizeTest()
        {
            // arrange
            var url = DummyFileHelper.GetFileUrl(DummyFileHelper.FileSize16Kb);
            var expectedSize = DummyFileHelper.FileSize16Kb;

            // act
            var actualSize = await new Request(url).GetFileSize().ConfigureAwait(false);

            // assert
            Assert.AreEqual(expectedSize, actualSize);
        }

        [TestMethod]
        public async Task IsSupportDownloadInRangeTest()
        {
            // arrange
            var url = DummyFileHelper.GetFileUrl(DummyFileHelper.FileSize16Kb);

            // act
            var actualCan = await new Request(url).IsSupportDownloadInRange().ConfigureAwait(false);

            // assert
            Assert.IsTrue(actualCan);
        }

        [TestMethod]
        public void GetTotalSizeFromContentLengthTest()
        {
            // arrange
            var length = 23432L;
            var headers = new Dictionary<string, string>() { { "Content-Length", length.ToString() } };
            var request = new Request("www.example.com");

            // act
            var actualLength = request.GetTotalSizeFromContentLength(headers);

            // assert
            Assert.AreEqual(length, actualLength);
        }

        [TestMethod]
        public void GetTotalSizeFromContentLengthWhenNoHeaderTest()
        {
            // arrange
            var length = -1;
            var headers = new Dictionary<string, string>();
            var request = new Request("www.example.com");

            // act
            var actualLength = request.GetTotalSizeFromContentLength(headers);

            // assert
            Assert.AreEqual(length, actualLength);
        }

        [TestMethod]
        public void GetTotalSizeFromContentRangeTest()
        {
            TestGetTotalSizeFromContentRange(23432, "bytes 0-0/23432");
        }

        [TestMethod]
        public void GetTotalSizeFromContentRangeWhenUnknownSizeTest()
        {
            TestGetTotalSizeFromContentRange(-1, "bytes 0-1000/*");
        }

        [TestMethod]
        public void GetTotalSizeFromContentRangeWhenUnknownRangeTest()
        {
            TestGetTotalSizeFromContentRange(23529, "bytes */23529");
        }

        [TestMethod]
        public void GetTotalSizeFromContentRangeWhenIncorrectTest()
        {
            TestGetTotalSizeFromContentRange(23589, "bytes -0/23589");
        }

        [TestMethod]
        public void GetTotalSizeFromContentRangeWhenNoHeaderTest()
        {
            TestGetTotalSizeFromContentRange(-1, null);
        }

        private void TestGetTotalSizeFromContentRange(long expectedLength, string contentRange)
        {
            // arrange
            var request = new Request("www.example.com");
            var headers = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(contentRange) == false)
                headers["Content-Range"] = contentRange;

            // act
            var actualLength = request.GetTotalSizeFromContentRange(headers);

            // assert
            Assert.AreEqual(expectedLength, actualLength);
        }

        [TestMethod]
        public void ToUnicodeFromEnglishToEnglishTest()
        {
            // arrange
            byte[] inputRawBytes = Encoding.UTF8.GetBytes(EnglishText);
            string inputEncodedText = Latin1Encoding.GetString(inputRawBytes);
            Request requestInstance = new Request("");

            // act 
            string decodedEnglishText = requestInstance.ToUnicode(inputEncodedText);

            // assert
            Assert.AreEqual(EnglishText, decodedEnglishText);
        }

        [TestMethod]
        public void ToUnicodeFromPersianToPersianTest()
        {
            // arrange
            byte[] inputRawBytes = Encoding.UTF8.GetBytes(PersianText);
            string inputEncodedText = Latin1Encoding.GetString(inputRawBytes);
            Request requestInstance = new Request("");

            // act 
            string decodedEnglishText = requestInstance.ToUnicode(inputEncodedText);

            // assert
            Assert.AreEqual(PersianText, decodedEnglishText);
        }

        [TestMethod]
        public void ToUnicodeFromAllToAllWithExtensionTest()
        {
            // arrange
            string inputRawText = EnglishText + PersianText + ".ext";
            byte[] inputRawBytes = Encoding.UTF8.GetBytes(inputRawText);
            string inputEncodedText = Latin1Encoding.GetString(inputRawBytes);
            Request requestInstance = new Request("");

            // act 
            string decodedEnglishText = requestInstance.ToUnicode(inputEncodedText);

            // assert
            Assert.AreEqual(inputRawText, decodedEnglishText);
        }

        [TestMethod]
        public void GetRequestWithCredentialsTest()
        {
            // arrange
            var requestConfig = new RequestConfiguration() {
                Credentials = new NetworkCredential("username", "password")
            };
            var request = new Request("http://test.com", requestConfig);

            // act
            var httpRequest = request.GetRequest();

            // assert
            Assert.IsNotNull(httpRequest.Credentials);
        }

        [TestMethod]
        public void GetRequestWithNullCredentialsTest()
        {
            // arrange
            var requestConfig = new RequestConfiguration();
            var request = new Request("http://test.com", requestConfig);

            // act
            var httpRequest = request.GetRequest();

            // assert
            Assert.IsNull(httpRequest.Credentials);
        }
    }
}