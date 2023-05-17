using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Pandora.Definitions.Attributes;
using Pandora.Definitions.Attributes.Validation;
using Pandora.Definitions.CustomTypes;


// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See NOTICE.txt in the project root for license information.


namespace Pandora.Definitions.ResourceManager.Network.v2022_11_01.NetworkProfiles;


internal class ContainerNetworkInterfaceConfigurationPropertiesFormatModel
{
    [JsonPropertyName("containerNetworkInterfaces")]
    public List<SubResourceModel>? ContainerNetworkInterfaces { get; set; }

    [JsonPropertyName("ipConfigurations")]
    public List<IPConfigurationProfileModel>? IPConfigurations { get; set; }

    [JsonPropertyName("provisioningState")]
    public ProvisioningStateConstant? ProvisioningState { get; set; }
}
