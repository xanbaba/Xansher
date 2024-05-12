using Xansher.CustomControls;
using Xansher.Model;

namespace Xansher.Messages.MetaData;

public class AddActiveDocumentMessageMetaData
{
    public required CodeFileModel CodeFileModel { get; init; }
    public required ProjectElementButton ProjectElementButton { get; init; }
}