using Letemknow.Shared;
using Microsoft.AspNetCore.Components;

namespace Letemknow.Client.Shared
{
    public abstract class LinkComponent : ComponentBase
    {

        [Parameter]
        public MailLink? Mail { get; set; }

        [Inject]
        public ILinkBusiness Link { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public ITrackingClient Tracking { get; set; }
        
        protected abstract string LinkUri { get; }

        protected abstract ClickTarget Target { get; }


        protected async Task OnClick()
        {
            await Tracking.TrackMailToClickAsync(ClickTarget.Outlook);
            Navigation.NavigateTo(LinkUri);
        }
    }
}
