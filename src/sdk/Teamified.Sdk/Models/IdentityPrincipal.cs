using Microsoft.Kiota.Abstractions.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Teamified.Sdk.Models {
    public class IdentityPrincipal : IParsable {
        /// <summary>The displayName property</summary>
        public string DisplayName { get; set; }
        /// <summary>The email property</summary>
        public string Email { get; set; }
        /// <summary>The id property</summary>
        public string Id { get; set; }
        /// <summary>The jobTitle property</summary>
        public string JobTitle { get; set; }
        /// <summary>The userPrincipalName property</summary>
        public string UserPrincipalName { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        /// </summary>
        public static IdentityPrincipal CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new IdentityPrincipal();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"displayName", n => { DisplayName = n.GetStringValue(); } },
                {"email", n => { Email = n.GetStringValue(); } },
                {"id", n => { Id = n.GetStringValue(); } },
                {"jobTitle", n => { JobTitle = n.GetStringValue(); } },
                {"userPrincipalName", n => { UserPrincipalName = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        /// </summary>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteStringValue("email", Email);
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("jobTitle", JobTitle);
            writer.WriteStringValue("userPrincipalName", UserPrincipalName);
        }
    }
}
