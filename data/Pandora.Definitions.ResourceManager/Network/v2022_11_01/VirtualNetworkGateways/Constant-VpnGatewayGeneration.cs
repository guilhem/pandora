using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.Network.v2022_11_01.VirtualNetworkGateways;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum VpnGatewayGenerationConstant
{
    [Description("Generation1")]
    GenerationOne,

    [Description("Generation2")]
    GenerationTwo,

    [Description("None")]
    None,
}
