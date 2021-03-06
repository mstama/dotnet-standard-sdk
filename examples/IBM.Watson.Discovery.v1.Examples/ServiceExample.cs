﻿/**
* Copyright 2019 IBM Corp. All Rights Reserved.
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

using Environment = IBM.Watson.Discovery.v1.Model.Environment;
using System;
using System.Collections.Generic;
using System.IO;
using IBM.Watson.Discovery.v1.Model;
using IBM.Cloud.SDK.Core.Http;
using IBM.Cloud.SDK.Core.Authentication.Iam;

namespace IBM.Watson.Discovery.v1.Examples
{
    public class ServiceExample
    {
        string apikey = "{apikey}";
        string url = "{url}";
        string versionDate = "{versionDate}";
        private string environmentId;
        private string collectionId;
        private string configurationId;
        private string documentId;
        private string queryId;
        private string exampleId;
        private string filepathToIngest = @"DiscoveryTestData\watson_beats_jeopardy.html";
        private string stopwordFileToIngest = @"DiscoveryTestData\stopwords.txt";
        private string metadata = "{\"Creator\": \".NET SDK Example\",\"Subject\": \"Discovery service\"}";
        private string naturalLanguageQuery = "Who beat Ken Jennings in Jeopardy!";
        private string gatewayId;
        private string credentialId;
        private string sessionToken;   
        private static string configurationName;
        private static string updatedConfigurationName;
        private static string collectionName;
        private static string updatedCollectionName;

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            configurationName = Guid.NewGuid().ToString();
            updatedConfigurationName = configurationName + "-updated";
            collectionName = Guid.NewGuid().ToString();
            updatedCollectionName = collectionName + "-updated";

            example.ListEnvironments();
            //example.CreateEnvironment();      // Commented since we can only have one environment.
            example.GetEnvironment();
            //example.UpdateEnvironment();      // Commented since we do not want to change our current environment.

            example.ListConfigurations();
            example.CreateConfiguration();
            example.GetConfiguration();
            example.UpdateConfiguration();

            example.TestConfigurationInEnvironment();

            example.ListCollections();
            example.CreateCollection();
            example.GetCollection();
            example.UpdateCollection();
            example.ListCollectionFields();
            example.ListFields();

            example.ListExpansions();
            example.CreateExpansions();
            example.GetTokenizationDictionaryStatus();
            example.CreateTokenizationDictionary();
            example.GetStopwordListStatus();
            example.CreateStopwordList();

            example.AddDocument();
            example.GetDocument();
            example.UpdateDocument();

            example.Query();
            example.QueryNotices();
            example.FederatedQuery();
            example.FederatedQueryNotices();
            example.QueryEntities();
            example.QueryRelations();

            example.ListTrainingData();
            example.AddTrainingData();
            example.GetTrainingData();
            example.ListTrainingExamples();
            example.CreateTrainingExample();
            example.UpdateTrainingExample();
            example.GetTrainingExample();

            example.DeleteUserData();

            example.CreateEvent();
            example.QueryLog();
            example.GetMetricsQuery();
            example.GetMetricsQueryEvent();
            example.GetMetricsQueryNoResult();
            example.GetMetricsQueryEventRate();
            example.GetMetricsQueryTokenEvent();

            example.ListCredentials();
            example.CreateCredentials();
            example.GetCredentials();
            example.UpdateCredentials();

            example.ListGateways();
            example.CreateGateway();
            example.GetGateway();

            example.DeleteGateway();
            example.DeleteCredentials();
            example.DeleteTrainingExample();
            example.DeleteTrainingData();
            example.DeleteAllTraininData();
            example.DeleteDocument();
            example.DeleteStopwordList();
            example.DeleteTokenizationDictionary();
            example.DeleteExpansion();
            example.DeleteCollection();
            example.DeleteConfiguration();
            //example.DeleteEnvironment();      // Commented since we do not want to delete our environment.

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Environments
        public void ListEnvironments()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListEnvironments();

            Console.WriteLine(result.Response);

            foreach (Environment environment in result.Result.Environments)
            {
                if (environment._ReadOnly != true)
                {
                    environmentId = environment.EnvironmentId;
                }
            }
        }

        public void CreateEnvironment()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateEnvironment(
                name: "My environment"
                );

            Console.WriteLine(result.Response);
        }

        public void GetEnvironment()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetEnvironment(
                environmentId: environmentId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateEnvironment()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateEnvironment(
                environmentId: environmentId,
                name: "My updated environment"
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteEnvironment()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteEnvironment(
                environmentId: environmentId
                );

            Console.WriteLine(result.Response);
        }

        public void ListFields()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListFields(
                environmentId: environmentId,
                collectionIds: new List<string>() { collectionId }
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Configurations
        public void ListConfigurations()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListConfigurations(
                environmentId: environmentId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateConfiguration()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateConfiguration(
                environmentId: environmentId,
                name: configurationName
                );

            Console.WriteLine(result.Response);

            configurationId = result.Result.ConfigurationId;
        }

        public void GetConfiguration()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateConfiguration()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateConfiguration(
                environmentId: environmentId,
                configurationId: configurationId,
                name: updatedConfigurationName
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteConfiguration()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteConfiguration(
                environmentId: environmentId,
                configurationId: configurationId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Test Configuration in Environment
        public void TestConfigurationInEnvironment()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    var result = service.TestConfigurationInEnvironment(
                        environmentId: environmentId,
                        configurationId: configurationId,
                        file: ms, filename: "watson_beats_jeopardy.html",
                        fileContentType: "text/html"
                        );

                    Console.WriteLine(result.Response);
                }
            }

        }
        #endregion

        #region Collections
        public void ListCollections()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListCollections(
                environmentId: environmentId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateCollection()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateCollection(
                environmentId: environmentId,
                name: collectionName
                );

            Console.WriteLine(result.Response);

            collectionId = result.Result.CollectionId;
        }

        public void GetCollection()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateCollection()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateCollection(
                environmentId: environmentId,
                collectionId: collectionId,
                name: updatedCollectionName
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteCollection()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteCollection(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void ListCollectionFields()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListCollectionFields(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Query Modifications
        public void ListExpansions()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListExpansions(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateExpansions()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var expansions = new List<Expansion>()
            {
                new Expansion()
                {
                    InputTerms = new List<string>()
                    {
                        "input-term"
                    },
                    ExpandedTerms = new List<string>()
                    {
                        "expanded-term"
                    }
                }
            };

            var result = service.CreateExpansions(
                environmentId: environmentId,
                collectionId: collectionId,
                expansions: expansions
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteExpansion()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteExpansions(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void GetTokenizationDictionaryStatus()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetTokenizationDictionaryStatus(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateTokenizationDictionary()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var tokenizationRules = new List<TokenDictRule>()
            {
                new TokenDictRule()
                {
                    Text = "すしネコ",
                    Tokens = new List<string>()
                    {
                        "すし", "ネコ"
                    },
                    Readings = new List<string>()
                    {
                        "寿司", "ネコ"
                    },
                    PartOfSpeech = "カスタム名詞"
                }
            };

            var result = service.CreateTokenizationDictionary(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    tokenizationRules: tokenizationRules
                    );

            Console.WriteLine(result.Response);
        }

        public void DeleteTokenizationDictionary()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteTokenizationDictionary(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void GetStopwordListStatus()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetStopwordListStatus(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateStopwordList()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            DetailedResponse<TokenDictStatusResponse> result;
            using (FileStream fs = File.OpenRead(stopwordFileToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.CreateStopwordList(
                        environmentId: environmentId,
                        collectionId: collectionId, 
                        stopwordFile: ms,
                        stopwordFilename: Path.GetFileName(stopwordFileToIngest)
                        );
                }
            }

            Console.WriteLine(result.Response);
        }

        public void DeleteStopwordList()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteStopwordList(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Documents
        public void AddDocument()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            DetailedResponse<DocumentAccepted> result;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.AddDocument(
                    environmentId: environmentId,
                    collectionId: collectionId,
                    file: ms,
                    filename: "watson_beats_jeopardy.html",
                    fileContentType: "text/html",
                    metadata: metadata
                    );

                    documentId = result.Result.DocumentId;
                }
            }

            Console.WriteLine(result.Response);
        }

        public void GetDocument()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetDocumentStatus(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: documentId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateDocument()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            DetailedResponse<DocumentAccepted> result;
            using (FileStream fs = File.OpenRead(filepathToIngest))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    result = service.UpdateDocument(
                        environmentId: environmentId,
                        collectionId: collectionId,
                        documentId: documentId,
                        file: ms,
                        filename: "watson_beats_jeopardy.html",
                        fileContentType: "text/html",
                        metadata: metadata
                        );
                }
            }

            Console.WriteLine(result.Response);
        }

        public void DeleteDocument()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteDocument(
                environmentId: environmentId,
                collectionId: collectionId,
                documentId: documentId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Queries
        public void Query()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.Query(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                returnFields: "extracted_metadata.sha1"
                );

            Console.WriteLine(result.Response);

            sessionToken = result.Result.SessionToken;
        }

        public void QueryNotices()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.QueryNotices(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                passages: true
                );

            Console.WriteLine(result.Response);
        }

        public void FederatedQuery()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.FederatedQuery(
                environmentId: environmentId,
                naturalLanguageQuery: naturalLanguageQuery,
                returnFields: "extracted_metadata.sha1"
                );

            Console.WriteLine(result.Response);
        }

        public void FederatedQueryNotices()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.FederatedQueryNotices(
                environmentId: environmentId,
                naturalLanguageQuery: naturalLanguageQuery,
                collectionIds: new List<string> { collectionId }
                );

            Console.WriteLine(result.Response);
        }

        public void QueryEntities()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.QueryEntities(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void QueryRelations()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.QueryRelations(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Training Data
        public void ListTrainingData()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListTrainingData(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void AddTrainingData()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var examples = new List<TrainingExample>()
            {
                new TrainingExample()
                {
                    DocumentId = "documentId",
                    CrossReference = "crossReference",
                    Relevance = 1
                }
            };

            var result = service.AddTrainingData(
                environmentId: environmentId,
                collectionId: collectionId,
                naturalLanguageQuery: naturalLanguageQuery,
                filter: "filter",
                examples: examples
                );
            queryId = result.Result.QueryId;

            Console.WriteLine(result.Response);
        }

        public void DeleteAllTraininData()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteAllTrainingData(
                environmentId: environmentId,
                collectionId: collectionId
                );

            Console.WriteLine(result.Response);
        }

        public void GetTrainingData()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetTrainingData(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteTrainingData()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteTrainingData(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId
                );

            Console.WriteLine(result.Response);
        }

        public void ListTrainingExamples()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListTrainingExamples(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateTrainingExample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId,
                documentId: documentId,
                relevance: 1
                );

            Console.WriteLine(result.Response);

            exampleId = result.Result.DocumentId;
        }

        public void DeleteTrainingExample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId,
                exampleId: exampleId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateTrainingExample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.UpdateTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId,
                exampleId: exampleId,
                crossReference: "crossReference",
                relevance: 1
                );

            Console.WriteLine(result.Response);
        }

        public void GetTrainingExample()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetTrainingExample(
                environmentId: environmentId,
                collectionId: collectionId,
                queryId: queryId,
                exampleId: exampleId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region User data
        public void DeleteUserData()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteUserData(
                customerId: "customerId"
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Events and Feedback
        public void CreateEvent()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var data = new EventData()
            {
                EnvironmentId = environmentId,
                SessionToken = sessionToken,
                CollectionId = collectionId,
                DocumentId = documentId
            };

            var result = service.CreateEvent(
                type: CreateEventResponse.TypeEnumValue.CLICK,
                data: data
                );

            Console.WriteLine(result.Response);
        }

        public void QueryLog()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.QueryLog();

            Console.WriteLine(result.Response);
        }

        public void GetMetricsQuery()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetMetricsQuery();

            Console.WriteLine(result.Response);
        }

        public void GetMetricsQueryEvent()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetMetricsQueryEvent();

            Console.WriteLine(result.Response);
        }

        public void GetMetricsQueryNoResult()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetMetricsQueryNoResults();

            Console.WriteLine(result.Response);
        }

        public void GetMetricsQueryEventRate()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetMetricsEventRate();

            Console.WriteLine(result.Response);
        }

        public void GetMetricsQueryTokenEvent()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetMetricsQueryTokenEvent();

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Credentials
        public void ListCredentials()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListCredentials(
                environmentId: environmentId
                );

            Console.WriteLine(result.Response);
        }

        public void CreateCredentials()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var credentialDetails = new CredentialDetails()
            {
                CredentialType = CredentialDetails.CredentialTypeEnumValue.OAUTH2,
                EnterpriseId = "myEnterpriseId",
                ClientId = "myClientId",
                ClientSecret = "myClientSecret",
                PublicKeyId = "myPublicIdKey",
                Passphrase = "myPassphrase",
                PrivateKey = "myPrivateKey"
            };

            var result = service.CreateCredentials(
                environmentId: environmentId,
                sourceType: Credentials.SourceTypeEnumValue.BOX,
                credentialDetails: credentialDetails
                );

            Console.WriteLine(result.Response);

            credentialId = result.Result.CredentialId;
        }

        public void GetCredentials()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetCredentials(
                environmentId: environmentId,
                credentialId: credentialId
                );

            Console.WriteLine(result.Response);
        }

        public void UpdateCredentials()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            string privateKey = "privatekey";
            var privateKeyBytes = System.Text.Encoding.UTF8.GetBytes(privateKey);
            var base64PrivateKey = Convert.ToBase64String(privateKeyBytes);

            var updatedCredentialDetails = new CredentialDetails()
            {
                CredentialType = CredentialDetails.CredentialTypeEnumValue.OAUTH2,
                EnterpriseId = "myEnterpriseIdUpdated",
                ClientId = "myClientIdUpdated",
                ClientSecret = "myClientSecretUpdated",
                PublicKeyId = "myPublicIdKeyUpdated",
                Passphrase = "myPassphraseUpdated",
                PrivateKey = base64PrivateKey
            };

            var result = service.UpdateCredentials(
                environmentId: environmentId,
                credentialId: credentialId,
                sourceType: Credentials.SourceTypeEnumValue.BOX,
                credentialDetails: updatedCredentialDetails
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteCredentials()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteCredentials(
                environmentId: environmentId,
                credentialId: credentialId
                );

            Console.WriteLine(result.Response);
        }
        #endregion

        #region Gateway Configuration
        public void ListGateways()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.ListGateways(
                environmentId: environmentId
                );
                
            Console.WriteLine(result.Response);
        }

        public void CreateGateway()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.CreateGateway(
                environmentId: environmentId,
                name: "dotnet-sdk-example-gateway"
                );

            Console.WriteLine(result.Response);

            gatewayId = result.Result.GatewayId;
        }

        public void GetGateway()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.GetGateway(
                environmentId: environmentId,
                gatewayId: gatewayId
                );

            Console.WriteLine(result.Response);
        }

        public void DeleteGateway()
        {
            IamConfig config = new IamConfig(
                apikey: apikey
                );

            DiscoveryService service = new DiscoveryService(versionDate, config);
            service.SetEndpoint(url);

            var result = service.DeleteGateway(
                environmentId: environmentId,
                gatewayId: gatewayId
                );

            Console.WriteLine(result.Response);
        }
        #endregion
    }
}
