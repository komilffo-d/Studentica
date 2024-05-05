using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Studentica.UI.Shared.Layout
{
    public abstract partial class LayoutBase: LayoutComponentBase
    {
        private protected List<BreadcrumbItem> _links=new();

        internal abstract List<BreadcrumbItem> Links { get; set; }
    }
}
