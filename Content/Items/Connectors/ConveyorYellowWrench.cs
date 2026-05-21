using Macrocosm.Common.Systems.Connectors;

namespace Macrocosm.Content.Items.Connectors;

public class ConveyorYellowWrench : ConveyorPipeWrench
{
    public override ConveyorPipeType PipeType => ConveyorPipeType.YellowPipe;
}
