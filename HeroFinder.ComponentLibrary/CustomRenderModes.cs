using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroFinder.ComponentLibrary;
public static class CustomRenderModes
{
    public static readonly InteractiveServerRenderMode InteractiveServerNoPreRender
        = new InteractiveServerRenderMode(prerender: false);

    public static readonly InteractiveWebAssemblyRenderMode InteractiveWebAssemblyNoPreRender
        = new InteractiveWebAssemblyRenderMode(prerender: false);

    public static readonly InteractiveAutoRenderMode InteractiveAutoNoPreRender
        = new InteractiveAutoRenderMode(prerender: false);
}

