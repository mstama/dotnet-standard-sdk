﻿/**
* Copyright 2017 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using IBM.Watson.NaturalLanguageUnderstanding.v1.Model;
using IBM.Cloud.SDK.Core.Util;
using Newtonsoft.Json;

namespace IBM.Watson.NaturalLanguageUnderstanding.v1.IntegrationTests
{
    [TestClass]
    public class NaturalLanguageUnderstandingIntegrationTests
    {
        private static string apikey;
        private static string endpoint;
        private NaturalLanguageUnderstandingService service;
        private static string credentials = string.Empty;
        private string versionDate = "2017-02-27";
        private string nluText = "Analyze various features of text content at scale. Provide text, raw HTML, or a public URL, and IBM Watson Natural Language Understanding will give you results for the features you request. The service cleans HTML content before analysis by default, so the results can ignore most advertisements and other unwanted content.";

        [TestInitialize]
        public void Setup()
        {
            service = new NaturalLanguageUnderstandingService();
            service.VersionDate = versionDate;
        }

        [TestMethod]
        public void Analyze_Success()
        {
            var text = nluText;
            var features = new Features()
            {
                Keywords = new KeywordsOptions()
                {
                    Limit = 8,
                    Sentiment = true,
                    Emotion = true
                },
                Categories = new CategoriesOptions()
                {
                    Limit = 10
                }
            };

            service.WithHeader("X-Watson-Test", "1");
            var result = service.Analyze(
                features: features,
                text: text,
                clean: true,
                fallbackToRaw: true,
                returnAnalyzedText: true,
                language: "en"
                );

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result.Categories.Count > 0);
            Assert.IsTrue(result.Result.Keywords.Count > 0);
        }

        [TestMethod]
        public void ListModels_Success()
        {
            service.WithHeader("X-Watson-Test", "1");
            var result = service.ListModels();

            Assert.IsNotNull(result.Result);
        }
    }
}
