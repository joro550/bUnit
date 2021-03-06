using Xunit;
using Bunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using static Bunit.ComponentParameterFactory;

namespace Bunit.Docs.Samples
{
  public class ReRenderTest
  {
    [Fact]
    public void RenderAgainUsingRender()
    {
      // Arrange - renders the Heading component
      using var ctx = new TestContext();
      var cut = ctx.RenderComponent<Heading>();
      Assert.Equal(1, cut.RenderCount);

      // Re-render without new parameters
      cut.Render();

      Assert.Equal(2, cut.RenderCount);
    }

    [Fact]
    public void RenderAgainUsingSetParametersAndRender()
    {
      // Arrange - renders the Heading component
      using var ctx = new TestContext();
      var cut = ctx.RenderComponent<Item>(parameters => parameters
        .Add(p => p.Value, "Foo")
      );
      cut.MarkupMatches("<span>Foo</span>");

      // Re-render with new parameters
      cut.SetParametersAndRender(parameters => parameters
        .Add(p => p.Value, "Bar")
      );

      cut.MarkupMatches("<span>Bar</span>");
    }

    [Fact]
    public void RendersViaInvokeAsync()
    {
      // Arrange - renders the Heading component
      using var ctx = new TestContext();
      var cut = ctx.RenderComponent<ImparativeCalc>();

      // Indirectly re-renders through the call to StateHasChanged 
      // in the Calculate(x, y) method.
      cut.InvokeAsync(() => cut.Instance.Calculate(1, 2));

      cut.MarkupMatches("<output>3</output>");
    }
  }
}