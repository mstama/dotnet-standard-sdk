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

namespace IBM.Watson.CompareComply.v1.Model
{
    /// <summary>
    /// A monetary amount identified in the input document.
    /// </summary>
    public class ContractAmts
    {
        /// <summary>
        /// The confidence level in the identification of the contract amount.
        /// </summary>
        public class ConfidenceLevelEnumValue
        {
            /// <summary>
            /// Constant HIGH for High
            /// </summary>
            public const string HIGH = "High";
            /// <summary>
            /// Constant MEDIUM for Medium
            /// </summary>
            public const string MEDIUM = "Medium";
            /// <summary>
            /// Constant LOW for Low
            /// </summary>
            public const string LOW = "Low";
            
        }

        /// <summary>
        /// The confidence level in the identification of the contract amount.
        /// Constants for possible values can be found using ContractAmts.ConfidenceLevelEnumValue
        /// </summary>
        [JsonProperty("confidence_level", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfidenceLevel { get; set; }
        /// <summary>
        /// The monetary amount.
        /// </summary>
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        /// <summary>
        /// The normalized form of the amount, which is listed as a string. This element is optional; that is, the
        /// service output lists it only if normalized text exists.
        /// </summary>
        [JsonProperty("text_normalized", NullValueHandling = NullValueHandling.Ignore)]
        public string TextNormalized { get; set; }
        /// <summary>
        /// The details of the normalized text, if applicable. This element is optional; that is, the service output
        /// lists it only if normalized text exists.
        /// </summary>
        [JsonProperty("interpretation", NullValueHandling = NullValueHandling.Ignore)]
        public Interpretation Interpretation { get; set; }
        /// <summary>
        /// One or more hash values that you can send to IBM to provide feedback or receive support.
        /// </summary>
        [JsonProperty("provenance_ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ProvenanceIds { get; set; }
        /// <summary>
        /// The numeric location of the identified element in the document, represented with two integers labeled
        /// `begin` and `end`.
        /// </summary>
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public Location Location { get; set; }
    }

}
