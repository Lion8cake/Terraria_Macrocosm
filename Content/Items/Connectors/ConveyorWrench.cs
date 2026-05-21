using Macrocosm.Common.Systems.Connectors;

namespace Macrocosm.Content.Items.Connectors;

public class ConveyorWrench : ConveyorPipeWrench
{
    public override ConveyorPipeType PipeType => ConveyorPipeType.RedPipe;
}
