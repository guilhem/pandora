using Pandora.Definitions.Attributes;
using Pandora.Definitions.CustomTypes;
using Pandora.Definitions.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;


// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See NOTICE.txt in the project root for license information.


namespace Pandora.Definitions.ResourceManager.Network.v2022_11_01.ExpressRouteCircuitStats;

internal class ExpressRouteCircuitsGetPeeringStatsOperation : Pandora.Definitions.Operations.GetOperation
{
    public override ResourceID? ResourceId() => new ExpressRouteCircuitPeeringId();

    public override Type? ResponseObject() => typeof(ExpressRouteCircuitStatsModel);

    public override string? UriSuffix() => "/stats";


}
