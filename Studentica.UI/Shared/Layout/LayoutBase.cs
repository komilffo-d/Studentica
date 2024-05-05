using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Studentica.UI.Shared.Layout
{
    public abstract partial class LayoutBase: LayoutComponentBase
    {
        internal List<BreadcrumbItem> _links;

        internal abstract List<BreadcrumbItem> Links { get; set; }
    }
}
