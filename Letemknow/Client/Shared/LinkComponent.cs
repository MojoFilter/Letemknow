using Letemknow.Shared;
using Microsoft.AspNetCore.Components;

namespace Letemknow.Client.Shared
{
    public class LinkComponent : ComponentBase
    {
        [Parameter]
        public MailLink? Mail { get; set; }
    }
}
