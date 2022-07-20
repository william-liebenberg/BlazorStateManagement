using Fluxor;
using Shared;
using System;
using System.Collections.Generic;

namespace BlazorGrid.ItemState
{
    public record ItemState
    {
        public List<ItemView> ItemViews { get; init; }
    }

    public class ItemStateFeature : Feature<ItemState>
    {
        public override string GetName() => nameof(ItemState);

        protected override ItemState GetInitialState() => new()
        {
            ItemViews = new List<ItemView>(),
        };
    }
}