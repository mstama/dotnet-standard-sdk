/**
* (C) Copyright IBM Corp. 2018, 2019.
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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBM.Watson.Discovery.v1.Model
{
    /// <summary>
    /// DocumentAccepted.
    /// </summary>
    public class DocumentAccepted
    {
        /// <summary>
        /// Status of the document in the ingestion process. A status of `processing` is returned for documents that are
        /// ingested with a *version* date before `2019-01-01`. The `pending` status is returned for all others.
        /// </summary>
        public class StatusEnumValue
        {
            /// <summary>
            /// Constant PROCESSING for processing
            /// </summary>
            public const string PROCESSING = "processing";
            /// <summary>
            /// Constant PENDING for pending
            /// </summary>
            public const string PENDING = "pending";
            
        }

        /// <summary>
        /// Status of the document in the ingestion process. A status of `processing` is returned for documents that are
        /// ingested with a *version* date before `2019-01-01`. The `pending` status is returned for all others.
        /// Constants for possible values can be found using DocumentAccepted.StatusEnumValue
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        /// <summary>
        /// The unique identifier of the ingested document.
        /// </summary>
        [JsonProperty("document_id", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentId { get; set; }
        /// <summary>
        /// Array of notices produced by the document-ingestion process.
        /// </summary>
        [JsonProperty("notices", NullValueHandling = NullValueHandling.Ignore)]
        public List<Notice> Notices { get; set; }
    }

}
