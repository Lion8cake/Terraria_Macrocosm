using Macrocosm.Common.Systems.Connectors;

namespace Macrocosm.Content.Items.Connectors;

public class ConveyorBlueWrench : ConveyorPipeWrench
{
    public override ConveyorPipeType PipeType => ConveyorPipeType.BluePipe;
}
