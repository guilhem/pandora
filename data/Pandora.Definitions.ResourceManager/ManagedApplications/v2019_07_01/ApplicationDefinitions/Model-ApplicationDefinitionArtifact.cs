using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Pandora.Definitions.Attributes;
using Pandora.Definitions.Attributes.Validation;
using Pandora.Definitions.CustomTypes;


// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See NOTICE.txt in the project root for license information.


namespace Pandora.Definitions.ResourceManager.ManagedApplications.v2019_07_01.ApplicationDefinitions;


internal class ApplicationDefinitionArtifactModel
{
    [JsonPropertyName("name")]
    [Required]
    public ApplicationDefinitionArtifactNameConstant Name { get; set; }

    [JsonPropertyName("type")]
    [Required]
    public ApplicationArtifactTypeConstant Type { get; set; }

    [JsonPropertyName("uri")]
    [Required]
    public string Uri { get; set; }
}
