using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Pandora.Definitions.Attributes;
using Pandora.Definitions.Attributes.Validation;
using Pandora.Definitions.CustomTypes;


// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See NOTICE.txt in the project root for license information.


namespace Pandora.Definitions.ResourceManager.RecoveryServices.v2023_06_01.GetPrivateLinkResources;


internal class PrivateLinkResourcePropertiesModel
{
    [JsonPropertyName("groupId")]
    public string? GroupId { get; set; }

    [JsonPropertyName("requiredMembers")]
    public List<string>? RequiredMembers { get; set; }

    [JsonPropertyName("requiredZoneNames")]
    public List<string>? RequiredZoneNames { get; set; }
}
