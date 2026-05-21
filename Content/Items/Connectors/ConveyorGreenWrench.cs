using Macrocosm.Common.Systems.Connectors;

namespace Macrocosm.Content.Items.Connectors;

public class ConveyorGreenWrench : ConveyorPipeWrench
{
    public override ConveyorPipeType PipeType => ConveyorPipeType.GreenPipe;
}
