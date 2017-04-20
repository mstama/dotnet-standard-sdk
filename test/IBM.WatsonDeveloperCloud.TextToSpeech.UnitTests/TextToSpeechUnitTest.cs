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

using IBM.WatsonDeveloperCloud.Http;
using IBM.WatsonDeveloperCloud.Http.Exceptions;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1;
using IBM.WatsonDeveloperCloud.TextToSpeech.v1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.WatsonDeveloperCloud.TextToSpeech.UnitTests
{
    [TestClass]
    public class TextToSpeechUnitTest
    {
        [TestMethod]
        public void GetVoices_Success()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(request);

            VoiceSet response =
                new VoiceSet()
                {
                    VoiceList = new List<Voice>()
                    {
                        new Voice()
                        {
                            Customizable = true,
                            Description = "description",
                            Gender = "gender",
                            Language = "language",
                            Name = "name",
                            SupportedFeatures = new SupportedFeatures()
                            {
                                CustomPronunciation = false,
                                VoiceTransformation = false
                            },
                            Url = "url"
                        }
                    }
                };

            request.As<VoiceSet>()
                   .Returns(Task.FromResult(response));

            TextToSpeechService service =
                new TextToSpeechService(client);

            var voiceSet =
                service.GetVoices();

            Assert.IsNotNull(voiceSet);
            Assert.IsNotNull(voiceSet.VoiceList);
            Assert.IsTrue(voiceSet.VoiceList.Count() > 0);
        }

        [TestMethod, ExpectedException(typeof(ServiceResponseException))]
        public void GetVoices_Catch_Exception()
        {
            IClient client = Substitute.For<IClient>();

            client.WithAuthentication(Arg.Any<string>(), Arg.Any<string>())
                  .Returns(client);

            IRequest request = Substitute.For<IRequest>();
            client.GetAsync(Arg.Any<string>())
                  .Returns(x =>
                  {
                      throw new AggregateException(new ServiceResponseException(Substitute.For<IResponse>(),
                                                                               Substitute.For<HttpResponseMessage>(HttpStatusCode.BadRequest),
                                                                               string.Empty));
                  });

            TextToSpeechService service =
                new TextToSpeechService(client);

            var voiceSet =
                service.GetVoices();
        }
    }
}