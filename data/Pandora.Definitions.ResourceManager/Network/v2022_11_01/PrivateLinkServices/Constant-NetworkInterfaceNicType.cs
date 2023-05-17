using Pandora.Definitions.Attributes;
using System.ComponentModel;

namespace Pandora.Definitions.ResourceManager.Network.v2022_11_01.PrivateLinkServices;

[ConstantType(ConstantTypeAttribute.ConstantType.String)]
internal enum NetworkInterfaceNicTypeConstant
{
    [Description("Elastic")]
    Elastic,

    [Description("Standard")]
    Standard,
}
