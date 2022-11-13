using Microsoft.Kiota.Abstractions.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Teamified.Sdk.Models {
    public class Team : IParsable {
        /// <summary>The channels property</summary>
        public List<Channel> Channels { get; set; }
        /// <summary>The description property</summary>
        public string Description { get; set; }
        /// <summary>The displayName property</summary>
        public string DisplayName { get; set; }
        /// <summary>The id property</summary>
        public string Id { get; set; }
        /// <summary>The members property</summary>
        public List<IdentityPrincipal> Members { get; set; }
        /// <summary>The owners property</summary>
        public List<IdentityPrincipal> Owners { get; set; }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        /// </summary>
        public static Team CreateFromDiscriminatorValue(IParseNode parseNode) {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new Team();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        public IDictionary<string, Action<IParseNode>> GetFieldDeserializers() {
            return new Dictionary<string, Action<IParseNode>> {
                {"channels", n => { Channels = n.GetCollectionOfObjectValues<Channel>(Channel.CreateFromDiscriminatorValue)?.ToList(); } },
                {"description", n => { Description = n.GetStringValue(); } },
                {"displayName", n => { DisplayName = n.GetStringValue(); } },
                {"id", n => { Id = n.GetStringValue(); } },
                {"members", n => { Members = n.GetCollectionOfObjectValues<IdentityPrincipal>(IdentityPrincipal.CreateFromDiscriminatorValue)?.ToList(); } },
                {"owners", n => { Owners = n.GetCollectionOfObjectValues<IdentityPrincipal>(IdentityPrincipal.CreateFromDiscriminatorValue)?.ToList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        /// </summary>
        public void Serialize(ISerializationWriter writer) {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<Channel>("channels", Channels);
            writer.WriteStringValue("description", Description);
            writer.WriteStringValue("displayName", DisplayName);
            writer.WriteStringValue("id", Id);
            writer.WriteCollectionOfObjectValues<IdentityPrincipal>("members", Members);
            writer.WriteCollectionOfObjectValues<IdentityPrincipal>("owners", Owners);
        }
    }
}
