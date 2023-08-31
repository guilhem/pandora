using Pandora.Definitions.Attributes;
using Pandora.Definitions.CustomTypes;
using Pandora.Definitions.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;


// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See NOTICE.txt in the project root for license information.


namespace Pandora.Definitions.ResourceManager.NetworkCloud.v2023_07_01.Networkclouds;

internal class AgentPoolsListByKubernetesClusterOperation : Pandora.Definitions.Operations.ListOperation
{
    public override string? FieldContainingPaginationDetails() => "nextLink";

    public override ResourceID? ResourceId() => new KubernetesClusterId();

    public override Type NestedItemType() => typeof(AgentPoolModel);

    public override string? UriSuffix() => "/agentPools";


}
